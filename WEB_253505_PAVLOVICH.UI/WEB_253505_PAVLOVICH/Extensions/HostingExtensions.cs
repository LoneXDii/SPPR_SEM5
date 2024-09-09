using WEB_253505_PAVLOVICH.UI.Services.CategoryService;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;
using WEB_253505_PAVLOVICH.UI.Services.FileService;

namespace WEB_253505_PAVLOVICH.UI.Extensions;

public static class HostingExtensions
{
    public static void RegisterCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICategoryService, ApiCategoryService>()
                        .AddScoped<IDeviceService, ApiDeviceService>()
                        .AddScoped<IFileService, ApiFileService>();
    }
}
