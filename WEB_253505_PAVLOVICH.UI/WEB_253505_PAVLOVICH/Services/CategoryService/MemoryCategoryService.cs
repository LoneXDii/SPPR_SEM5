using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.Domain.Models;

namespace WEB_253505_PAVLOVICH.UI.Services.CategoryService;

public class MemoryCategoryService : ICategoryService
{
    public Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        var categories = new List<Category>
        {
            new Category{Id = 1, Name = "Смартфоны", NormalizedName = "smartphones"},
            new Category{Id = 2, Name = "Планшеты", NormalizedName = "tablets"},
            new Category{Id = 3, Name = "Ноутбуки", NormalizedName = "laptops"},
            new Category{Id = 4, Name = "Смарт-часы", NormalizedName = "smart-clocks"},
            new Category{Id = 5, Name = "Наушники", NormalizedName = "headphones"}
        };

        var result = ResponseData<List<Category>>.Success(categories);
        return Task.FromResult(result);
    }
}
