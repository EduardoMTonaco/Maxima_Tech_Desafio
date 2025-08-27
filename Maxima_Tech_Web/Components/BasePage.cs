using Blazored.LocalStorage;
using Maxima_Tech_Web.Class.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Maxima_Tech_Web.Components
{
    public class BasePage : ComponentBase
    {
        protected HttpContextApi _httpContextApi;
        [Inject]
        protected HttpContextApi HttpContextApi
        {
            get => _httpContextApi;
            set => _httpContextApi = value;
        }

        protected IJSRuntime _jsRuntime;
        [Inject]
        protected IJSRuntime JS
        {
            get => _jsRuntime;
            set => _jsRuntime = value;
        }
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
    }
}
