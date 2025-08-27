using System.ComponentModel.DataAnnotations;

namespace Maxima_Tech_API.Controllers.Produtos.Parameter
{
    public class ProdutoInsert
    {
        [Required]
        public string Nome {  get; set; }
        [Required]
        public string Descricao { get; set; }
        public int DepartamentoId { get; set; }
        [Required]
        public decimal Preco { get; set; }
    }
}
