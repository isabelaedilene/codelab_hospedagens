using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelabHospedagens.Domain.SeedWork
{
    public class RespostaGenerica
    {
        public object? Dados { get; set; }
        public string Mensagem { get; set; }
        public bool Sucesso { get; set; }

        public RespostaGenerica(string mensagem, bool sucesso, object? dados = null)
        {
            Dados = dados;
            Mensagem = mensagem;
            Sucesso = sucesso;
        }
    }
}
