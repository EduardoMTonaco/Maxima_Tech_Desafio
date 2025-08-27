using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Maxima_Tech_Web.Components
{
    public class LoginMainPage : LayoutComponentBase
    {
        protected HttpClient _httpClient;
        [Inject]
        protected HttpClient HttpClient
        {
            get => _httpClient;
            set => _httpClient = value;
        }
        protected ILocalStorageService _localStorage;
        [Inject]
        protected ILocalStorageService LocalStorage
        {
            get => _localStorage;
            set => _localStorage = value;
        }
        protected static bool IsLogged;
    }
}
