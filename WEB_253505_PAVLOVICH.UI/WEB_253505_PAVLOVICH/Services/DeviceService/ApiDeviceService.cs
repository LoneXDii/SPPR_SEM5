using System.Text;
using System.Text.Json;
using WEB_253505_PAVLOVICH.Domain.Entities;
using WEB_253505_PAVLOVICH.Domain.Models;
using WEB_253505_PAVLOVICH.UI.Services.Authentication;
using WEB_253505_PAVLOVICH.UI.Services.FileService;

namespace WEB_253505_PAVLOVICH.UI.Services.DeviceService;

//Add list all async method
public class ApiDeviceService : IDeviceService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiDeviceService> _logger;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IFileService _fileService;
    private readonly ITokenAccessor _tokenAccessor;
    private readonly string _pageSize;

    public ApiDeviceService(HttpClient httpClient, IConfiguration configuration,
                            ILogger<ApiDeviceService> logger, IFileService fileService,
                            ITokenAccessor tokenAccessor)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _logger = logger;
        _pageSize = configuration.GetSection("ItemsPerPage").Value!;
        _fileService = fileService;
        _tokenAccessor = tokenAccessor;
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

        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
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
        _logger.LogError($"-----> Error in acessing devices list. Error:{response.StatusCode.ToString()}");
        return ResponseData<ProductListModel<Device>>.Error($"Error in acessing devices list. Error:{response.StatusCode.ToString()}");
    }


    public async Task<ResponseData<Device>> CreateDeviceAsync(Device device, IFormFile? formFile)
    {
        device.Image = "Images/noimage.jpg";

        if (formFile != null)
        {
            var imageUrl = await _fileService.SaveFileAsync(formFile);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                device.Image = imageUrl;
            }
        }

        var uri = new Uri(_httpClient.BaseAddress?.AbsoluteUri + "Devices");
        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
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

    public async Task DeleteDeviceAsync(int id)
    {
        var uri = new Uri(_httpClient.BaseAddress?.AbsoluteUri + $"Devices/{id}");
        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
        var response = await _httpClient.DeleteAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"-----> Object not deleted. Error:{response.StatusCode.ToString()}");
        }
    }

    public async Task<ResponseData<Device>> GetDeviceByIdAsync(int id)
    {
        var uri = new Uri(_httpClient.BaseAddress?.AbsoluteUri + $"Devices/{id}");
        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
        var response = await _httpClient.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content
                                     .ReadFromJsonAsync<ResponseData<Device>>(_serializerOptions);

            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Error: {ex.Message}");
                return ResponseData<Device>.Error($"Error: {ex.Message}");
            }
        }
        _logger.LogError($"-----> Can't get device with id={id}. Error:{response.StatusCode.ToString()}");
        return ResponseData<Device>.Error($"Can't get device with id={id}. Error:{response.StatusCode.ToString()}");
    }

    public async Task UpdateDeviceAsync(int id, Device device, IFormFile? formFile)
    {
        if (formFile != null)
        {
            var imageUrl = await _fileService.SaveFileAsync(formFile);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                await _fileService.DeleteFileAsync(device.Image!);
                device.Image = imageUrl;
            }
        }
        var uri = new Uri(_httpClient.BaseAddress?.AbsoluteUri + $"Devices/{id}");
        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
        var response = await _httpClient.PutAsJsonAsync(uri, device);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"-----> Object not updated. Error:{response.StatusCode.ToString()}");
        }
    }
}