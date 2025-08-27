using Maxima_Tech_API.Class.DBHandler;
using Maxima_Tech_API.Class.DTO.Login;
using Maxima_Tech_API.Class.Security;
using Maxima_Tech_API.Controllers.Usuarios.Parameter;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace Maxima_Tech_API.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        public UsuarioController( ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "UsuariosInsert")]
        public IActionResult Register(UsuarioParameter userRegister)
        {
            
            try
            {
                if(string.IsNullOrEmpty(userRegister.Username))
                {
                    return BadRequest("O UserName não pode estar vazio.");
                }
                else if (string.IsNullOrEmpty(userRegister.Password))
                {
                    return BadRequest("O Password não pode estar vazio.");
                }
                else if (string.IsNullOrEmpty(userRegister.Email))
                {
                    return BadRequest("O Email não pode estar vazio.");
                }
                else if (string.IsNullOrEmpty(userRegister.NomeCompleto))
                {
                    return BadRequest("O NomeCompleto não pode estar vazio.");
                }
                else if (userRegister.Password.Length < 8)
                {
                    return BadRequest("O Password deve possuir no minimo 8 digitos.");
                }
                UsuariosDTO usuarioDTO = new UsuariosDTO();
                usuarioDTO.Username = userRegister.Username;
                usuarioDTO.EncyptPassword = new PasswordHelper().EncryptPassword(userRegister.Password);
                usuarioDTO.Email = userRegister.Email;
                usuarioDTO.NomeCompleto = userRegister.NomeCompleto;
                CollectiveUsuariosDTO collectiveUsuarios = new CollectiveUsuariosDTO();
                collectiveUsuarios.Insert(usuarioDTO);
                usuarioDTO.ResetPassword();
                return Ok(usuarioDTO);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                {
                    string delimiter = "produtos.";
                    int index = ex.Message.IndexOf(delimiter);
                    string field = ex.Message.Substring(index + delimiter.Length);
                    return BadRequest($"O {field} informado já está em uso. Por favor, informe um {field} único.");
                }
                else if (ex.Message.Contains("a foreign key constraint fails"))
                {
                    string delimiter = "REFERENCES `";
                    int index = ex.Message.IndexOf(delimiter);
                    string field = ex.Message.Substring(index + delimiter.Length);
                    field = field.Replace("\'", "");
                    return BadRequest($"O {field} informado não existe. Por favor, consulte a lista de {field} válido e tente novamente.");
                }
                return BadRequest("Erro inexperado. Entre em contato com o suporte.");
            }
            catch (Exception)
            {
                return BadRequest("Erro inexperado. Entre em contato com o suporte.");
            }
        }
    }
}
