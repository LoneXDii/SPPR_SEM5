using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Text.Json;
using WEB_253505_PAVLOVICH.Domain.Entities;

namespace WEB_253505_PAVLOVICH.BlazorWasm.Services;

internal class DataService : IDataService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly string _pageSize;

    public DataService(HttpClient httpClient, IConfiguration configuration, IAccessTokenProvider accessTokenProvider)
    {
        _httpClient = httpClient;
        _pageSize = configuration.GetSection("ItemsPerPage").Value;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _accessTokenProvider = accessTokenProvider;
    }

    public List<Category> Categories { get; set; }
    public List<Device> Devices { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public Category SelectedCategory { get; set; } = null;

    public event Action DataLoaded;

    public Task GetCategoryListAsync()
    {
        throw new NotImplementedException();
    }

    public Task GetProductListAsync(int pageNo = 1)
    {
        throw new NotImplementedException();
    }
}
