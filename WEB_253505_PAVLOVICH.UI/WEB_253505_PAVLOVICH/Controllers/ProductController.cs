using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<IActionResult> Index()
    {
        var deviceResponce = await _deviceService.GetDeviceListAsync(null);
        if (!deviceResponce.Successfull)
            return NotFound(deviceResponce.ErrorMessage);
        return View(deviceResponce.Data);
    }
}
