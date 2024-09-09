using System.Text;
using System.Text.Json;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.Domain.Models;

namespace WEB_253505_PAVLOVICH.UI.Services.DeviceService;

//Add list all async method
public class ApiDeviceService : IDeviceService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiDeviceService> _logger;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly string _pageSize;

    public ApiDeviceService(HttpClient httpClient, IConfiguration configuration,
                            ILogger<ApiDeviceService> logger)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _logger = logger;
        _pageSize = configuration.GetSection("ItemsPerPage").Value!;
    }

    public async Task<ResponseData<ProductListModel<Device>>> GetDeviceListAsync(string? categoryNormalizedName, int pageNo = 1)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress?.AbsoluteUri}devices/");
        if (categoryNormalizedName != null)
        {
            urlString.Append($"{categoryNormalizedName}/");
        }
        if (pageNo > 1)
        {
            urlString.Append(QueryString.Create("pageno", pageNo.ToString()));
        };
        if (!_pageSize.Equals("3"))
        {
            urlString.Append(QueryString.Create("pageSize", _pageSize));
        }

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content
                                     .ReadFromJsonAsync<ResponseData<ProductListModel<Device>>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Error: {ex.Message}");
                return ResponseData<ProductListModel<Device>>.Error($"Error: {ex.Message}");
            }
        }
        _logger.LogError($"-----> Error in acessing server. Error:{response.StatusCode.ToString()}");
        return ResponseData<ProductListModel<Device>>.Error($"Error in acessing server. Error:{response.StatusCode.ToString()}");
    }


    public async Task<ResponseData<Device>> CreateDeviceAsync(Device device, IFormFile? formFile)
    {
        var uri = new Uri(_httpClient.BaseAddress?.AbsoluteUri + "Devices");
        var response = await _httpClient.PostAsJsonAsync(uri, device, _serializerOptions);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content
                                     .ReadFromJsonAsync<ResponseData<Device>>(_serializerOptions);
            return data;
        }
        _logger.LogError($"-----> Object not created. Error:{ response.StatusCode.ToString()}");
        return ResponseData<Device>.Error($"Object not created. Error:{response.StatusCode.ToString()}");
    }

    public Task DeleteDeviceAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<Device>> GetDeviceByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateDeviceAsync(int id, Device device, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }
}
