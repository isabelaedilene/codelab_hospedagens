using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelabHospedagens.Domain.ChaleAggregate
{
    public class Item
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public Item(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }
    }
}
