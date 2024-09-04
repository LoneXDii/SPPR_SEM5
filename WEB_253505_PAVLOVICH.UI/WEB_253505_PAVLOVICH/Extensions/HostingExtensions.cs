using WEB_253505_PAVLOVICH.UI.Services.CategoryService;

namespace WEB_253505_PAVLOVICH.UI.Extensions;

public static class HostingExtensions
{
    public static void RegisterCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
    }
}
