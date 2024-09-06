using System.Text;
using System.Text.Json;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.Domain.Models;

namespace WEB_253505_PAVLOVICH.UI.Services.CategoryService;

public class ApiCategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiCategoryService> _logger;
    private readonly JsonSerializerOptions _serializerOptions;

    public ApiCategoryService(HttpClient httpClient, ILogger<ApiCategoryService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;

        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        var uriString = new StringBuilder($"{_httpClient.BaseAddress?.AbsoluteUri}Categories/");
        var response = await _httpClient.GetAsync(new Uri(uriString.ToString()));

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"-----> Error in acessing server. Error:{response.StatusCode.ToString()}");
            return ResponseData<List<Category>>.Error($"Error in acessing server. Error: {response.StatusCode}");
        }

        try
        {
            return await response.Content
                                 .ReadFromJsonAsync<ResponseData<List<Category>>>(_serializerOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError($"-----> Error: {ex.Message}");
            return ResponseData<List<Category>>.Error($"Error: {ex.Message}");
        }
    }
}
