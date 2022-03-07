using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelabHospedagens.Domain.HospedagemAggregate

{
    public interface IHospedagemRepository
    {
        Task<IEnumerable<Hospedagem>> ObterHospedagensCliente(Guid id);
        Task Inserir(Hospedagem hospedagem);
        Task<IEnumerable<Servico>> ObterServicos(IEnumerable<string> nomeServicos);
    }
}
