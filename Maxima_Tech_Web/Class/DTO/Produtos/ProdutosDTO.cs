using System.Text.Json.Serialization;

namespace Maxima_Tech_Web.Class.DTO.Produtos
{
    public class ProdutosDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("departamentoId")]
        public int DepartamentoId { get; set; }

        [JsonPropertyName("preco")]
        public decimal Preco { get; set; }

        [JsonPropertyName("Status")]
        public bool Status { get; set; } = true;

        [JsonPropertyName("departamento")]
        public DepartamentosDTO Departamento { get; set; }

    }
}
