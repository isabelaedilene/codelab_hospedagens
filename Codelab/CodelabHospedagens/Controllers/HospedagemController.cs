using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Service.HospedagemCommand;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace CodelabHospedagens.Controllers
{
    [ApiController]
    [Route("/hospedagem")]
    [Produces(MediaTypeNames.Application.Json)]

    public class HospedagemController : ControllerBase
    {
        private readonly IHospedagemService _hospedagemService;

        public HospedagemController(IHospedagemService hospedagemService)
        {
            _hospedagemService = hospedagemService;
        }

        [HttpGet]
        [Route("cliente/{id}")]
        [ProducesResponseType(typeof(RespostaGenerica), 200)]
        [ProducesResponseType(typeof(RespostaGenerica), 500)]
        public async Task<ActionResult> ObterHospedagemCliente(Guid id)
        {
            var resposta = await _hospedagemService.ObterHospedagensCliente(id);

            if(resposta.Sucesso)
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
        public async Task<ActionResult> Reservar([FromBody] HospedagemDto hospedagemDto)
        {
            var resposta = await _hospedagemService.ReservarChale(hospedagemDto);

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