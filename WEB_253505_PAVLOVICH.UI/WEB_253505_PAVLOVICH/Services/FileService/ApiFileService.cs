﻿
using WEB_253505_PAVLOVICH.UI.Services.Authentication;
using WEB_253505_PAVLOVICH.UI.Services.DeviceService;

namespace WEB_253505_PAVLOVICH.UI.Services.FileService;

public class ApiFileService : IFileService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiFileService> _logger;
    private readonly ITokenAccessor _tokenAccessor;

    public ApiFileService(HttpClient httpClient, ILogger<ApiFileService> logger,
                          ITokenAccessor tokenAccessor)
    {
        _httpClient = httpClient;
        _logger = logger;
        _tokenAccessor = tokenAccessor;
    }

    public async Task<string> SaveFileAsync(IFormFile formFile)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post
        };

        var extension = Path.GetExtension(formFile.FileName);
        var newName = Path.ChangeExtension(Path.GetRandomFileName(), extension);

        var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(formFile.OpenReadStream());
        content.Add(streamContent, "file", newName);

        request.Content = content;

        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        _logger.LogError($"-----> File not saved. Error:{response.StatusCode.ToString()}");
        return string.Empty;
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var uri = new Uri(_httpClient.BaseAddress?.AbsoluteUri + $"/{fileName.Split('/').Last()}");

        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
        var response = await _httpClient.DeleteAsync(uri);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"-----> File not deleted. Error:{response.StatusCode.ToString()}");
        }
    }
}
