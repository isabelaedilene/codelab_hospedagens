using CodelabHospedagens.Domain.HospedagemAggregate;
using CodelabHospedagens.Infra.DataBaseSettings;
using Dapper;

namespace CodelabHospedagens.Infra.Repository
{
    public class HospedagemRepository : IHospedagemRepository
    {
        private readonly DbSession _session;

        public HospedagemRepository(DbSession session)
        {
            _session = session;
        }

        public async Task Inserir(Hospedagem hospedagem)
        {
            var dataInicio = hospedagem.DataInicio.Date;
            var dataFim = hospedagem.DataFim.Date;

            var queryHospedagem = @"INSERT INTO hospedagem (id, id_chale, id_cliente, data_inicio, data_fim, quantidade_pessoas, desconto, valor_final) 
                                    VALUES (@Id, @IdChale, @IdCliente, @dataInicio, @dataFim, @QuantidadePessoas, @Desconto, @ValorFinal)";

            await _session.Connection.ExecuteAsync(queryHospedagem, new { 
                hospedagem.Id, 
                hospedagem.IdChale, 
                hospedagem.IdCliente,
                dataInicio,
                dataFim,
                hospedagem.QuantidadePessoas,
                hospedagem.Desconto,
                hospedagem.ValorFinal
            });

            var queryHospedagemServicos = $@"INSERT INTO hospedagem_servico (id_hospedagem, id_servico, valor) 
                                            VALUES (@Id, @Nome, @Valor)";

            foreach (var servico in hospedagem.Servicos)
            {
                await _session.Connection.ExecuteAsync(queryHospedagemServicos, new { hospedagem.Id, servico.Nome, servico.Valor });
            }
        }

        public async Task<IEnumerable<Hospedagem>> ObterHospedagensCliente(Guid id)
        {
            var queryHospedagens = @"SELECT 
                            id Id,
                            id_chale IdChale,
                            id_cliente IdCliente,
                            data_inicio DataInicio,
                            data_fim DataFim,
                            quantidade_pessoas Quantidade,
                            desconto Desconto,
                            valor_final ValorFinal
                        FROM
                            hospedagem
                        WHERE
                            id_cliente = @id";
            
            var hospedagensResultado = await _session.Connection.QueryAsync(queryHospedagens, new { id });

            var queryServicos = @"SELECT 
                                    id_hospedagem IdHospedagem,
                                    id_servico IdServico,
                                    valor Valor
                                FROM
                                    hospedagem_servico
                                WHERE
                                    id_hospedagem = @Id";

            List<Hospedagem> hospedagens = new();
            foreach (var hospedagem in hospedagensResultado)
            {
                var servicosHospedagem = await _session.Connection.QueryAsync(queryServicos, new { hospedagem.Id });

                List<Servico> servicos = new ();
                foreach (var servico in servicosHospedagem)
                {
                    servicos.Add(new Servico(servico.IdServico, servico.Valor));
                }

                hospedagens.Add(new Hospedagem(
                    hospedagem.Id,
                    hospedagem.IdChale,
                    hospedagem.IdCliente,
                    Convert.ToDateTime(hospedagem.DataInicio),
                    Convert.ToDateTime(hospedagem.DataFim), 
                    Convert.ToInt32(hospedagem.QuantidadePessoas),
                    Convert.ToDecimal(hospedagem.Desconto),
                    Convert.ToDecimal(hospedagem.ValorFinal),
                    servicos
                ));
            }

            return hospedagens;
        }

        public async Task<IEnumerable<Servico>> ObterServicos(IEnumerable<string> nomeServicos)
        {
            var query = @"SELECT 
                            nome Nome, 
                            valor Valor
                        FROM
                            servico
                        WHERE nome IN @nomeServicos";

            return await _session.Connection.QueryAsync<Servico>(query, new { nomeServicos });
        }
    }
}
