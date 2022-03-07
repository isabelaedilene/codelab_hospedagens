using CodelabHospedagens.Domain.SeedWork;

namespace CodelabHospedagens.Domain.HospedagemAggregate
{
    public class Hospedagem : IAggregateRoot
    {
        public string Id { get; set; }
        public string IdChale { get; set; }
        public string IdCliente { set; get; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int QuantidadePessoas { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorFinal { get; set; }
        public List<Servico> Servicos { get; set; }

        public Hospedagem
        (
            string id,
            string idChale,
            string idCliente,
            DateTime dataInicio,
            DateTime dataFim,
            int quantidadePessoas,
            decimal desconto,
            decimal valorFinal,
            List<Servico> servicos
        )
        {
            Id = id;
            IdChale= idChale;
            IdCliente = idCliente;
            DataInicio = dataInicio;
            DataFim = dataFim;
            QuantidadePessoas = quantidadePessoas;
            Desconto = desconto;
            ValorFinal = valorFinal;
            Servicos = servicos;
        }
    }
}
