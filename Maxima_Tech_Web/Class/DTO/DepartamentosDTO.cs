using System.Text.Json.Serialization;

namespace Maxima_Tech_Web.Class.DTO
{
    public class DepartamentosDTO
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome  { get; set; }
    }
}
