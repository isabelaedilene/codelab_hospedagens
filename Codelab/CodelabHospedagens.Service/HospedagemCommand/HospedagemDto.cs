using CodelabHospedagens.Domain.ChaleAggregate;
using CodelabHospedagens.Domain.ClienteAggregate;
using CodelabHospedagens.Domain.HospedagemAggregate;

namespace CodelabHospedagens.Service.HospedagemCommand
{
    public class HospedagemDto
    {
        public string IdChale { get; set; }
        public string IdCliente { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int QuantidadePessoas { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorFinal { get; set; }
        public List<Servico> Servicos { get; set; }
    }
}
