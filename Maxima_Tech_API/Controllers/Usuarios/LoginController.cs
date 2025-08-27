using Maxima_Tech_API.Class.DBHandler;
using Maxima_Tech_API.Class.DTO.Login;
using Maxima_Tech_API.Class.Security;
using Maxima_Tech_API.Controllers.Usuarios.Parameter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace Maxima_Tech_API.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<LoginController> _logger;
        public LoginController(AuthService authService, ILogger<LoginController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost(Name = "Login")]
        public IActionResult Login(LoginParameter usuario)
        {
            try
            {
                UsuariosDTO user = new UsuariosDTO();
                user.Username = usuario.Username;

                CollectiveUsuariosDTO collectiveUsuarios = new CollectiveUsuariosDTO();
                UsuariosDTO usuariosDTO = collectiveUsuarios.ObjOne(user);

                PasswordHelper passwordHelper = new PasswordHelper();
                if (string.IsNullOrEmpty(usuariosDTO.EncyptPassword))
                {
                    return NotFound("Usuário inválido");
                }
                else if (usuariosDTO != null && passwordHelper.CheckPassword(usuario.Password, usuariosDTO.EncyptPassword))
                {
                    var token = _authService.GerarToken(usuario);
                    return Ok(new { token });
                }
                return Unauthorized("Credenciais inválidas");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro inexperado. Entre em contato com o suporte." + ex.Message);
            }
        }
        [HttpGet(Name = "Logged")]
        [Authorize]
        public IActionResult Logged()
        {
            return Ok();
        }
    }
}
