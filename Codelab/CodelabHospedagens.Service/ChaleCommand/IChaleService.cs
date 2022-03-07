using CodelabHospedagens.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodelabHospedagens.Service.ChaleCommand
{
    public interface IChaleService
    {
        public Task<RespostaGenerica> ObterChaleAsync(Guid id);
        public Task<RespostaGenerica> ObterTodosChalesAsync();
        public Task<RespostaGenerica> InserirChaleAsync(ChaleDto chaeDto);
        public RespostaGenerica RemoverChale(Guid id);
        public Task<RespostaGenerica> ObterChalesDisponiveis(DateTime inicio, DateTime fim);
    }
}
