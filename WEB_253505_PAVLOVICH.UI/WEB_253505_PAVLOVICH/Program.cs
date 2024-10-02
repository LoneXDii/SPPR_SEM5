using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Configuration;
using WEB_253505_PAVLOVICH.UI.Extensions;
using WEB_253505_PAVLOVICH.UI.HelpClasses;
using WEB_253505_PAVLOVICH.UI.Services.Authentication;
using WEB_253505_PAVLOVICH.UI.Services.CategoryService;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;
using WEB_253505_PAVLOVICH.UI.Services.FileService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.RegisterCustomServices();
builder.Services.AddRazorPages();

UriData.ApiUri = builder.Configuration.GetSection("UriData").GetValue<string>("ApiUri")!;

builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(opt => opt.BaseAddress = new Uri(UriData.ApiUri));
builder.Services.AddHttpClient<IDeviceService, ApiDeviceService>(opt =>opt.BaseAddress = new Uri(UriData.ApiUri));
builder.Services.AddHttpClient<IFileService, ApiFileService>(opt =>opt.BaseAddress = new Uri($"{UriData.ApiUri}Files"));

var keycloakData =
builder.Configuration.GetSection("Keycloak").Get<KeycloakData>();
builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme =
                    CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                    OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddJwtBearer()
                .AddOpenIdConnect(opt =>
                {
                    opt.Authority = $"{keycloakData.Host}/auth/realms/{keycloakData.Realm}";
                    opt.ClientId = keycloakData.ClientId;
                    opt.ClientSecret = keycloakData.ClientSecret;
                    opt.ResponseType = OpenIdConnectResponseType.Code;
                    opt.Scope.Add("openid");
                    opt.SaveTokens = true;
                    opt.RequireHttpsMetadata = false;
                    opt.MetadataAddress = $"{keycloakData.Host}/realms/{keycloakData.Realm}/.well-known/openid-configuration";
                });

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<ITokenAccessor, KeycloakTokenAccessor>();

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

app.MapRazorPages();

app.Run();
