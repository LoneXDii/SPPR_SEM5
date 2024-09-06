using Microsoft.EntityFrameworkCore;
using WEB_253505_PAVLOVICH.API.Data;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.Domain.Models;

namespace WEB_253505_PAVLOVICH.API.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _dbContext;

    public CategoryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        var categories = await _dbContext.Categories.ToListAsync();
        if (!categories.Any() || categories is null)
        {
            return ResponseData<List<Category>>.Error("No categories in db");
        }
        return ResponseData<List<Category>>.Success(categories);
    }
}
