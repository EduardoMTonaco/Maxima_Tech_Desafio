using Blazored.LocalStorage;
using Maxima_Tech_Web.Class.Login.Model;
using Maxima_Tech_Web.Class.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Maxima_Tech_Web.Components.Pages.Login
{
    public partial class LoginPage : LoginMainPage
    {
        protected HttpContextApi _httpContextApi;
        [Inject]
        protected HttpContextApi HttpContextApi
        {
            get => _httpContextApi;
            set => _httpContextApi = value;
        }
        protected NavigationManager _navigation;
        [Inject]
        protected NavigationManager Navigation
        {
            get => _navigation;
            set => _navigation = value;
        }
        protected HttpClient _httpClient;
        [Inject]
        protected HttpClient HttpClient
        {
            get => _httpClient;
            set => _httpClient = value;
        }
        private LoginModel _loginModel { get; set; }

        protected override void OnInitialized()
        {
            _loginModel = new LoginModel();
            base.OnInitialized();
        }
        private string Message = "";
        private bool showMessageBox = false;
        private async Task Login()
        {
            try
            {
                var json = JsonSerializer.Serialize(_loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var requestTask = HttpClient.PostAsync($"{_httpContextApi.GetApiUrl()}/api/Login", content);
                var delayTask = Task.Delay(TimeSpan.FromSeconds(10));

                var completedTask = await Task.WhenAny(requestTask, delayTask);

                if (completedTask == delayTask)
                {
                    Message = "A requisição demorou mais de 10 segundos";
                    showMessageBox = true;
                }
                else
                {
                    var response = await requestTask;
                    if (response.IsSuccessStatusCode)
                    {
                        var token = await response.Content.ReadAsStringAsync();
                        await LocalStorage.SetItemAsync("token", token);
                        Navigation.NavigateTo("/", true, true);
                        IsLogged = true;
                    }
                    else
                    {
                        IsLogged = false;
                        Message = "Login Falhou. Entre em contato com o suporte." ;
                        showMessageBox = true;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                IsLogged = false;
                Message = ex.Message;
                showMessageBox = true;
            }
            showMessageBox = true;
        }
        private void CloseMessageBox()
        {
            showMessageBox = false;
        }
    }
}
