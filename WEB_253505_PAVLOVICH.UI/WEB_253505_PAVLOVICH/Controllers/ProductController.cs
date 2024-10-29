using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_253505_PAVLOVICH.UI.Extensions;
using WEB_253505_PAVLOVICH.UI.Services.CategoryService;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;

namespace WEB_253505_PAVLOVICH.UI.Controllers;

public class ProductController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IDeviceService _deviceService;

    public ProductController(ICategoryService categoryService, IDeviceService deviceService)
    {
        _categoryService = categoryService;
        _deviceService = deviceService;
    }

    [Route("devices/{category?}")]
    public async Task<IActionResult> Index(string? category, int PageNo = 1)
    {
        var deviceResponce = await _deviceService.GetDeviceListAsync(category, PageNo);
        if (!deviceResponce.Successfull)
        {
            return NotFound(deviceResponce.ErrorMessage);
        }

        var categoriesList = await _categoryService.GetCategoryListAsync();
        if (!categoriesList.Successfull)
        {
            return NotFound(categoriesList.ErrorMessage);
        }

        var categoryName = category is null 
            ? "Все" 
            : categoriesList.Data?.FirstOrDefault(c => c.NormalizedName.Equals(category))?.Name;

        ViewData["CurrentCategory"] = categoryName;
        ViewData["Categories"] = categoriesList.Data;

        if (Request.IsAjaxRequest())
        {
            return PartialView("_PaginationPartial", new
            {
                CurrentCategory = category,
                Categories = categoriesList.Data,
                Devices = deviceResponce.Data!.Items,
                ReturnUrl = Request.Path + Request.QueryString.ToUriComponent(),
                CurrentPage = deviceResponce.Data.CurrentPage,
                TotalPages = deviceResponce.Data.TotalPages,
                Admin = false
            });
        }

        return View(deviceResponce.Data);
    }
}
