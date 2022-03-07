using CodelabHospedagens.Domain.SeedWork;

namespace CodelabHospedagens.Service.HospedagemCommand
{
    public interface IHospedagemService
    {
        public Task<RespostaGenerica> ObterHospedagensCliente(Guid id);
        public Task<RespostaGenerica> ReservarChale(HospedagemDto hospedagemDto);
    }
}
