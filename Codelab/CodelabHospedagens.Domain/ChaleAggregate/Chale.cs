using CodelabHospedagens.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelabHospedagens.Domain.ChaleAggregate
{
    public class Chale : IAggregateRoot
    {
        public string Id { get; set; }
        public string Localizacao { get; set; }
        public int Capacidade { get; set; }
        public decimal ValorAltaEstacao { get; set; }
        public decimal ValorBaixaEstacao { get; set; }
        public List<Item> Itens { get; set; }

        public Chale
        (
            string id,
            string localizacao,
            int capacidade,
            decimal valorAltaEstacao,
            decimal valorBaixaEstacao,
            List<Item> itens
        )
        {
            Id = id;
            Localizacao = localizacao;
            Capacidade = capacidade;
            ValorAltaEstacao = valorAltaEstacao;
            ValorBaixaEstacao = valorBaixaEstacao;
            Itens = itens;
        }

        public Chale() { }
    }
}
