using Microsoft.AspNetCore.Mvc;
using Moq;
using PosTech.Fase1.Contatos.Api.Controllers;
using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Application.Interfaces;
using PosTech.Fase1.Contatos.Application.Model;
using PosTech.Fase1.Contatos.Application.Result;

namespace PosTech.Fase1.Contatos.Tests.Presentation;

public class ContatosControllerTest
{

    //private readonly ContatoDTO _contatoDTO;
    //private const string MensagemErro = "Mensagem Erro";

    //public ContatosControllerTest()
    //{
    //    _contatoDTO = new ContatoDTO() { Ativo = true, ContatoId = 1, DddId = 11, Email = "teste@teste@gmail.com", Nome = "Tiago", Telefone = "62138587" };

    //}
    //[Fact]
    //public async Task ContatosController_AdiconarComSucesso()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();
    //    contatoService.Setup(c => c.AdicionarAsync(_contatoDTO)).ReturnsAsync(new ServiceResult<ContatoDTO>(_contatoDTO));
    //    var contatoController = new ContatosController(contatoService.Object);
    //    //act
    //    var result = await contatoController.AdicionarAsync(_contatoDTO);

    //    //assert
    //    var okResult = Assert.IsType<OkObjectResult>(result);
    //    Assert.IsType<ContatoDTO>(okResult.Value);

    //}
    //[Fact]
    //public async Task ContatosController_AdiconarComErro()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();
    //    contatoService.Setup(c => c.AdicionarAsync(_contatoDTO)).ReturnsAsync(new ServiceResult<ContatoDTO>(new ValidacaoException(MensagemErro)));
    //    var contatoController = new ContatosController(contatoService.Object);

    //    //act
    //    var result = await contatoController.AdicionarAsync(_contatoDTO);

    //    //assert

    //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
    //    var retornoMensagemApi = Assert.IsType<ValidacaoException>(badRequestResult.Value);

    //    Assert.Equal(MensagemErro, retornoMensagemApi.Message);
    //}
    //[Fact]
    //public async Task ContatosController_AtualizarComSucesso()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();
    //    contatoService.Setup(c => c.Atualizar(_contatoDTO)).ReturnsAsync(new ServiceResult<bool>(true));
    //    var contatoController = new ContatosController(contatoService.Object);
    //    //act
    //    var result = await contatoController.Atualizar(_contatoDTO);

    //    //assert
    //    Assert.IsType<NoContentResult>(result);
    //}
    //[Fact]
    //public async Task ContatosController_AtualizarComErro()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();

    //    contatoService.Setup(c => c.Atualizar(_contatoDTO)).ReturnsAsync(new ServiceResult<bool>(new ValidacaoException(MensagemErro)));
    //    var contatoController = new ContatosController(contatoService.Object);
    //    //act
    //    var result = await contatoController.Atualizar(_contatoDTO);

    //    //assert
    //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
    //    var retornoMensagemApi = Assert.IsType<ValidacaoException>(badRequestResult.Value);
    //    Assert.Equal(MensagemErro, retornoMensagemApi.Message);
    //}
    //[Fact]
    //public async Task ContatosController_ExcluirComSucesso()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();
    //    var contatoId = 1;

    //    contatoService.Setup(c => c.Excluir(contatoId)).ReturnsAsync(new ServiceResult<bool>(true));
    //    var contatoController = new ContatosController(contatoService.Object);
    //    //act
    //    var result = await contatoController.Excluir(contatoId);

    //    //assert
    //    Assert.IsType<NoContentResult>(result);
    //}
    //[Fact]
    //public async Task ContatosController_ExcluirComErro()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();
    //    var contatoId = 1;

    //    contatoService.Setup(c => c.Excluir(contatoId)).ReturnsAsync(new ServiceResult<bool>(new ValidacaoException(MensagemErro)));
    //    var contatoController = new ContatosController(contatoService.Object);
    //    //act
    //    var result = await contatoController.Excluir(contatoId);

