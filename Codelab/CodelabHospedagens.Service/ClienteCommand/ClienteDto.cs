using CodelabHospedagens.Domain.ClienteAggregate;

namespace CodelabHospedagens.Service.ClienteCommand
{
    public class ClienteDto
    {
        public string Nome { get; set; }
        public string Rg { get; set; }
        public DateTime Nascimento { get; set; }
        public Endereco Endereco { get; set; }
        public List<Telefone> Telefones { get; set; }
    }
}
