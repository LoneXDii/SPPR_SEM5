using Microsoft.Extensions.Options;
using WEB_253505_PAVLOVICH.UI.Extensions;
using WEB_253505_PAVLOVICH.UI.HelpClasses;
using WEB_253505_PAVLOVICH.UI.Services.CategoryService;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.RegisterCustomServices();

UriData.ApiUri = builder.Configuration.GetSection("UriData").GetValue<string>("ApiUri")!;

builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(opt => opt.BaseAddress = new Uri(UriData.ApiUri));
builder.Services.AddHttpClient<IDeviceService, ApiDeviceService>(opt =>opt.BaseAddress = new Uri(UriData.ApiUri));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
