using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.Domain.Models;
using WEB_253505_PAVLOVICH.UI.Services.CategoryService;

namespace WEB_253505_PAVLOVICH.UI.Services.DeviceService;

public class MemoryDeviceService : IDeviceService
{
    List<Device> _devices = new();
    List<Category> _categories;

    public MemoryDeviceService(ICategoryService categoryService)
    {
        _categories = categoryService.GetCategoryListAsync().Result.Data!;
        SetupData();
    }

    private void SetupData()
    {
        _devices = new List<Device> {
            new Device{Id = 1, Name = "Samsung Galaxy S23 FE", Description = "8GB/128GB (Графит)",
                       Image = "Images/samsung-galaxy-s23fe.jpg", Price = 1799,
                       Category = _categories.Find(c=>c.NormalizedName.Equals("smartphones"))},

            new Device{Id = 2, Name = "iPhone 15 Pro", Description = "8GB/128GB (Синий титан)",
                       Image = "Images/iphone-15.jpg", Price =4099,
                       Category = _categories.Find(c=>c.NormalizedName.Equals("smartphones"))},

            new Device{Id = 3, Name = "Xiaomi Redmi Pad Pro", Description = "6GB/128GB (Графитовый серый)",
                       Image = "Images/redmi-pad-pro.jpg", Price = 999,
                       Category = _categories.Find(c=>c.NormalizedName.Equals("tablets"))},

            new Device{Id = 4, Name = "Samsung Galaxy Tab S9", Description = "12GB/256GB (Бежевый)",
                       Image = "Images/samsung-galaxy-tab-s9.jpg", Price = 2999,
                       Category = _categories.Find(c=>c.NormalizedName.Equals("tablets"))},

            new Device{Id = 5, Name = "ASUS TUF Gaming A15", Description = "AMD Ryzen 7, RTX 3050, 16GB RAM, 512GB SSD",
                       Image = "Images/asus-tuf-a15.jpg", Price = 3799,
                       Category = _categories.Find(c=>c.NormalizedName.Equals("laptops"))},

            new Device{Id = 6, Name = "Apple MacBook Pro 14\"", Description = "M3 Max, 36GB RAM, 1024GB SSD",
                       Image = "Images/macbook-pro.jpg", Price = 15233,
                       Category = _categories.Find(c=>c.NormalizedName.Equals("laptops"))},

            new Device{Id = 7, Name = "HUAWEI Watch GT 4", Description = "Серебристый корпус, зеленый ремешек",
                       Image = "Images/huawei-watch-gt4.jpg", Price = 499,
                       Category = _categories.Find(c=>c.NormalizedName.Equals("smart-clocks"))},

            new Device{Id = 8, Name = "Sony WH-1000XM4 ", Description = "Черные беспроводные наушники с микрофоном",
                       Image = "Images/sony-wh1000.jpg", Price = 999,
                       Category = _categories.Find(c=>c.NormalizedName.Equals("headphones"))},
        };
    }


    public Task<ResponseData<Device>> CreateDeviceAsync(Device device, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDeviceAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<Device>> GetDeviceByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<ProductListModel<Device>>> GetDeviceListAsync(string? categoryNormalizedName, int pageNo = 1)
    {
        var items = _devices
                        .Where(p => categoryNormalizedName is null || p.Category.NormalizedName.Equals(categoryNormalizedName))
                        .ToList();
        //total pages and current page logick

        var pagedItems = new ProductListModel<Device> 
        { 
            Items = items,
            CurrentPage = pageNo,
            TotalPages = pageNo //temp
        };

        var result = ResponseData<ProductListModel<Device>>.Success(pagedItems);
        return Task.FromResult(result);
    }

    public Task UpdateDeviceAsync(int id, Device device, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }
}
