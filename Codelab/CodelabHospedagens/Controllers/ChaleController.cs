using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Service.ChaleCommand;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace CodelabHospedagens.Controllers
{
    [ApiController]
    [Route("/chale")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ChaleController : ControllerBase
    {
        private readonly IChaleService _chaleService;

        public ChaleController(IChaleService chaleService)
        {
            _chaleService = chaleService;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(RespostaGenerica), 200)]
        [ProducesResponseType(typeof(RespostaGenerica), 500)]
        public async Task<ActionResult> ObterChale(Guid id)
        {
            var resposta = await _chaleService.ObterChaleAsync(id);

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
        public async Task<ActionResult> ObterTodosChales()
        {
            var resposta = await _chaleService.ObterTodosChalesAsync();

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
        public async Task<ActionResult> InserirChale([FromBody] ChaleDto chaleDto)
        {
            var resposta = await _chaleService.InserirChaleAsync(chaleDto);

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
        public ActionResult RemoverChale(Guid id)
        {
            var resposta = _chaleService.RemoverChale(id);

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
        [Route("disponiveis")]
        [ProducesResponseType(typeof(RespostaGenerica), 200)]
        [ProducesResponseType(typeof(RespostaGenerica), 500)]
        public async Task<ActionResult> ObterChalesDisponiveis([FromQuery] DateTime inicio, DateTime fim)
        {
            var resposta = await _chaleService.ObterChalesDisponiveis(inicio, fim);

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