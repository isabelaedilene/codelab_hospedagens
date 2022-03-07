using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Service.ClienteCommand;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace CodelabHospedagens.Controllers
{
    [ApiController]
    [Route("/cliente")]
    [Produces(MediaTypeNames.Application.Json)]

    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(RespostaGenerica), 200)]
        [ProducesResponseType(typeof(RespostaGenerica), 500)]
        public async Task<ActionResult> ObterCliente(Guid id)
        {
            var resposta = await _clienteService.ObterClienteAsync(id);

            if (resposta.Sucesso)
            {
                return Ok(resposta);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, resposta);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(RespostaGenerica), 200)]
        [ProducesResponseType(typeof(RespostaGenerica), 500)]
        public async Task<ActionResult> ObterTodosClientes()
        {
            var resposta = await _clienteService.ObterTodosClientesAsync();

            if (resposta.Sucesso)
            {
                return Ok(resposta);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, resposta);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(RespostaGenerica), 200)]
        [ProducesResponseType(typeof(RespostaGenerica), 500)]
        public async Task<ActionResult> InserirCliente([FromBody] ClienteDto clienteDto)
        {
            var resposta = await _clienteService.InserirClienteAsync(clienteDto);

            if (resposta.Sucesso)
            {
                return Ok(resposta);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, resposta);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(RespostaGenerica), 200)]
        [ProducesResponseType(typeof(RespostaGenerica), 500)]
        public ActionResult RemoverCliente(Guid id)
        {
            var resposta = _clienteService.RemoverCliente(id);

            if (resposta.Sucesso)
            {
                return Ok(resposta);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, resposta);
            }
        }
    }
}