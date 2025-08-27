using System.ComponentModel.DataAnnotations;

namespace Maxima_Tech_API.Controllers.Usuarios.Parameter
{
    public class UsuarioParameter
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password{ get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string NomeCompleto { get; set; }
    }
}
