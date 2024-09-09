using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;

namespace WEB_253505_PAVLOVICH.UI.Areas.Admin.Pages;

public class IndexModel : PageModel
{
    private readonly IDeviceService _deviceService;

    public IndexModel(IDeviceService deviceService)
    {
        _deviceService = deviceService;

        var devices = _deviceService.GetDeviceListAsync(null).Result.Data;
        for (int i = 1; i <= devices?.TotalPages; i++)
        {
            Devices.AddRange(_deviceService.GetDeviceListAsync(null, i).Result.Data!.Items);
        }
    }

    [BindProperty]
    public List<Device> Devices { get;set; } = new();

    public void OnGet()
    {
        
    }
}
