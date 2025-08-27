using Maxima_Tech_API.Class.DTO.Departamentos;
using Maxima_Tech_API.Class.Security;
using Maxima_Tech_API.Controllers.Departamentos.Parameter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Maxima_Tech_API.Controllers.Departamentos
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartamentosController : ControllerBase
    {
        private readonly ILogger<DepartamentosController> _logger;

        public DepartamentosController( ILogger<DepartamentosController> logger)
        {
            _logger = logger;           
        }
        [HttpGet(Name = "DepartamentosSelect")]
        public IActionResult DepartamentosSelect([FromQuery] DepartamentosSelect departamento)
        {
            try
            {
                DepartamentoDTO departamentoDTO = new DepartamentoDTO();
                if (departamento != null)
                {
                    if (departamento.Id > 0)
                    {
                        departamentoDTO.Id = departamento.Id;
                    }
                    if (!string.IsNullOrEmpty(departamento.Nome))
                    {
                        departamentoDTO.Nome = departamento.Nome;
                    }
                }

                CollectiveDepartamentosDTO departamentos = new CollectiveDepartamentosDTO();
                List<DepartamentoDTO> departamentList = departamentos.ObjList(departamentoDTO);
                if (departamentList.Count > 0)
                {
                    return new OkObjectResult(departamentList);
                }
                return NotFound("Nenhum resultado encontrado.");
            }
            catch (Exception)
            {
                return BadRequest("Erro inexperado. Entre em contato com o suporte.");
            }
        }

    }
}
