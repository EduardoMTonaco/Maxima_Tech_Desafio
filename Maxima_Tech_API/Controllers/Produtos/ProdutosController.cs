using MassTransit;
using Maxima_Tech_API.Class.DTO.Departamentos;
using Maxima_Tech_API.Class.DTO.Produtos;
using Maxima_Tech_API.Controllers.Produtos.Parameter;
using Maxima_Tech_Web.Components.Pages.Produtos;
using MaximaTech.Contracts.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxima_Tech_API.Controllers.Produtos
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProdutosController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public ProdutosController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet(Name = "ProdutosSelect")]
        public IActionResult ProdutosSelect([FromQuery] ProdutoSelect produto)
        {
            try
            {
                ProdutoDTO produtoDTO = new ProdutoDTO() { Status = true };
                if (produto.Id != null && !string.IsNullOrEmpty(produto.Id))
                {
                    Guid guid = Guid.Parse(produto.Id);
                    produtoDTO.Id = guid;
                }
                if (produto.Nome != null && !string.IsNullOrEmpty(produto.Nome))
                {
                    produtoDTO.Nome = produto.Nome;

                }
                if (produto.Descricao != null && !string.IsNullOrEmpty(produto.Descricao))
                {
                    produtoDTO.Descricao = produto.Descricao;

                }
                if (produto.DepartamentoId > 0)
                {
                    produtoDTO.DepartamentoId = produto.DepartamentoId;

                }
                if (produto.Preco > 0)
                {
                    produtoDTO.Preco = produto.Preco;

                }

                if (!string.IsNullOrEmpty(produto.DepartamentoNome))
                {
                    CollectiveDepartamentosDTO collectiveDepartamentosDTO = new CollectiveDepartamentosDTO();
                    DepartamentoDTO departamento = new DepartamentoDTO() { Nome = produto.DepartamentoNome };
                    departamento = collectiveDepartamentosDTO.ObjOne(departamento);
                    if (departamento != null)
                    {
                        if (departamento.Id > 0)
                        {
                            if (produto.DepartamentoId > 0 && departamento.Id != produto.DepartamentoId)
                            {
                                return NotFound("Nenhum resultado encontrado.");
                            }
                            produtoDTO.DepartamentoId = departamento.Id;
                        }
                        else
                        {
                            return NotFound("Nenhum resultado encontrado.");
                        }
                    }
                }


                CollectiveProdutosDTO produtos = new CollectiveProdutosDTO();
                produtos.MaxAmount = 100;
                List<ProdutoDTO> produtosList = produtos.ObjList(produtoDTO);

                if (produtosList.Count > 0)
                {
                    return new OkObjectResult(produtosList);
                }
                return NotFound("Nenhum resultado encontrado.");


            }
            catch (Exception)
            {
                return BadRequest("Erro inexperado. Entre em contato com o suporte.");
            }
        }
        [HttpPost(Name = "ProdutosInsert")]
        public IActionResult ProdutosInsert([FromBody] ProdutoInsert produto)
        {
            try
            {
                (bool flowControl, IActionResult value) = CheckProduto(produto);
                if (!flowControl)
                {
                    return value;
                }
                ProdutoDTO produtoDTO = new ProdutoDTO() { Nome = produto.Nome, Descricao = produto.Descricao, DepartamentoId = produto.DepartamentoId, Preco = produto.Preco };
                CollectiveProdutosDTO collectiveProdutosDTO = new CollectiveProdutosDTO();
                collectiveProdutosDTO.Insert(produtoDTO);
                ProdutoDTO produtoEvent = collectiveProdutosDTO.ObjOne(produtoDTO);
                var evento = new ProdutoCadastradoEvent
                {
                    Id = produtoEvent.Id,
                    Nome = produtoEvent.Nome,
                    DataCadastro = DateTime.UtcNow
                };
#if !DEBUG
_publishEndpoint.Publish(evento);
#endif

                return Created();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                {
                    string delimiter = "usuarios.";
                    int index = ex.Message.IndexOf(delimiter);
                    string field = ex.Message.Substring(index + delimiter.Length);
                    return BadRequest($"O {field} informado já está em uso. Por favor, informe um {field} diferente.");
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
        [HttpDelete(Name = "ProdutosDelete")]
        public IActionResult ProdutosDelete(Guid id)
        {
            try
            {
                ProdutoDTO produtoDTO = new ProdutoDTO() { Id = id, Status = false };
                CollectiveProdutosDTO collectiveProdutosDTO = new CollectiveProdutosDTO();
                collectiveProdutosDTO.Update(produtoDTO);

                return new OkObjectResult(new
                {
                    mensagem = "Produto deletado com sucesso",
                    produto = collectiveProdutosDTO.ObjOne(produtoDTO)
                });
            }
            catch (Exception)
            {
                return BadRequest("Erro inexperado. Entre em contato com o suporte.");
            }
        }
        [HttpPut(Name = "ProdutosUpdate")]
        public IActionResult ProdutosUpdate(Guid id, [FromBody] ProdutoUpdate produto)
        {
            try
            {
                if (id == Guid.Empty || id == null)
                {
                    return BadRequest("O ID do produto é obrigatório.");
                }
                ProdutoDTO produtoDTO = new ProdutoDTO();
                produtoDTO.Id = id;
                bool validUpdate = false;
                if (produto.Nome != null && !string.IsNullOrEmpty(produto.Nome))
                {
                    produtoDTO.Nome = produto.Nome;
                    validUpdate = true;
                }
                if (produto.Descricao != null && !string.IsNullOrEmpty(produto.Descricao))
                {
                    produtoDTO.Descricao = produto.Descricao;
                    validUpdate = true;
                }
                if (produto.DepartamentoId > 0)
                {
                    produtoDTO.DepartamentoId = produto.DepartamentoId;
                    validUpdate = true;
                }
                if (produto.Preco > 0)
                {
                    produtoDTO.Preco = produto.Preco;
                    validUpdate = true;
                }
                if (!validUpdate)
                {
                    return BadRequest("O produto tem que ter pelo menos um atributo para ser modificado.");
                }
                CollectiveProdutosDTO collectiveProdutosDTO = new CollectiveProdutosDTO();
                collectiveProdutosDTO.Update(produtoDTO);
                return new OkObjectResult(produto);
            }
            catch (Exception)
            {
                return BadRequest("Erro inexperado. Entre em contato com o suporte.");
            }
        }
        private (bool flowControl, IActionResult value) CheckProduto(ProdutoInsert produto)
        {
            if (string.IsNullOrEmpty(produto.Nome))
            {
                return (flowControl: false, value: BadRequest("O código do produto não pode estar vazio."));
            }
            else if (produto.Nome.Length > 40)
            {
                return (flowControl: false, value: BadRequest("O Codigo do produto deve ser um numero até 40 digitos."));
            }
            else if (string.IsNullOrEmpty(produto.Descricao))
            {
                return (flowControl: false, value: BadRequest("A descrição do produto não pode estar vazio."));
            }
            else if (produto.Descricao.Length > 150)
            {
                return (flowControl: false, value: BadRequest("A descrição do produto tem que ser menor do que 150 caracteres."));
            }
            else if (produto.DepartamentoId <= 0)
            {
                return (flowControl: false, value: BadRequest("O ID do departamento deve ser maior do que zero."));
            }
            else if (produto.Preco <= 0)
            {
                return (flowControl: false, value: BadRequest("O Preco deve ser maior do que zero."));
            }

            return (flowControl: true, value: null);
        }
    }
}
