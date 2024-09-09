using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.UI.Services.CategoryService;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;

namespace WEB_253505_PAVLOVICH.UI.Areas.Admin.Pages;

public class CreateModel : PageModel
{
    private readonly IDeviceService _deviceService;
    private readonly ICategoryService _categoryService;

    public CreateModel(IDeviceService deviceService, ICategoryService categoryService)
    {
        _deviceService = deviceService;
        _categoryService = categoryService;
        Categories = new SelectList(_categoryService.GetCategoryListAsync().Result.Data, "Id", "Name");
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Device Device { get; set; } = default!;
    [BindProperty]
    public IFormFile? Image { get; set; }
    public SelectList Categories { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var response = await _deviceService.CreateDeviceAsync(Device, Image);

        if (!response.Successfull)
        {
            return Page();
        }
        return RedirectToPage("./Index");
    }
}
