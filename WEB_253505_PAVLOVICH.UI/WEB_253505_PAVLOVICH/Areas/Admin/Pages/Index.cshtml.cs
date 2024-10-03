using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.UI.Extensions;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;

namespace WEB_253505_PAVLOVICH.UI.Areas.Admin.Pages;

public class IndexModel : PageModel
{
    private readonly IDeviceService _deviceService;

    public IndexModel(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [BindProperty]
    public List<Device> Devices { get;set; } = new();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    public async Task<IActionResult> OnGetAsync(int pageNo = 1)
    {
        var response = await _deviceService.GetDeviceListAsync(null, pageNo);
        if (response.Successfull)
        {
            Devices = response.Data.Items;
            TotalPages = response.Data.TotalPages;
            CurrentPage = pageNo;

            if (Request.IsAjaxRequest())
            {
                return Partial("_PaginationPartial", new
                {
                    Admin = true,
                    CurrentPage = CurrentPage,
                    TotalPages = TotalPages,
                    Devices = Devices
                });
            }

            return Page();
        }

        return RedirectToPage("/Error");
    }
}
