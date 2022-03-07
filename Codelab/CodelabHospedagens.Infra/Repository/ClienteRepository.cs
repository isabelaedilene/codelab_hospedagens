using CodelabHospedagens.Domain.ClienteAggregate;
using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Infra.DataBaseSettings;
using Dapper;

namespace CodelabHospedagens.Infra
{
    public class ClienteRepository : IRepository<Cliente>
    {
        private readonly DbSession _session;

        public ClienteRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<Cliente> ObterPorIdAsync(Guid id)
        {
            var query = $@"SELECT 
                            C.Id ClienteId,
                            C.Nome, 
                            C.Nascimento, 
                            C.Rg, 
                            T.Numero NumeroTelefone,
                            T.Tipo, 
                            E.Logradouro, 
                            E.Numero NumeroEndereco,
                            E.Cep, 
                            E.Bairro, 
                            E.Cidade, 
                            E.Estado 
                        FROM 
                            cliente C 
                        JOIN
                            telefone T ON C.Id = T.id_cliente  
                        JOIN 
                            endereco E  on E.id_cliente = C.Id 
                        WHERE 
                            C.Id = @id";

            var respostas = await _session.Connection.QueryAsync(query, new {id});

            Cliente cliente = new();

            if(respostas.Any())
            {
                var telefones = new List<Telefone>();
                foreach (var resposta in respostas)
                {
                    telefones.Add(new Telefone(resposta.NumeroTelefone, resposta.Tipo));
                }

                var primeiroRegistro = respostas.FirstOrDefault();

                var endereco = new Endereco(primeiroRegistro?.Logradouro, primeiroRegistro?.NumeroEndereco, primeiroRegistro?.Bairro, primeiroRegistro?.Cidade, primeiroRegistro?.Estado, primeiroRegistro?.Cep);

                cliente = new Cliente(primeiroRegistro?.ClienteId, primeiroRegistro?.Nome, primeiroRegistro?.Rg, primeiroRegistro?.Nascimento, endereco, telefones);
            }

            return cliente;
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            var query = $@"SELECT 
                            C.Id AS ClienteId,
                            C.Nome,
                            C.Nascimento,
                            C.Rg,
                            T.Numero AS NumeroTelefone,
                            T.Tipo,
                            E.Logradouro,
                            E.Numero AS NumeroEndereco,
                            E.Cep,
                            E.Bairro,
                            E.Cidade,
                            E.Estado
                        FROM
                            cliente C
                        JOIN
                            telefone T ON C.Id = T.id_cliente
                        JOIN
                            endereco E ON E.id_cliente = C.Id";

            var respostas = await _session.Connection.QueryAsync(query);

            var clientesDistintos = respostas.DistinctBy(r => r.ClienteId).ToList();

            List<Cliente> clientes = new ();

            foreach(var cliente in clientesDistintos)
            {
                var telefones = new List<Telefone>();

                var respostasFiltradas = respostas.Where(r => r.ClienteId == cliente.ClienteId);

                foreach (var resposta in respostasFiltradas)
                {
                    telefones.Add(new Telefone(resposta.NumeroTelefone, resposta.Tipo));
                }

                var endereco = new Endereco(cliente.Logradouro, cliente.NumeroEndereco, cliente.Bairro, cliente.Cidade, cliente.Estado, cliente.Cep);

                clientes.Add(new Cliente(cliente.ClienteId, cliente.Nome, cliente.Rg, cliente.Nascimento, endereco, telefones));
            }

            return clientes;
        }

        public async Task InserirAsync(Cliente cliente)
        {
            var queryCliente = @"INSERT INTO cliente (id, nome, rg, nascimento) 
                                    VALUES (@id, @nome, @rg, @nascimento)";

            var queryTelefone = $@"INSERT INTO telefone (numero, id_cliente, tipo) 
                                    VALUES (@numero, @Id, @tipo)";

            var queryEndereco = $@"INSERT INTO endereco (id_cliente, logradouro, numero, bairro, cidade, estado, cep) 
                                    VALUES (@Id, @logradouro, @numero, @bairro, @cidade, @estado, @cep)";

            await _session.Connection.ExecuteAsync(queryCliente, new { cliente.Id, cliente.Nome, cliente.Rg, cliente.Nascimento });

            foreach (var telefone in cliente.Telefones)
            {
                await _session.Connection.ExecuteAsync(queryTelefone, new { telefone.Numero, cliente.Id, telefone.Tipo });
            }
            
            await _session.Connection.ExecuteAsync(queryEndereco, new { cliente.Id, cliente.Endereco.Logradouro, cliente.Endereco.Numero, cliente.Endereco.Bairro, cliente.Endereco.Cidade, cliente.Endereco.Estado, cliente.Endereco.Cep });
        }

        public void Remover(Guid id)
        {
            var queryTelefone = @"DELETE FROM telefone WHERE id_cliente = @id";
            var queryEndereco = "DELETE FROM endereco WHERE id_cliente = @id";
            var queryCliente = @"DELETE FROM cliente WHERE id = @id";

            _session.Connection.Execute(queryTelefone, new { id });
            _session.Connection.Execute(queryEndereco, new { id });
            _session.Connection.Execute(queryCliente, new { id });
        }
    }
}