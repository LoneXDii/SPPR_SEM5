﻿namespace WEB_253505_PAVLOVICH.UI.Services.Authentication;

public interface ITokenAccessor
{
    Task<string> GetAccessTokenAsync();
    Task SetAuthorizationHeaderAsync(HttpClient httpClient);
}
