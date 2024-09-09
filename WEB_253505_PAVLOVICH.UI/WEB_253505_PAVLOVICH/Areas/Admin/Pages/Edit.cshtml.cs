using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;

namespace WEB_253505_PAVLOVICH.UI.Areas.Admin.Pages;

public class EditModel : PageModel
{
    private readonly IDeviceService _deviceService;

    public EditModel(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [BindProperty]
    public Device Device { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var device =  await _deviceService.GetDeviceByIdAsync(id.Value);
        if (device == null)
        {
            return NotFound();
        }
        Device = device.Data!;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _deviceService.UpdateDeviceAsync(Device.Id, Device, null);


        return RedirectToPage("./Index");
    }

    private bool DeviceExists(int id)
    {
        return _deviceService.GetDeviceByIdAsync(id).Result is null
               ? false
               : true;
    }
}
