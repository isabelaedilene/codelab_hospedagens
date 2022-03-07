using AutoFixture;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;
using CodelabHospedagens.Domain.ClienteAggregate;
using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Service.ClienteCommand;
using System;

namespace CodelabHospedagens.Tests
{
    public class ClienteServiceTestesUnitarios
    {
        [Fact]
        public async Task DeveInserirNovoClienteCorretamente()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Cliente, ClienteDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var clienteDto = _fixture
               .Build<ClienteDto>()
               .WithAutoProperties()
               .Create();
            var cliente = _fixture
               .Build<Cliente>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.InserirAsync(cliente));
            _conversorMock.Setup(x => x.Converter(clienteDto)).Returns(cliente);
            var service = new ClienteService(_repositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.InserirClienteAsync(clienteDto);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Cliente inserido com sucesso.", true);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveInserirClienteSeForLancadaExcecao()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Cliente, ClienteDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var clienteDto = _fixture
               .Build<ClienteDto>()
               .WithAutoProperties()
               .Create();
            var cliente = _fixture
               .Build<Cliente>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.InserirAsync(cliente)).ThrowsAsync(new Exception());
            _conversorMock.Setup(x => x.Converter(clienteDto)).Returns(cliente);
            var service = new ClienteService(_repositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.InserirClienteAsync(clienteDto);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Não foi possível inserir o cliente.", false);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task DeveRetornarClienteCorretamenteDeAcordaComIdInformado()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Cliente, ClienteDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var idCliente = _fixture.Create<Guid>();
            var idClienteInexistente = _fixture.Create<Guid>();
            var clienteDto = _fixture
               .Build<ClienteDto>()
               .WithAutoProperties()
               .Create();
            var clienteUm = _fixture
               .Build<Cliente>()
               .With(x => x.Id, idCliente.ToString())
               .WithAutoProperties()
               .Create();
            var clienteDois = _fixture
               .Build<Cliente>()
               .With(x => x.Id, idClienteInexistente.ToString())
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterPorIdAsync(idCliente)).ReturnsAsync(clienteUm);
            _conversorMock.Setup(x => x.Converter(clienteUm)).Returns(clienteDto);
            var service = new ClienteService(_repositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterClienteAsync(idCliente);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Consulta realizada com sucesso.", true, clienteDto);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveRetornarClienteComIdInexistenteInformado()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Cliente, ClienteDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var idCliente = _fixture.Create<Guid>();
            var idClienteInexistente = _fixture.Create<Guid>();
            var clienteDto = _fixture
               .Build<ClienteDto>()
               .WithAutoProperties()
               .Create();
            var clienteUm = _fixture
               .Build<Cliente>()
               .With(x => x.Id, idCliente.ToString())
               .WithAutoProperties()
               .Create();
            var clienteDois = _fixture
               .Build<Cliente>()
               .With(x => x.Id, idClienteInexistente.ToString())
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterPorIdAsync(idCliente)).ReturnsAsync(clienteUm);
            _conversorMock.Setup(x => x.Converter(clienteUm)).Returns(clienteDto);
            var service = new ClienteService(_repositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterClienteAsync(idClienteInexistente);

            //Assert
            resultadoAtual.Dados.Should().BeNull();
        }

        [Fact]
        public async Task NaoDeveRetornarClienteSeExcecaoForLancada()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Cliente, ClienteDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var idCliente = _fixture.Create<Guid>();
            var idClienteInexistente = _fixture.Create<Guid>();
            var clienteDto = _fixture
               .Build<ClienteDto>()
               .WithAutoProperties()
               .Create();
            var clienteUm = _fixture
               .Build<Cliente>()
               .With(x => x.Id, idCliente.ToString())
               .WithAutoProperties()
               .Create();
            var clienteDois = _fixture
               .Build<Cliente>()
               .With(x => x.Id, idClienteInexistente.ToString())
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterPorIdAsync(idCliente)).ThrowsAsync(new Exception());
            _conversorMock.Setup(x => x.Converter(clienteUm)).Returns(clienteDto);
            var service = new ClienteService(_repositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterClienteAsync(idCliente);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Não foi possível realizar a consulta.", false);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task DeveRetornarTodosOsClientesEncontradosComSucesso()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Cliente, ClienteDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var clientes = _fixture
               .Build<List<Cliente>>()
               .WithAutoProperties()
               .Create();
            var clientesDto = _fixture
               .Build<List<ClienteDto>>()
               .WithAutoProperties()
               .Create();
            var clienteDto = _fixture
               .Build<ClienteDto>()
               .WithAutoProperties()
               .Create();
            var cliente = _fixture
               .Build<Cliente>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterTodosAsync()).ReturnsAsync(clientes);
            _conversorMock.Setup(x => x.ConverterLista(clientes)).Returns(clientesDto);
            var service = new ClienteService(_repositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterTodosClientesAsync();

            //Assert
            var resultadoEsperado = new RespostaGenerica("Consulta realizada com sucesso.", true, clientesDto);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task DeveRetornarErroAoLancarExcecaoAoObterTodosClientes()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Cliente, ClienteDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var clientes = _fixture
               .Build<List<Cliente>>()
               .WithAutoProperties()
               .Create();
            var clientesDto = _fixture
               .Build<List<ClienteDto>>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterTodosAsync()).ThrowsAsync(new Exception());
            _conversorMock.Setup(x => x.ConverterLista(clientes)).Returns(clientesDto);
            var service = new ClienteService(_repositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterTodosClientesAsync();

            //Assert
            var resultadoEsperado = new RespostaGenerica("Ocorreu um erro interno ao processar a consulta.", false);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public void DeveRemoverClienteCorretamente()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Cliente, ClienteDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();

            //Act
            _repositoryMock.Setup(x => x.Remover(It.IsAny<Guid>()));
            var service = new ClienteService(_repositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = service.RemoverCliente(It.IsAny<Guid>());

            //Assert
            var resultadoEsperado = new RespostaGenerica("Registro removido com sucesso.", true);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public void DeveRetornarErroCasoNaoSejaPossivelRemoverClienteCorretamente()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Cliente, ClienteDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();

            //Act
            _repositoryMock.Setup(x => x.Remover(It.IsAny<Guid>())).Throws(new Exception());
            var service = new ClienteService(_repositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = service.RemoverCliente(It.IsAny<Guid>());

            //Assert
            var resultadoEsperado = new RespostaGenerica("Ocorreu um erro interno ao tentar remover o registro.", false);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}