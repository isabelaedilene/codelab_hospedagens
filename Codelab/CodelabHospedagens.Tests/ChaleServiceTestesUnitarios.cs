using AutoFixture;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using CodelabHospedagens.Domain.SeedWork;
using CodelabHospedagens.Domain.ChaleAggregate;
using CodelabHospedagens.Service.ChaleCommand;
using System;
using System.Collections.Generic;

namespace CodelabHospedagens.Tests
{
    public class ChaleServiceTestesUnitarios
    {
        [Fact]
        public async Task DeveInserirNovoChaleCorretamente()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Chale>>();
            var _chaleRepositoryMock = new Mock<IChaleRepository>();
            var _conversorMock = new Mock<IConversor<Chale, ChaleDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var chaleDto = _fixture
               .Build<ChaleDto>()
               .WithAutoProperties()
               .Create();
            var chale = _fixture
               .Build<Chale>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.InserirAsync(chale));
            _conversorMock.Setup(x => x.Converter(chaleDto)).Returns(chale);
            var service = new ChaleService(_repositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.InserirChaleAsync(chaleDto);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Inserção realizada com sucesso.", true);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task DeveRetornarMensagemDeErroSeNaoForPossivelInserirNovoChale()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Chale>>();
            var _chaleRepositoryMock = new Mock<IChaleRepository>();
            var _conversorMock = new Mock<IConversor<Chale, ChaleDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var chaleDto = _fixture
               .Build<ChaleDto>()
               .WithAutoProperties()
               .Create();
            var chale = _fixture
               .Build<Chale>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.InserirAsync(chale)).ThrowsAsync(new Exception());
            _conversorMock.Setup(x => x.Converter(chaleDto)).Returns(chale);
            var service = new ChaleService(_repositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.InserirChaleAsync(chaleDto);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Não foi possível realizar a inserção", false);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task DeveRetornarChaleCorretamenteDeAcordaComIdInformado()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Chale>>();
            var _chaleRepositoryMock = new Mock<IChaleRepository>();
            var _conversorMock = new Mock<IConversor<Chale, ChaleDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var idChale = _fixture.Create<Guid>();
            var idChaleInexistente = _fixture.Create<Guid>();
            var chaleDto = _fixture
               .Build<ChaleDto>()
               .WithAutoProperties()
               .Create();
            var chaleUm = _fixture
               .Build<Chale>()
               .With(x => x.Id, idChale.ToString())
               .WithAutoProperties()
               .Create();
            var chaleDois = _fixture
               .Build<Chale>()
               .With(x => x.Id, idChaleInexistente.ToString())
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterPorIdAsync(idChale)).ReturnsAsync(chaleUm);
            _conversorMock.Setup(x => x.Converter(chaleUm)).Returns(chaleDto);
            var service = new ChaleService(_repositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterChaleAsync(idChale);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Consulta realizada com sucesso.", true, chaleDto);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveRetornarChaleComIdInexistenteInformado()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Chale>>();
            var _chaleRepositoryMock = new Mock<IChaleRepository>();
            var _conversorMock = new Mock<IConversor<Chale, ChaleDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var idChale = _fixture.Create<Guid>();
            var idChaleInexistente = _fixture.Create<Guid>();
            var chaleDto = _fixture
               .Build<ChaleDto>()
               .WithAutoProperties()
               .Create();
            var chaleUm = _fixture
               .Build<Chale>()
               .With(x => x.Id, idChale.ToString())
               .WithAutoProperties()
               .Create();
            var chaleDois = _fixture
               .Build<Chale>()
               .With(x => x.Id, idChaleInexistente.ToString())
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterPorIdAsync(idChale)).ReturnsAsync(chaleUm);
            _conversorMock.Setup(x => x.Converter(chaleUm)).Returns(chaleDto);
            var service = new ChaleService(_repositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterChaleAsync(idChaleInexistente);

            //Assert
            resultadoAtual.Dados.Should().BeNull();
        }

