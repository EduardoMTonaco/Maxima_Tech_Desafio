using Blazored.LocalStorage;
using Maxima_Tech_Web.Class.Login.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Maxima_Tech_Web.Components.Layout
{
    public partial class Header : LoginMainPage
    {
        private async Task Logout()
        {
            IsLogged = false;
            await LocalStorage.RemoveItemAsync("token");
            IsLogged = false;
            Navigation.NavigateTo("/", true);
        }
    }
}
