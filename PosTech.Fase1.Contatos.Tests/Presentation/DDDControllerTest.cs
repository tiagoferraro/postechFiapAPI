using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using PosTech.Fase1.Contatos.Api.Controllers;
using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Application.Interfaces;
using PosTech.Fase1.Contatos.Application.Model;
using PosTech.Fase1.Contatos.Application.Result;

namespace PosTech.Fase1.Contatos.Tests.Presentation;

public class DDDControllerTest
{
    private readonly DDDDto _dddDTO;
    private const string MensagemErro = "Mensagem Erro";
    public DDDControllerTest()
    {
        _dddDTO = new DDDDto(){DddId = 11,Regiao = "São Paulo",UfNome = "São Paulo",UfSigla = "SP"};
    }
    [Fact]
    public async Task ContatosController_AdiconarComSucesso()
    {

        //arrange
        var dddService = new Mock<IDDDService>();
        dddService.Setup(c => c.Adicionar(_dddDTO)).ReturnsAsync(new ServiceResult<DDDDto>(_dddDTO));
        var dddController = new DDDController(dddService.Object);
        //act
        var result = await dddController.Adicionar(_dddDTO);

        //assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<DDDDto>(okResult.Value);

    }
    [Fact]
    public async Task ContatosController_AdiconarComErro()
    {

        //arrange
        var dddService = new Mock<IDDDService>();
        dddService.Setup(c => c.Adicionar(_dddDTO)).ReturnsAsync(new ServiceResult<DDDDto>(new ValidacaoException(MensagemErro)));
        var dddController = new DDDController(dddService.Object);

        //act
        var result = await dddController.Adicionar(_dddDTO);

        //assert

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var retornoMensagemApi = Assert.IsType<ValidacaoException>(badRequestResult.Value);

        Assert.Equal(MensagemErro, retornoMensagemApi.Message);
    }
    [Fact]
    public async Task ContatosController_AtualizarComSucesso()
    {

        //arrange
        var dddService = new Mock<IDDDService>();
        dddService.Setup(c => c.Atualizar(_dddDTO)).ReturnsAsync(new ServiceResult<bool>(true));
        var dddController = new DDDController(dddService.Object);
        //act
        var result = await dddController.Atualizar(_dddDTO);

        //assert
        Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public async Task ContatosController_AtualizarComErro()
    {

        //arrange
        var dddService = new Mock<IDDDService>();

        dddService.Setup(c => c.Atualizar(_dddDTO)).ReturnsAsync(new ServiceResult<bool>(new ValidacaoException(MensagemErro)));
        var dddController = new DDDController(dddService.Object);
        //act
        var result = await dddController.Atualizar(_dddDTO);

        //assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var retornoMensagemApi = Assert.IsType<ValidacaoException>(badRequestResult.Value);
        Assert.Equal(MensagemErro, retornoMensagemApi.Message);
    }
  
    [Fact]
    public async Task ContatosController_listarComSucesso()
    {

        //arrange
        var dddService = new Mock<IDDDService>();


        dddService.Setup(c => c.Listar()).ReturnsAsync(new ServiceResult<IEnumerable<DDDDto>>((new List<DDDDto>() { _dddDTO })));
        var dddController = new DDDController(dddService.Object);
        //act
        var result = await dddController.Listar();

        //assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var listaRetornada = (IEnumerable<DDDDto>)okResult.Value!;
        Assert.Single(listaRetornada);
    }
    [Fact]
    public async Task ContatosController_listarComErro()
    {

        //arrange
        var dddService = new Mock<IDDDService>();


        dddService.Setup(c => c.Listar()).ReturnsAsync(new ServiceResult<IEnumerable<DDDDto>>(new ValidacaoException(MensagemErro)));
        var dddController = new DDDController(dddService.Object);
        //act
        var result = await dddController.Listar();

        //assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var retornoMensagemApi = Assert.IsType<ValidacaoException>(badRequestResult.Value);
        Assert.Equal(MensagemErro, retornoMensagemApi.Message);
    }
    [Fact]
    public async Task ContatosController_ObterComSucesso()
    {

        //arrange
        var dddService = new Mock<IDDDService>();

        const int contatoId = 1;
        dddService.Setup(c => c.Obter(contatoId)).ReturnsAsync(new ServiceResult<DDDDto>(_dddDTO));
        var dddController = new DDDController(dddService.Object);
        //act
        var result = await dddController.Obter(contatoId);

        //assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<DDDDto>(okResult.Value);


    }
    [Fact]
    public async Task ContatosController_ObterComErro()
    {

        //arrange
        var dddService = new Mock<IDDDService>();

        const int contatoId = 1;
        dddService.Setup(c => c.Obter(contatoId)).ReturnsAsync(new ServiceResult<DDDDto>(new ValidacaoException(MensagemErro)));
        var dddController = new DDDController(dddService.Object);
        //act
        var result = await dddController.Obter(contatoId);

        //assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var retornoMensagemApi = Assert.IsType<ValidacaoException>(badRequestResult.Value);
        Assert.Equal(MensagemErro, retornoMensagemApi.Message);
    }
    

}

