using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.Domain.Models;

namespace WEB_253505_PAVLOVICH.API.Services.CategoryService;

public interface ICategoryService
{
    public Task<ResponseData<List<Category>>> GetCategoryListAsync();
}
