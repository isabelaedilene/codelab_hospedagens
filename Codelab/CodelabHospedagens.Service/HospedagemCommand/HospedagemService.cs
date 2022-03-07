using CodelabHospedagens.Domain.HospedagemAggregate;
using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Domain.ChaleAggregate;
using CodelabHospedagens.Domain.ClienteAggregate;

namespace CodelabHospedagens.Service.HospedagemCommand
{
    public class HospedagemService : IHospedagemService
    {
        private readonly IHospedagemRepository _repository;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<Chale> _chaleRepository;
        private readonly IConversor<Hospedagem, HospedagemDto> _conversor;
        private readonly IUnitOfWork _transaction;

        public HospedagemService(IHospedagemRepository repository, IRepository<Cliente> clienteRepository, IRepository<Chale> chaleRepository, IConversor<Hospedagem, HospedagemDto> conversor, IUnitOfWork transaction)
        {
            _repository = repository;
            _chaleRepository = chaleRepository;
            _clienteRepository = clienteRepository;
            _conversor = conversor;
            _transaction = transaction;
        }

        public async Task<RespostaGenerica> ObterHospedagensCliente(Guid id)
        {
            try
            {
                _transaction.BeginTransaction();

                var hospedagens = await _repository.ObterHospedagensCliente(id);

                var cliente = await _clienteRepository.ObterPorIdAsync(id);

                var hospedagensDto = _conversor.ConverterLista(hospedagens);

                _transaction.Commit();

                return new RespostaGenerica("Consulta realizada com sucesso.", true, hospedagensDto);
            }
            catch
            {
                _transaction.Rollback();

                return new RespostaGenerica("Não foi possível realizar a consulta.", false);
            }
        }

        public async Task<RespostaGenerica> ReservarChale(HospedagemDto hospedagemDto)
        {
            try
            {
                _transaction.BeginTransaction();
                
                var idChale = Guid.Parse(hospedagemDto.IdChale);
                var chale = await _chaleRepository.ObterPorIdAsync(idChale);

                var quantidadeDias = (hospedagemDto.DataFim - hospedagemDto.DataInicio).Days;

                var valorFinal = ObterValorChale(chale, hospedagemDto.DataInicio.Month, hospedagemDto.DataFim.Month) * quantidadeDias;
                valorFinal -= hospedagemDto.Desconto;

                if (hospedagemDto.QuantidadePessoas > chale.Capacidade)
                {
                    return new RespostaGenerica("Não foi possível realizar a reserva. O quarto não suporta a quantidade de pessoas.", true);
                }

                var servicosDesejados = await _repository.ObterServicos(hospedagemDto.Servicos.Select(s => s.Nome));

                foreach (var servicoDesejado in servicosDesejados)
                {
                    servicoDesejado.Valor *= hospedagemDto.QuantidadePessoas;
                    valorFinal += servicoDesejado.Valor;
                }
                
                hospedagemDto.ValorFinal = valorFinal;
                hospedagemDto.Servicos = servicosDesejados.ToList();

                var hospedagem = _conversor.Converter(hospedagemDto);

                await _repository.Inserir(hospedagem);

                _transaction.Commit();

                return new RespostaGenerica("Reserva realizada com sucesso.", true);
            }
            catch
            {
                _transaction.Rollback();

                return new RespostaGenerica("Não foi possível realizar a reserva.", false);
            }
        }

        private static decimal ObterValorChale(Chale chale, int inicioMes, int fimMes)
        {
            if ((inicioMes == 1) || (inicioMes == 7) || (inicioMes == 12) || (fimMes == 1) || (fimMes == 7) || (fimMes == 12))
            {
                return chale.ValorAltaEstacao;
            }
            else
            {
                return chale.ValorBaixaEstacao;
            }
        }
    }
}