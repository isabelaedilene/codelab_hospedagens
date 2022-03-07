using CodelabHospedagens.Domain.SeedWork;

namespace CodelabHospedagens.Service.ClienteCommand
{
    public interface IClienteService
    {
        public Task<RespostaGenerica> ObterClienteAsync(Guid id);
        public Task<RespostaGenerica> ObterTodosClientesAsync();
        public Task<RespostaGenerica> InserirClienteAsync(ClienteDto clienteDto);
        public RespostaGenerica RemoverCliente(Guid id);
    }
}
