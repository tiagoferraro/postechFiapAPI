using Microsoft.AspNetCore.Mvc.ModelBinding;
using PosTech.Fase1.Contatos.Api.Extension;

namespace PosTech.Fase1.Contatos.Tests.Presentation;

public class ExtensionsTest
{
    [Fact]
    public void ErrorExtension_PreecherComSucesso()
    {
        //arrange
        var mensagemErro = "esta é uma mensagem de erro";
        //action
        var listaErros = mensagemErro.ConverteParaErro();
        //assert
        Assert.Equal(listaErros.mensagemErro.First(),mensagemErro);
    }
    [Fact]
    public void ModelStateExtension_PreecherComSucesso()
    {

        //arrange
        var mensagemErro = "esta é uma mensagem de erro";
        var modelStateDictionary = new ModelStateDictionary();
        modelStateDictionary.AddModelError("Error",mensagemErro);
        //action
        var listaErros = modelStateDictionary.RetornaErrosMessages();
        //assert
        Assert.Equal(listaErros.mensagemErro.First(), mensagemErro);
    }

}

