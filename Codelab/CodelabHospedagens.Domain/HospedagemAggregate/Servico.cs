using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelabHospedagens.Domain.HospedagemAggregate
{
    public class Servico
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public Servico() { }

        public Servico(string nome, decimal valor)
        {
            Nome = nome;
            Valor = valor;
        }
    }
}
