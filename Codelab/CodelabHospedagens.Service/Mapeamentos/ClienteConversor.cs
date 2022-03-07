using CodelabHospedagens.Domain.ClienteAggregate;
using CodelabHospedagens.Service.ClienteCommand;

namespace CodelabHospedagens.Domain.SeedWork
{
    public class ClienteConversor : IConversor<Cliente, ClienteDto>
    {
        public ClienteDto Converter(Cliente origem)
        {
            return new ClienteDto
            {
                Nome = origem.Nome,
                Rg = origem.Rg,
                Nascimento = origem.Nascimento,
                Endereco = origem.Endereco,
                Telefones = origem.Telefones
            };
        }

        public Cliente Converter(ClienteDto origem)
        {
            return new Cliente
            {
                Id = Guid.NewGuid().ToString(),
                Nome = origem.Nome,
                Rg = origem.Rg,
                Nascimento = origem.Nascimento,
                Endereco = origem.Endereco,
                Telefones = origem.Telefones
            };
        }

        public IEnumerable<ClienteDto> ConverterLista(IEnumerable<Cliente> origem)
        {
            List<ClienteDto> clientesDto = new();

            foreach (var cliente in origem)
            {
                clientesDto.Add(new ClienteDto
                {
                    Nome = cliente.Nome,
                    Rg = cliente.Rg,
                    Nascimento = cliente.Nascimento,
                    Endereco = cliente.Endereco,
                    Telefones = cliente.Telefones
                });
            }
            
            return clientesDto;
        }
    }
}
