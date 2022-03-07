using CodelabHospedagens.Domain.ChaleAggregate;
using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Service.ChaleCommand;

namespace CodelabHospedagens.Service.Mapeamentos
{
    public class ChaleConversor : IConversor<Chale, ChaleDto>
    {
        public ChaleDto Converter(Chale origem)
        {
            return new ChaleDto
            {
                Localizacao = origem.Localizacao,
                Capacidade = origem.Capacidade,
                ValorAltaEstacao = origem.ValorAltaEstacao,
                ValorBaixaEstacao = origem.ValorBaixaEstacao,
                Itens = origem.Itens
            };
        }

        public Chale Converter(ChaleDto origem)
        {
            return new Chale
            (
                Guid.NewGuid().ToString(),
                origem.Localizacao,
                origem.Capacidade,
                origem.ValorAltaEstacao,
                origem.ValorBaixaEstacao,
                origem.Itens
            );
        }

        public IEnumerable<ChaleDto> ConverterLista(IEnumerable<Chale> origem)
        {
            List<ChaleDto> chalesDto = new();

            foreach (var chale in origem)
            {
                chalesDto.Add(new ChaleDto
                {
                    Localizacao = chale.Localizacao,
                    Capacidade = chale.Capacidade,
                    ValorAltaEstacao = chale.ValorAltaEstacao,
                    ValorBaixaEstacao = chale.ValorBaixaEstacao,
                    Itens = chale.Itens
                });
            }

            return chalesDto;
        }
    }
}
