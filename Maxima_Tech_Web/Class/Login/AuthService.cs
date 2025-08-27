using Blazored.LocalStorage;
using Maxima_Tech_Web.Class.Login.Model;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Maxima_Tech_Web.Class.Login
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<HttpClient> GetHttpClientAsync()
        {
            string token = await _localStorageService.GetItemAsync<string>("token");
            HttpClient httpClient = new HttpClient();
            if (!string.IsNullOrEmpty(token))
            {
                TokenResponse tokenStorage = JsonSerializer.Deserialize<TokenResponse>(token);              
                if (tokenStorage != null && !string.IsNullOrEmpty(tokenStorage.Token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenStorage.Token);
                }
            }
            return httpClient;
        }
    }
}
