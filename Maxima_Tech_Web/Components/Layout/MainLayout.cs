using Blazored.LocalStorage;
using Maxima_Tech_Web.Class.Login;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Maxima_Tech_Web.Components.Layout
{
    public partial class MainLayout : LoginMainPage
    {
       
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            CheckLoggin();
        }
        private void CheckLoggin()
        {
            AuthService authService = new AuthService(HttpClient, LocalStorage);
            HttpClient = authService.GetHttpClientAsync().Result;
            var response = HttpClient.GetAsync("https://localhost:7020/api/Logged").Result;
            if (response.IsSuccessStatusCode)
            {
                IsLogged = true;
            }
        }
    }
}
