using Microsoft.EntityFrameworkCore;
using WEB_253505_PAVLOVICH.API.Data;
using WEB_253505_PAVLOVICH.API.Services.CategoryService;
using WEB_253505_PAVLOVICH.API.Services.DeviceService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
var connStr = builder.Configuration.GetConnectionString("MySQLConnection");
ServerVersion vesrion = ServerVersion.AutoDetect(connStr);
builder.Services.AddDbContext<AppDbContext>(opt =>
                    opt.UseMySql(connStr, new MySqlServerVersion(new Version(8, 0, 36))),
                    ServiceLifetime.Scoped);

// DI
builder.Services.AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IDeviceService, DeviceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

//await DbInitializer.SeedData(app);

app.Run();