        [Fact]
        public async Task NaoDeveRetornarChaleSeExcecaoForLancada()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Chale>>();
            var _chaleRepositoryMock = new Mock<IChaleRepository>();
            var _conversorMock = new Mock<IConversor<Chale, ChaleDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var idChale = _fixture.Create<Guid>();
            var idChaleInexistente = _fixture.Create<Guid>();
            var chaleDto = _fixture
               .Build<ChaleDto>()
               .WithAutoProperties()
               .Create();
            var chaleUm = _fixture
               .Build<Chale>()
               .With(x => x.Id, idChale.ToString())
               .WithAutoProperties()
               .Create();
            var chaleDois = _fixture
               .Build<Chale>()
               .With(x => x.Id, idChaleInexistente.ToString())
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterPorIdAsync(idChale)).ThrowsAsync(new Exception());
            _conversorMock.Setup(x => x.Converter(chaleUm)).Returns(chaleDto);
            var service = new ChaleService(_repositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterChaleAsync(idChale);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Não foi possível realizar a consulta.", false);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task DeveRetornarTodosOsChalesDisponiveisNaDataComSucesso()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Chale>>();
            var _chaleRepositoryMock = new Mock<IChaleRepository>();
            var _conversorMock = new Mock<IConversor<Chale, ChaleDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var dataInicio = _fixture
               .Build<DateTime>()
               .WithAutoProperties()
               .Create();
            var dataFim = _fixture
               .Build<DateTime>()
               .WithAutoProperties()
               .Create();
            var chales = _fixture
               .Build<List<Chale>>()
               .WithAutoProperties()
               .Create();
            var chalesDto = _fixture
               .Build<List<ChaleDto>>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterTodosAsync()).ReturnsAsync(chales);
            _conversorMock.Setup(x => x.ConverterLista(chales)).Returns(chalesDto);
            var service = new ChaleService(_repositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterChalesDisponiveis(dataInicio, dataFim);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Consulta realizada com sucesso.", true, chalesDto);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task NaoDeveRetornarChalesDisponiveisSeForLancadaExcecao()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Chale>>();
            var _chaleRepositoryMock = new Mock<IChaleRepository>();
            var _conversorMock = new Mock<IConversor<Chale, ChaleDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var dataInicio = _fixture
               .Build<DateTime>()
               .WithAutoProperties()
               .Create();
            var dataFim = _fixture
               .Build<DateTime>()
               .WithAutoProperties()
               .Create();
            var chales = _fixture
               .Build<List<Chale>>()
               .WithAutoProperties()
               .Create();
            var chalesDto = _fixture
               .Build<List<ChaleDto>>()
               .WithAutoProperties()
               .Create();

            //Act
            _chaleRepositoryMock.Setup(x => x.ObterChalesDisponiveis(dataInicio, dataFim)).ThrowsAsync(new Exception());
            _conversorMock.Setup(x => x.ConverterLista(chales)).Returns(chalesDto);
            var service = new ChaleService(_repositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterChalesDisponiveis(dataInicio, dataFim);

            //Assert
            var resultadoEsperado = new RespostaGenerica("Não foi possível realizar a consulta.", false);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }


        [Fact]
        public async Task DeveRetornarTodosOsChalesEncontradosComSucesso()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Chale>>();
            var _chaleRepositoryMock = new Mock<IChaleRepository>();
            var _conversorMock = new Mock<IConversor<Chale, ChaleDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var chales = _fixture
               .Build<List<Chale>>()
               .WithAutoProperties()
               .Create();
            var chalesDto = _fixture
               .Build<List<ChaleDto>>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterTodosAsync()).ReturnsAsync(chales);
            _conversorMock.Setup(x => x.ConverterLista(chales)).Returns(chalesDto);
            var service = new ChaleService(_repositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterTodosChalesAsync();

            //Assert
            var resultadoEsperado = new RespostaGenerica("Consulta realizada com sucesso.", true, chalesDto);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task DeveRetornarErroAoLancarExcecaoAoObterTodosClientes()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Chale>>();
            var _chaleRepositoryMock = new Mock<IChaleRepository>();
            var _conversorMock = new Mock<IConversor<Chale, ChaleDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();
            var _fixture = new Fixture();
            var chales = _fixture
               .Build<List<Chale>>()
               .WithAutoProperties()
               .Create();
            var chalesDto = _fixture
               .Build<List<ChaleDto>>()
               .WithAutoProperties()
               .Create();

            //Act
            _repositoryMock.Setup(x => x.ObterTodosAsync()).ThrowsAsync(new Exception());
            _conversorMock.Setup(x => x.ConverterLista(chales)).Returns(chalesDto);
            var service = new ChaleService(_repositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = await service.ObterTodosChalesAsync();

            //Assert
            var resultadoEsperado = new RespostaGenerica("Ocorreu um erro interno ao processar a consulta.", false);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public void DeveRemoverClienteCorretamente()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Chale>>();
            var _chaleRepositoryMock = new Mock<IChaleRepository>();
            var _conversorMock = new Mock<IConversor<Chale, ChaleDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();

            //Act
            _repositoryMock.Setup(x => x.Remover(It.IsAny<Guid>()));
            var service = new ChaleService(_repositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = service.RemoverChale(It.IsAny<Guid>());

            //Assert
            var resultadoEsperado = new RespostaGenerica("Registro removido com sucesso.", true);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public void DeveRetornarErroCasoNaoSejaPossivelRemoverClienteCorretamente()
        {
            //Arrange
            var _repositoryMock = new Mock<IRepository<Chale>>();
            var _chaleRepositoryMock = new Mock<IChaleRepository>();
            var _conversorMock = new Mock<IConversor<Chale, ChaleDto>>();
            var _unitOfWorkMock = new Mock<IUnitOfWork>();

            //Act
            _repositoryMock.Setup(x => x.Remover(It.IsAny<Guid>())).Throws(new Exception());
            var service = new ChaleService(_repositoryMock.Object, _chaleRepositoryMock.Object, _conversorMock.Object, _unitOfWorkMock.Object);
            var resultadoAtual = service.RemoverChale(It.IsAny<Guid>());

            //Assert
            var resultadoEsperado = new RespostaGenerica("O registro não pôde ser removido.", false);
            resultadoAtual.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
