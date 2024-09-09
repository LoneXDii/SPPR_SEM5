using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;

namespace WEB_253505_PAVLOVICH.UI.Areas.Admin.Pages;

public class DeleteModel : PageModel
{
    private readonly IDeviceService _deviceService;

    public DeleteModel(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [BindProperty]
    public Device Device { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var device = await _deviceService.GetDeviceByIdAsync(id.Value);

        if (device is null)
        {
            return NotFound();
        }
        else
        {
            Device = device.Data!;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var device = await _deviceService.GetDeviceByIdAsync(id.Value);
        if (device != null)
        {
            Device = device.Data!;
            await _deviceService.DeleteDeviceAsync(id.Value);
        }

        return RedirectToPage("./Index");
    }
}