    //    //assert
    //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
    //    var retornoMensagemApi = Assert.IsType<ValidacaoException>(badRequestResult.Value);
    //    Assert.Equal(MensagemErro, retornoMensagemApi.Message);
    //}
    //[Fact]
    //public async Task ContatosController_listarComSucesso()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();


    //    contatoService.Setup(c => c.Listar()).ReturnsAsync(new ServiceResult<IEnumerable<ContatoDTO>>((new List<ContatoDTO>() { _contatoDTO })));
    //    var contatoController = new ContatosController(contatoService.Object);
    //    //act
    //    var result = await contatoController.Listar();

    //    //assert
    //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
    //    var  listaRetornada = (IEnumerable<ContatoDTO>)okResult.Value!;
    //    Assert.Single( listaRetornada);
    //}
    //[Fact]
    //public async Task ContatosController_listarComErro()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();


    //    contatoService.Setup(c => c.Listar()).ReturnsAsync(new ServiceResult<IEnumerable<ContatoDTO>>(new ValidacaoException(MensagemErro)));
    //    var contatoController = new ContatosController(contatoService.Object);
    //    //act
    //    var result = await contatoController.Listar();

    //    //assert
    //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
    //    var retornoMensagemApi = Assert.IsType<ValidacaoException>(badRequestResult.Value);
    //    Assert.Equal(MensagemErro, retornoMensagemApi.Message);
    //}
    //[Fact]
    //public async Task ContatosController_ObterComSucesso()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();

    //    const int contatoId = 1;
    //    contatoService.Setup(c => c.Obter(contatoId)).ReturnsAsync(new ServiceResult<ContatoDTO>( _contatoDTO));
    //    var contatoController = new ContatosController(contatoService.Object);
    //    //act
    //    var result = await contatoController.Obter(contatoId);

    //    //assert
    //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
    //    Assert.IsType<ContatoDTO>(okResult.Value);
        
        
    //}
    //[Fact]
    //public async Task ContatosController_ObterComErro()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();

    //    const int contatoId = 1;
    //    contatoService.Setup(c => c.Obter(contatoId)).ReturnsAsync(new ServiceResult<ContatoDTO>(new ValidacaoException(MensagemErro)));
    //    var contatoController = new ContatosController(contatoService.Object);
    //    //act
    //    var result = await contatoController.Obter(contatoId);

    //    //assert
    //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
    //    var retornoMensagemApi = Assert.IsType<ValidacaoException>(badRequestResult.Value);
    //    Assert.Equal(MensagemErro, retornoMensagemApi.Message);
    //}
    //[Fact]
    //public async Task ContatosController_listarFiltroDDDComSucesso()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();

    //    var dddId = 11;
    //    contatoService.Setup(c => c.ListarComDDD(dddId)).ReturnsAsync(new ServiceResult<IEnumerable<ContatoDTO>>((new List<ContatoDTO>() { _contatoDTO })));
    //    var contatoController = new ContatosController(contatoService.Object);
    //    //act
    //    var result = await contatoController.ListarComDDD(dddId);

    //    //assert
    //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
    //    var listaRetornada = (IEnumerable<ContatoDTO>)okResult.Value!;
    //    Assert.Single( listaRetornada);
    //}
    //[Fact]
    //public async Task ContatosController_listarFiltroDDDComErro()
    //{

    //    //arrange
    //    var contatoService = new Mock<IContatoService>();

    //    var dddId = 11;
    //    contatoService.Setup(c => c.ListarComDDD(dddId)).ReturnsAsync(new ServiceResult<IEnumerable<ContatoDTO>>(new ValidacaoException(MensagemErro)));
    //    var contatoController = new ContatosController(contatoService.Object);
    //    //act
    //    var result = await contatoController.ListarComDDD(dddId);

    //    //assert
    //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
    //    var retornoMensagemApi = Assert.IsType<ValidacaoException>(badRequestResult.Value);
    //    Assert.Equal(MensagemErro, retornoMensagemApi.Message);
    //}
}

