using AutoFixture;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Domain.HospedagemAggregate;
using CodelabHospedagens.Service.HospedagemCommand;
using System;
using System.Collections.Generic;
using CodelabHospedagens.Domain.ChaleAggregate;
using CodelabHospedagens.Domain.ClienteAggregate;

namespace CodelabHospedagens.Tests
{
    public class HospedagemServiceTestesUnitarios
    {
        [Fact]
        public async Task DeveObterTodasHospedagensDeUmDeterminadoCliente()
        {
            //Arrange
            var _repositoryMock = new Mock<IHospedagemRepository>();
            var _chaleRepositoryMock = new Mock<IRepository<Chale>>();
            var _clienteRepositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Hospedagem, HospedagemDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var idHospedagem = _fixture.Create<Guid>();
            var hospedagens = _fixture
               .Build<List<Hospedagem>>()
               .WithAutoProperties()
               .Create();
            var hospedagensDto = _fixture
               .Build<List<HospedagemDto>>()
               .WithAutoProperties()
               .Create();
            var hospedagemDto = _fixture
               .Build<List<HospedagemDto>>()
               .WithAutoProperties()
               .Create();
            var hospedagem = _fixture
               .Build<Hospedagem>()
               .WithAutoProperties()
               .Create();
            var cliente = _fixture
               .Build<Cliente>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterHospedagensCliente(idHospedagem)).ReturnsAsync(hospedagens);
            _clienteRepositoryMock.Setup(x => x.ObterPorIdAsync(idHospedagem)).ReturnsAsync(cliente);
            _conversorMock.Setup(x => x.ConverterLista(hospedagens)).Returns(hospedagensDto);
            var service = new HospedagemService(_repositoryMock.Object, _clienteRepositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterHospedagensCliente(idHospedagem);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Consulta realizada com sucesso.", true, hospedagensDto);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveRetornarHospedagensSeForLancadaExcecao()
        {
            //Arrange
            var _repositoryMock = new Mock<IHospedagemRepository>();
            var _chaleRepositoryMock = new Mock<IRepository<Chale>>();
            var _clienteRepositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Hospedagem, HospedagemDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var idHospedagem = _fixture.Create<Guid>();
            var hospedagens = _fixture
               .Build<List<Hospedagem>>()
               .WithAutoProperties()
               .Create();
            var hospedagensDto = _fixture
               .Build<List<HospedagemDto>>()
               .WithAutoProperties()
               .Create();
            var hospedagemDto = _fixture
               .Build<List<HospedagemDto>>()
               .WithAutoProperties()
               .Create();
            var hospedagem = _fixture
               .Build<Hospedagem>()
               .WithAutoProperties()
               .Create();
            var cliente = _fixture
               .Build<Cliente>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterHospedagensCliente(idHospedagem)).ThrowsAsync(new Exception());
            _clienteRepositoryMock.Setup(x => x.ObterPorIdAsync(idHospedagem)).ReturnsAsync(cliente);
            _conversorMock.Setup(x => x.ConverterLista(hospedagens)).Returns(hospedagensDto);
            var service = new HospedagemService(_repositoryMock.Object, _clienteRepositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterHospedagensCliente(idHospedagem);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Não foi possível realizar a consulta.", false);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task DeveReservarChaleCorretamente()
        {
            //Arrange
            var _repositoryMock = new Mock<IHospedagemRepository>();
            var _chaleRepositoryMock = new Mock<IRepository<Chale>>();
            var _clienteRepositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Hospedagem, HospedagemDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var idHospedagem = _fixture.Create<Guid>();
            var idChale = Guid.NewGuid();
            var hospedagemDto = _fixture
               .Build<HospedagemDto>()
               .With(x => x.QuantidadePessoas, 1)
               .With(x => x.IdChale, idChale.ToString())
               .With(x => x.Desconto, 10)
               .WithAutoProperties()
               .Create();
            var hospedagem = _fixture
               .Build<Hospedagem>()
               .With(x => x.Id, idChale.ToString())
               .WithAutoProperties()
               .Create();
            var chale = _fixture
               .Build<Chale>()
               .With(x => x.Capacidade, 2)
               .With(x => x.ValorAltaEstacao, 200)
               .With(x => x.ValorAltaEstacao, 100)
               .WithAutoProperties()
               .Create();
            var nomes = _fixture
               .Build<List<string>>()
               .WithAutoProperties()
               .Create();
            var servicos = _fixture
               .Build<List<Servico>>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterServicos(nomes)).ReturnsAsync(servicos);
            _chaleRepositoryMock.Setup(x => x.ObterPorIdAsync(idChale)).ReturnsAsync(chale);
            _conversorMock.Setup(x => x.Converter(hospedagemDto)).Returns(hospedagem);
            var service = new HospedagemService(_repositoryMock.Object, _clienteRepositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ReservarChale(hospedagemDto);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Reserva realizada com sucesso.", true);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveReservarChaleSeQuantidadePessoasForMaiorQueCapacidadeChale()
        {
            //Arrange
            var _repositoryMock = new Mock<IHospedagemRepository>();
            var _chaleRepositoryMock = new Mock<IRepository<Chale>>();
            var _clienteRepositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Hospedagem, HospedagemDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var idHospedagem = _fixture.Create<Guid>();
            var idChale = Guid.NewGuid();
            var hospedagemDto = _fixture
               .Build<HospedagemDto>()
               .With(x => x.QuantidadePessoas, 5)
               .With(x => x.IdChale, idChale.ToString())
               .WithAutoProperties()
               .Create();
            var hospedagem = _fixture
               .Build<Hospedagem>()
               .With(x=> x.Id, idChale.ToString())
               .WithAutoProperties()
               .Create();
            var chale = _fixture
               .Build<Chale>()
               .With(x => x.Capacidade, 2)
               .WithAutoProperties()
               .Create();
            var nomes = _fixture
               .Build<List<string>>()
               .WithAutoProperties()
               .Create();
            var servicos = _fixture
               .Build<List<Servico>>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterServicos(nomes)).ReturnsAsync(servicos);
            _chaleRepositoryMock.Setup(x => x.ObterPorIdAsync(idChale)).ReturnsAsync(chale); 
            _conversorMock.Setup(x => x.Converter(hospedagemDto)).Returns(hospedagem);
            var service = new HospedagemService(_repositoryMock.Object, _clienteRepositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ReservarChale(hospedagemDto);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Não foi possível realizar a reserva. O quarto não suporta a quantidade de pessoas.", true);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveReservarChaleSeForLancadaExcecaoNoRepositorio()
        {
            //Arrange
            var _repositoryMock = new Mock<IHospedagemRepository>();
            var _chaleRepositoryMock = new Mock<IRepository<Chale>>();
            var _clienteRepositoryMock = new Mock<IRepository<Cliente>>();
            var _conversorMock = new Mock<IConversor<Hospedagem, HospedagemDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var idHospedagem = _fixture.Create<Guid>();
            var idChale = Guid.NewGuid();
            var hospedagemDto = _fixture
               .Build<HospedagemDto>()
               .WithAutoProperties()
               .Create();
            var hospedagem = _fixture
               .Build<Hospedagem>()
               .WithAutoProperties()
               .Create();
            var chale = _fixture
               .Build<Chale>()
               .WithAutoProperties()
               .Create();
            var nomes = _fixture
               .Build<List<string>>()
               .WithAutoProperties()
               .Create();
            var servicos = _fixture
               .Build<List<Servico>>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterServicos(nomes)).ThrowsAsync(new Exception());
            _chaleRepositoryMock.Setup(x => x.ObterPorIdAsync(idChale)).ReturnsAsync(chale);
            _conversorMock.Setup(x => x.Converter(hospedagemDto)).Returns(hospedagem);
            var service = new HospedagemService(_repositoryMock.Object, _clienteRepositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ReservarChale(hospedagemDto);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Não foi possível realizar a reserva.", false);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
