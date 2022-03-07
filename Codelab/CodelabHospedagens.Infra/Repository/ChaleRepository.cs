using CodelabHospedagens.Domain.ChaleAggregate;
using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Infra.DataBaseSettings;
using Dapper;

namespace CodelabHospedagens.Infra.Repository
{
    public class ChaleRepository : IRepository<Chale>, IChaleRepository
    {
        private readonly DbSession _session;

        public ChaleRepository(DbSession session)
        {
            _session = session;
        }
        public async Task InserirAsync(Chale chale)
        {
            var queryChale = $@"INSERT INTO chale(
                                    id,
                                    localizacao,
                                    capacidade,
                                    valor_baixa_estacao,
                                    valor_alta_estacao)
                                VALUES(
                                    @Id,
                                    @Localizacao,
                                    @Capacidade,
                                    @ValorBaixaEstacao,
                                    @ValorAltaEstacao) ";

            await _session.Connection.ExecuteAsync(queryChale, new { 
                chale.Id,
                chale.Localizacao,
                chale.Capacidade,
                chale.ValorBaixaEstacao,
                chale.ValorAltaEstacao
            });

            var nomesItens = chale.Itens.Select(i => i.Nome);

            var queryItens = "SELECT * FROM item WHERE nome IN @nomesItens";

            var itens = await _session.Connection.QueryAsync(queryItens, new { nomesItens });

            var queryChaleItem = $@"INSERT INTO chale_item (id_chale, nome_item) 
                                    VALUES (@Id, @nome)";
                
            foreach (var item in itens)
            {
                await _session.Connection.ExecuteAsync(queryChaleItem, new { chale.Id, item.nome});
            }
        }

        public async Task<IEnumerable<Chale>> ObterChalesDisponiveis(DateTime inicio, DateTime fim)
        {
            var queryReservados = $@"SELECT * FROM hospedagem 
                                    WHERE data_inicio BETWEEN @inicio AND @fim
                                    OR data_fim BETWEEN @inicio AND @fim
                                    OR data_inicio <= @inicio and data_fim >= @fim;";

            var chalesReservados = await _session.Connection.QueryAsync(queryReservados, new { inicio, fim });

            var idChalesReservados = chalesReservados.Select(i => i.id_chale);

            var queryDisponiveis = @"SELECT 
                                        *
                                    FROM
                                        chale
                                    WHERE
                                        id NOT IN @idChalesReservados";

            var chalesDisponiveis = await _session.Connection.QueryAsync(queryDisponiveis, new { idChalesReservados });

            List<Chale> chales = new();
            foreach (var chaleDisponivel in chalesDisponiveis)
            {
                var chale = await ObterPorIdAsync(Guid.Parse(chaleDisponivel.id));
                chales.Add(chale);
            }

            return chales;
        }

        public async Task<Chale> ObterPorIdAsync(Guid id)
        {
            var queryChale = $@"SELECT 
                                    id Id,
                                    localizacao Localizacao,
                                    capacidade Capacidade,
                                    valor_baixa_estacao ValorBaixaEstacao,
                                    valor_alta_estacao ValorAltaEstacao
                                FROM
                                    chale
                                WHERE
                                    id = '{id}'";
            
            var chaleResultado = await _session.Connection.QueryFirstAsync(queryChale);

            var queryChaleItens = $@"SELECT 
                                        i.nome Nome,
                                        i.descricao Descricao
                                    FROM
                                        item i
                                    JOIN
                                        chale_item c ON i.nome = c.nome_item
                                    WHERE
                                        c.id_chale = '{id}'";

            var itensResultado = await _session.Connection.QueryAsync(queryChaleItens);

            List<Item> itens = new();
            foreach (var item in itensResultado)
            {
                itens.Add(new Item(item.Nome, item.Descricao));
            }

            var chale = new Chale
            (
                chaleResultado.Id,
                chaleResultado.Localizacao,
                chaleResultado.Capacidade,
                chaleResultado.ValorBaixaEstacao,
                chaleResultado.ValorAltaEstacao,
                itens
            );

            return chale;
        }

        public async Task<IEnumerable<Chale>> ObterTodosAsync()
        {
            var queryChale = $@"SELECT 
                                    id Id,
                                    localizacao Localizacao,
                                    capacidade Capacidade,
                                    valor_baixa_estacao ValorBaixaEstacao,
                                    valor_alta_estacao ValorAltaEstacao
                                FROM
                                    chale";

            var chalesResultado = await _session.Connection.QueryAsync(queryChale);

            var queryChaleItens = $@"SELECT 
                                        i.nome Nome,
                                        i.descricao Descricao
                                    FROM
                                        item i
                                    JOIN
                                        chale_item c ON i.nome = c.nome_item
                                    WHERE
                                        c.id_chale = @Id";

            List<Chale> chales = new ();

            foreach (var chale in chalesResultado)
            {
                var itensResultado = await _session.Connection.QueryAsync(queryChaleItens, new { chale.Id });

                List<Item> itens = new();
                foreach (var item in itensResultado)
                {
                    itens.Add(new Item(item.Nome, item.Descricao));
                }

                chales.Add(new Chale
                (
                    chale.Id,
                    chale.Localizacao,
                    chale.Capacidade,
                    chale.ValorAltaEstacao,
                    chale.ValorBaixaEstacao,
                    itens
                ));
            }

            return chales;
        }

        public void Remover(Guid id)
        {
            var queryChaleItem = @"DELETE FROM chale_item
                                 WHERE id_chale = @id";

            var queryChale = @"DELETE FROM chale
                               WHERE id = @id";

            _session.Connection.Execute(queryChaleItem, new { id });
            _session.Connection.Execute(queryChale, new { id });
        }
    }
}
