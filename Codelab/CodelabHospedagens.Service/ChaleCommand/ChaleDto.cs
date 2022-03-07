using CodelabHospedagens.Domain.ChaleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelabHospedagens.Service.ChaleCommand
{
    public class ChaleDto
    {
        public string Localizacao { get; set; }
        public int Capacidade { get; set; }
        public decimal ValorAltaEstacao { get; set; }
        public decimal ValorBaixaEstacao { get; set; }
        public List<Item> Itens { get; set; }
    }
}
