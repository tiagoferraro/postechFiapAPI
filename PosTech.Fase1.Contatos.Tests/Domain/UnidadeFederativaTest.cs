using PosTech.Fase1.Contatos.Domain.ObjectValue;

namespace PosTech.Fase1.Contatos.Tests.Domain;

public class UnidadeFederativaTest
{
    [Fact]
    public void UnidadeFederativa_Construir_Umparametro()
    {
        //Arrange
        //Action
        var unidadeFederativa = new UnidadeFederativa("SP");
        //Assert
        Assert.Equal("SP",unidadeFederativa.Sigla );
        Assert.Equal("São Paulo",unidadeFederativa.Nome);
    }
    [Fact]
    public void UnidadeFederativa_Construir_DoisParametro()
    {
        //Arrange
        //Action
        var unidadeFederativa = new UnidadeFederativa("SP", "São Paulo 2");
        //Assert
        Assert.Equal("SP", unidadeFederativa.Sigla);
        Assert.Equal("São Paulo 2", unidadeFederativa.Nome);
    }
    [Fact]
    public void UnidadeFederativa_ErroUFInvalida()
    {
        //Arrange
        //Action
        //Assert
        Assert.Throws<InvalidOperationException>(() => new UnidadeFederativa("WW"));

    }
}
