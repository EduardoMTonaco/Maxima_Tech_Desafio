using Blazored.LocalStorage;
using Maxima_Tech_Web.Class.DTO;
using Maxima_Tech_Web.Class.DTO.Produtos;
using Maxima_Tech_Web.Class.Login;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Text.Json;

namespace Maxima_Tech_Web.Components.Pages.Produtos
{
    public partial class ListProdutos : BasePage
    {
        private ConfirmModal confirmModal;
        private string Message = "";
        private bool showMessageBox = false;

        public IList<ProdutosDTO> Produtos { get; set; } = new List<ProdutosDTO>();
        protected string TXT_Id { get; set; } = "";
        protected string TXT_Nome { get; set; } = "";
        protected string TXT_Descricao { get; set; } = "";
        protected string TXT_Preco { get; set; } = "";
        private bool grayRow = false;
        private bool isOpenConfirmModal = false;
        protected string ProductId { get; set; } = "";
        private List<DepartamentosDTO> Departamentos { get; set; } = new List<DepartamentosDTO>();
        private int SelectedDepartamentoId { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                AuthService authService = new AuthService(HttpClient, LocalStorage);
                HttpClient = await authService.GetHttpClientAsync();
                var response = await HttpClient.GetAsync($"{_httpContextApi.GetApiUrl()}/api/Departamentos");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var departamentos = JsonSerializer.Deserialize<List<DepartamentosDTO>>(json);
                    if (departamentos != null)
                    {
                        Departamentos = departamentos;
                        StateHasChanged();
                    }
                }
            }
        }

        private async Task BTN_Search()
        {
            Produtos.Clear();
            AuthService authService = new AuthService(HttpClient, LocalStorage);
            HttpClient = await authService.GetHttpClientAsync();
            string preco = TXT_Preco.Replace(",", ".");
            var queryParams = new Dictionary<string, string>
            {
                {"Id", TXT_Id},
                {"Nome", TXT_Nome},
                {"Descricao", TXT_Descricao},
                {"DepartamentoId",SelectedDepartamentoId > 0 ? SelectedDepartamentoId.ToString() : ""} ,
                {"Preco", preco}
            };
            var queryString = string.Join("&", queryParams.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));
            var response = await _httpClient.GetAsync($"{_httpContextApi.GetApiUrl()}/api/Produtos?{queryString}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var produtos = JsonSerializer.Deserialize<List<ProdutosDTO>>(json);
                if (produtos != null)
                {
                    Produtos = produtos;
                }
            }

        }
        private void BTN_Delete(string id)
        {

            isOpenConfirmModal = false;
            isOpenConfirmModal = true;
            ProductId = id;
        }

        private bool GrayColumn()
        {
            if (grayRow)
            {
                grayRow = false;
                return true;
            }
            else
            {
                grayRow = true;
                return false;
            }
        }
        protected void BTN_DeleteModal(string product)
        {
            isOpenConfirmModal = false;
            isOpenConfirmModal = true;
            ProductId = product;
        }
        private async Task DeleteProduct()
        {

            AuthService authService = new AuthService(HttpClient, LocalStorage);
            HttpClient = await authService.GetHttpClientAsync();
            var response = await _httpClient.DeleteAsync($"{_httpContextApi.GetApiUrl()}/api/Produtos?id={ProductId}");
            if (response.IsSuccessStatusCode)
            {
                Message = $"Exclusão realizada com sucesso!";
            }
            else
            {
                Message = $"Exclusão não realizada!";
            }
            isOpenConfirmModal = false;
            showMessageBox = true;
            await BTN_Search();
        }

        private void CloseMessageBox()
        {
            showMessageBox = false;
        }
        private void HandleDepartamentoChange(ChangeEventArgs e)
        {
            SelectedDepartamentoId = int.Parse(e.Value.ToString());
        }
    }
}
