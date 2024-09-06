using Microsoft.EntityFrameworkCore;
using WEB_253505_PAVLOVICH.Domain.Entities;

namespace WEB_253505_PAVLOVICH.API.Data;

public class DbInitializer
{
    public static async Task SeedData(WebApplication app)
    {
        var baseUrl = app.Configuration.GetSection("BaseUrl").Value;

        var categories = new List<Category>
        {
            new Category{Name = "Смартфоны", NormalizedName = "smartphones"},
            new Category{Name = "Планшеты", NormalizedName = "tablets"},
            new Category{Name = "Ноутбуки", NormalizedName = "laptops"},
            new Category{Name = "Смарт-часы", NormalizedName = "smart-clocks"},
            new Category{Name = "Наушники", NormalizedName = "headphones"}
        };

        var devices = new List<Device> {
            new Device{Name = "Samsung Galaxy S23 FE", 
                       Description = "8GB/128GB (Графит)",
                       Image = $"{baseUrl}/Images/samsung-galaxy-s23fe.jpg", 
                       Price = 1799,
                       Category = categories[0]},

            new Device{Name = "iPhone 15 Pro", 
                       Description = "8GB/128GB (Синий титан)",
                       Image = $"{baseUrl}/Images/iphone-15.jpg", 
                       Price =4099,
                       Category = categories[0]},

            new Device{Name = "Xiaomi Redmi Pad Pro", 
                       Description = "6GB/128GB (Графитовый серый)",
                       Image = $"{baseUrl}/Images/redmi-pad-pro.jpg", 
                       Price = 999,
                       Category = categories[1]},

            new Device{Name = "Samsung Galaxy Tab S9", 
                       Description = "12GB/256GB (Бежевый)",
                       Image = $"{baseUrl}/Images/samsung-galaxy-tab-s9.jpg", 
                       Price = 2999,
                       Category = categories[1]},

            new Device{Name = "ASUS TUF Gaming A15", 
                       Description = "AMD Ryzen 7, RTX 3050, 16GB RAM, 512GB SSD",
                       Image = $"{baseUrl}/Images/asus-tuf-a15.jpg", 
                       Price = 3799,
                       Category = categories[2]},
                    
            new Device{Name = "Apple MacBook Pro 14\"", 
                       Description = "M3 Max, 36GB RAM, 1024GB SSD",
                       Image = $"{baseUrl}/Images/macbook-pro.jpg", 
                       Price = 15233,
                       Category = categories[2]},

            new Device{Name = "HUAWEI Watch GT 4", 
                       Description = "Серебристый корпус, зеленый ремешек",
                       Image = $"{baseUrl}/Images/huawei-watch-gt4.jpg", 
                       Price = 499,
                       Category = categories[3]},

            new Device{Name = "Sony WH-1000XM4 ", 
                       Description = "Беспроводные наушники с микрофоном",
                       Image = $"{baseUrl}/Images/sony-wh1000.jpg", 
                       Price = 999,
                       Category = categories[4]},
        };

        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Categories.AddRangeAsync(categories);
        await dbContext.Devices.AddRangeAsync(devices);
        await dbContext.SaveChangesAsync();
    }
}
