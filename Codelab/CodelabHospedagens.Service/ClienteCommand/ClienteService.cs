using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Domain.ClienteAggregate;

namespace CodelabHospedagens.Service.ClienteCommand
{
    public class ClienteService : IClienteService
    {
        private readonly IRepository<Cliente> _repository;
        private readonly IConversor<Cliente, ClienteDto> _conversor;
        private readonly IUnitOfWork _transaction;

        public ClienteService(IRepository<Cliente> repository, IConversor<Cliente, ClienteDto> conversor, IUnitOfWork transaction)
        {
            _repository = repository;
            _conversor = conversor;
            _transaction = transaction;
        }

        public async Task<RespostaGenerica> InserirClienteAsync(ClienteDto clienteDto)
        {
            try
            {
                _transaction.BeginTransaction();

                var cliente = _conversor.Converter(clienteDto);
                await _repository.InserirAsync(cliente);

                _transaction.Commit();

                return new RespostaGenerica("Cliente inserido com sucesso.", true);
            }
            catch
            {
                _transaction.Rollback();

                return new RespostaGenerica("Não foi possível inserir o cliente.", false);
            }
        }

        public async Task<RespostaGenerica> ObterClienteAsync(Guid id)
        {
            try
            {
                _transaction.BeginTransaction();

                var cliente = await _repository.ObterPorIdAsync(id);
                var clienteDto = _conversor.Converter(cliente);

                _transaction.Commit();

                return new RespostaGenerica("Consulta realizada com sucesso.", true, clienteDto);
            }
            catch
            {
                _transaction.Rollback();

                return new RespostaGenerica("Não foi possível realizar a consulta.", false);
            }
        }

        public async Task<RespostaGenerica> ObterTodosClientesAsync()
        {
            try
            {
                _transaction.BeginTransaction();

                var clientes = await _repository.ObterTodosAsync();

                var clientesDto = _conversor.ConverterLista(clientes);

                _transaction.Commit();

                return new RespostaGenerica("Consulta realizada com sucesso.", true, clientesDto);
            }
            catch
            {
                _transaction.Rollback();

                return new RespostaGenerica("Ocorreu um erro interno ao processar a consulta.", false);
            }
        }

        public RespostaGenerica RemoverCliente(Guid id)
        {
            try
            {
                _transaction.BeginTransaction();

                _repository.Remover(id);

                _transaction.Commit();

                return new RespostaGenerica("Registro removido com sucesso.", true);
            }
            catch
            {
                _transaction.Rollback();

                return new RespostaGenerica("Ocorreu um erro interno ao tentar remover o registro.", false);
            }
        }        
    }
}