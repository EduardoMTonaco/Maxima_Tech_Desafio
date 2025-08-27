using System.Text.Json.Serialization;

namespace Maxima_Tech_Web.Class.Login.Model
{
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
