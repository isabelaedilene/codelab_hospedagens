using CodelabHospedagens.Domain.SeedWork;

namespace CodelabHospedagens.Domain.ClienteAggregate
{
    public class Cliente : IAggregateRoot
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Rg { get; set; }
        public DateTime Nascimento { get; set; }
        public Endereco Endereco { get; set; }
        public List<Telefone> Telefones { get; set; }

        public Cliente(string id, string nome, string rg, DateTime nascimento, Endereco endereco, List<Telefone> telefones)
        {
            Id = id;
            Nome = nome;
            Rg = rg;
            Nascimento = nascimento;
            Endereco = endereco;
            Telefones = telefones;
        }

        public Cliente(){ }
    }
}
