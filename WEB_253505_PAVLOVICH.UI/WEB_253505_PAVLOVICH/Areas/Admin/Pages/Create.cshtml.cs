using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;

namespace WEB_253505_PAVLOVICH.UI.Areas.Admin.Pages;

public class CreateModel : PageModel
{
    private readonly IDeviceService _deviceService;

    public CreateModel(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Device Device { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        throw new NotImplementedException();
        //_deviceService.CreateDeviceAsync(Device);

        return RedirectToPage("./Index");
    }
}
