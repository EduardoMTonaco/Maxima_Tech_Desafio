using Maxima_Tech_Web.Class.DTO;
using Maxima_Tech_Web.Class.DTO.Produtos;
using Maxima_Tech_Web.Class.Login;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;

namespace Maxima_Tech_Web.Components.Pages.Produtos
{
    public partial class Produto : BasePage
    {
        [Parameter]
        [SupplyParameterFromQuery]
        public string Id { get; set; }
        protected string TXT_Id { get; set; } = "";
        protected string TXT_Nome { get; set; } = "";
        protected string TXT_Descricao { get; set; } = "";
        protected string TXT_Preco { get; set; } = "";
        [Parameter]
        public string ActionLabel { get; set; } = "Registrar";       
        [Parameter]
        public string SelectedDepartamentoId { get; set; } = "0";
        [Parameter]
        public string Title { get; set; } = "Registrar Produto";
        private string Message = "";
        private bool showMessageBox = false;
        private List<DepartamentosDTO> Departamentos { get; set; } = new List<DepartamentosDTO>();

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
                if (!string.IsNullOrEmpty(Id))
                {
                    response = await HttpClient.GetAsync($"{_httpContextApi.GetApiUrl()}/api/Produtos?id={Uri.EscapeDataString(Id)}");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var produtos = JsonSerializer.Deserialize<List<ProdutosDTO>>(json);
                        if (produtos != null && produtos.Count > 0)
                        {
                            ProdutosDTO produto = produtos.FirstOrDefault();
                            TXT_Id = produto.Id.ToString();
                            TXT_Nome = produto.Nome;
                            TXT_Descricao = produto.Descricao;
                            TXT_Preco = produto.Preco.ToString().Replace(".", ",");
                            SelectedDepartamentoId = produto.DepartamentoId.ToString();                          
                        }
                    }
                    ActionLabel = "Alterar";
                    Title = "Alterar Produto";
                }
                StateHasChanged();
            }
        }
        public async Task BTN_Action()
        {
            AuthService authService = new AuthService(HttpClient, LocalStorage);
            HttpClient = await authService.GetHttpClientAsync();
            ProdutoInsertUpdate produto = new ProdutoInsertUpdate();
            decimal preco = 0;
            if (string.IsNullOrEmpty(TXT_Nome))
            {
                Message = $"O campo nome não pode ficar vazio.";
                showMessageBox = true;
                return;
            }
            else if (string.IsNullOrEmpty(TXT_Descricao))
            {
                Message = $"O campo descrição não pode ficar vazio.";
                showMessageBox = true;
                return;
            }
            else if (string.IsNullOrEmpty(TXT_Preco))
            {
                Message = $"O campo preço não pode ficar vazio.";
                showMessageBox = true;
                return;
            }

            else if (!decimal.TryParse(TXT_Preco, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("pt-BR"), out preco))
            {
                Message = $"Preço invalido!";
                showMessageBox = true;
                return;
            }
            else if(int.Parse(SelectedDepartamentoId) <= 0)
            {
                Message = $"Por favor, selecionar um departamento.";
                showMessageBox = true;
                return;
            }
            produto.Nome = TXT_Nome;
            produto.Descricao = TXT_Descricao;
            produto.DepartamentoId = int.Parse(SelectedDepartamentoId);
            produto.Preco = preco;
            if (!string.IsNullOrEmpty(Id))
            {
                HttpResponseMessage response = await HttpClient.PutAsJsonAsync($"{_httpContextApi.GetApiUrl()}/api/produtos?id={Id}", produto);
                if (response.IsSuccessStatusCode)
                {

                    Message = $"Alteração realizada com sucesso!";
                }
                else
                {
                    Message = $"Alteração não realizada!";
                }
            }
            else
            {
                HttpResponseMessage response = await HttpClient.PostAsJsonAsync($"{_httpContextApi.GetApiUrl()}/api/produtos", produto);
                if (response.IsSuccessStatusCode)
                {

                    Message = $"Registro de produto realizado com sucesso!";
                    TXT_Nome = "";
                    TXT_Descricao = "";
                    SelectedDepartamentoId = "0";
                    TXT_Preco = "";
                    StateHasChanged();

                }
                else
                {
                    Message = $"Registro de produto não realizado!";
                }
            }
            showMessageBox = true;
        }
        private void CloseMessageBox()
        {
            showMessageBox = false;
        }
    }
}
