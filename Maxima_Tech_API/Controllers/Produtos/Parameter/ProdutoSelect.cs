namespace Maxima_Tech_API.Controllers.Produtos.Parameter
{
    public class ProdutoSelect
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int DepartamentoId { get; set; }
        public decimal Preco { get; set; }
        public string? DepartamentoNome { get; set; }
    }
}
