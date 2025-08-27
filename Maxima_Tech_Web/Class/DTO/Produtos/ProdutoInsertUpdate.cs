namespace Maxima_Tech_Web.Class.DTO.Produtos
{
    public class ProdutoInsertUpdate
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int DepartamentoId { get; set; }
        public decimal Preco { get; set; }
    }
}
