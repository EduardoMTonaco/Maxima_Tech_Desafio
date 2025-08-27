namespace Maxima_Tech_API.Controllers.Produtos.Parameter
{
    public class ProdutoUpdate
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int DepartamentoId { get; set; }
        public decimal Preco { get; set; }
    }
}
