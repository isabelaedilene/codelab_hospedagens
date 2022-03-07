using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelabHospedagens.Domain.ChaleAggregate
{
    public interface IChaleRepository
    {
       Task<IEnumerable<Chale>> ObterChalesDisponiveis(DateTime inicio, DateTime fim);
    }
}
