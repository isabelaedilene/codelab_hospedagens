using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Domain.HospedagemAggregate;
using CodelabHospedagens.Service.HospedagemCommand;    

namespace CodelabHospedagens.Service.Mapeamentos
{
    public class HospedagemConversor : IConversor<Hospedagem, HospedagemDto>
    {
        public HospedagemDto Converter(Hospedagem origem)
        {
            return new HospedagemDto
            {
                DataInicio = origem.DataInicio,
                DataFim = origem.DataFim,
                QuantidadePessoas = origem.QuantidadePessoas,
                Desconto = origem.Desconto,
                ValorFinal = origem.ValorFinal,
                Servicos = origem.Servicos
            };
        }

        public Hospedagem Converter(HospedagemDto origem)
        {
            return new Hospedagem
            (
                Guid.NewGuid().ToString(),
                origem.IdChale,
                origem.IdCliente,
                origem.DataInicio,
                origem.DataFim,
                origem.QuantidadePessoas,
                origem.Desconto,
                origem.ValorFinal,
                origem.Servicos
            );
        }

        public IEnumerable<HospedagemDto> ConverterLista(IEnumerable<Hospedagem> origem)
        {
            List<HospedagemDto> hospedagensDto = new();

            foreach (var hospedagem in origem)
            {
                hospedagensDto.Add(new HospedagemDto
                {
                    IdChale = hospedagem.IdChale,
                    IdCliente = hospedagem.IdCliente,
                    DataInicio = hospedagem.DataInicio,
                    DataFim = hospedagem.DataFim,
                    QuantidadePessoas = hospedagem.QuantidadePessoas,
                    Desconto = hospedagem.Desconto,
                    ValorFinal = hospedagem.ValorFinal,
                    Servicos = hospedagem.Servicos
                });
            }

            return hospedagensDto;
        }
    }
}
