using System.ComponentModel.DataAnnotations;

namespace Maxima_Tech_API.Controllers.Usuarios.Parameter
{
    public class LoginParameter
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
