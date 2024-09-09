using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.UI.Services.CategoryService;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;

namespace WEB_253505_PAVLOVICH.UI.Areas.Admin.Pages;

public class DetailsModel : PageModel
{
    private readonly IDeviceService _deviceService;
    private readonly ICategoryService _categoryService;

    public DetailsModel(IDeviceService deviceService, ICategoryService categoryService)
    {
        _deviceService = deviceService;
        _categoryService = categoryService;
    }

    public Device Device { get; set; } = default!;
    public Category Category { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var device = await _deviceService.GetDeviceByIdAsync(id.Value);
        if (device == null)
        {
            return NotFound();
        }
        else
        {
            Device = device.Data!;
            var categories = await _categoryService.GetCategoryListAsync();
            Category = categories.Data?.FirstOrDefault(c => c.Id == Device.CategoryId)!;
        }
        return Page();
    }
}
