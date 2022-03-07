using CodelabHospedagens.Domain.ChaleAggregate;
using CodelabHospedagens.Domain.SeedWork;

namespace CodelabHospedagens.Service.ChaleCommand
{
    public class ChaleService : IChaleService
    {
        private readonly IRepository<Chale> _repository;
        private readonly IChaleRepository _chaleRepository;
        private readonly IConversor<Chale, ChaleDto> _conversor;
        private readonly IUnitOfWork _transaction;

        public ChaleService(IRepository<Chale> repository, IChaleRepository chaleRepository, IConversor<Chale, ChaleDto> conversor, IUnitOfWork transaction)
        {
            _repository = repository;
            _chaleRepository = chaleRepository;
            _conversor = conversor;
            _transaction = transaction;
        }

        public async Task<RespostaGenerica> InserirChaleAsync(ChaleDto chaleDto)
        {
            try
            {
                _transaction.BeginTransaction();

                var chale = _conversor.Converter(chaleDto);
                await _repository.InserirAsync(chale);

                _transaction.Commit();

                return new RespostaGenerica("Inserção realizada com sucesso.", true);
            }
            catch
            {
                _transaction.Rollback();

                return new RespostaGenerica("Não foi possível realizar a inserção", false);
            }
        }

        public async Task<RespostaGenerica> ObterChaleAsync(Guid id)
        {
            try
            {
                _transaction.BeginTransaction();

                var chale = await _repository.ObterPorIdAsync(id);
                var chaleDto = _conversor.Converter(chale);
                
                _transaction.Commit();

                return new RespostaGenerica("Consulta realizada com sucesso.", true, chaleDto);
            }
            catch
            {
                _transaction.Rollback();

                return new RespostaGenerica("Não foi possível realizar a consulta.", false);
            }
        }

        public async Task<RespostaGenerica> ObterChalesDisponiveis(DateTime inicio, DateTime fim)
        {
            try
            {
                _transaction.BeginTransaction();

                var chalesDisponiveis = await _chaleRepository.ObterChalesDisponiveis(inicio, fim);
                var chalesDisponiveisDto = _conversor.ConverterLista(chalesDisponiveis);

                _transaction.Commit();

                return new RespostaGenerica("Consulta realizada com sucesso.", true, chalesDisponiveisDto);
            }
            catch
            {
                _transaction.Rollback();

                return new RespostaGenerica("Não foi possível realizar a consulta.", false);
            }
        }

        public async Task<RespostaGenerica> ObterTodosChalesAsync()
        {
            try
            {
                _transaction.BeginTransaction();

                var chales = await _repository.ObterTodosAsync();
                var chalesDto = _conversor.ConverterLista(chales);

                _transaction.Commit();

                return new RespostaGenerica("Consulta realizada com sucesso.", true, chalesDto);
            }
            catch
            {
                _transaction.Rollback();

                return new RespostaGenerica("Ocorreu um erro interno ao processar a consulta.", false);
            }
        }

        public RespostaGenerica RemoverChale(Guid id)
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

                return new RespostaGenerica("O registro não pôde ser removido.", false);
            }
        }
    }
}
