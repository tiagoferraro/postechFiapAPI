using Microsoft.EntityFrameworkCore;
using PosTech.Fase1.Contatos.Domain.Entities;
using PosTech.Fase1.Contatos.Infra.Context;
using PosTech.Fase1.Contatos.Infra.Repository;
using PostTech.Fase2.Contatos.Integracao.Tests.Fixture;
using Xunit.Extensions.Ordering;


namespace PostTech.Fase2.Contatos.Integracao.Tests.Infra;

[Collection(nameof(ContextDbCollection)), Order(1)]
public class DDDRepositoryTest
{
    private readonly AppDBContext context;
    private readonly DDDRepository repository;
    public DDDRepositoryTest(ContextDbFixture fixture)
    {
        context = fixture.Context!;
        repository = new DDDRepository(context);
        context.Database.ExecuteSqlRaw("DELETE FROM DDD");
    }

    [Fact]
    public async Task DDDRepository_AdicionarDDD_ComSucesso()
    {
        // Arrange
        var ddd = new DDD(11, "SP", "São Paulo");

        // Act
        await repository.Adicionar(ddd);
        // Assert
        var dddAdicionado = await repository.Obter(ddd.DddId);
        Assert.NotNull(dddAdicionado);
        Assert.Equal(ddd.DddId, dddAdicionado.DddId);
        Assert.Equal(ddd.Regiao, dddAdicionado.Regiao);
    }

    [Fact]
    public async Task DDDRepository_ObterDDD_ComSucesso()
    {
        // Arrange
        var ddd = new DDD(21, "RJ", "Rio de Janeiro");

        await repository.Adicionar(ddd);
        // Act
        var dddObtido = await repository.Obter(ddd.DddId);
        // Assert
        Assert.NotNull(dddObtido);
        Assert.Equal(ddd.DddId, dddObtido.DddId);
        Assert.Equal(ddd.Regiao, dddObtido.Regiao);
    }

    [Fact]
    public async Task DDDRepository_AtualizarDDD_ComSucesso()
    {
        // Arrange
        var ddd = new DDD(31, "MG", "Minas Gerais");

        await repository.Adicionar(ddd);
        context.Entry(ddd).State = EntityState.Detached;

        ddd = new DDD(31, "MG", "Belo Horizonte");

        // Act
        await repository.Atualizar(ddd);
        var dddAtualizado = await repository.Obter(ddd.DddId);
        // Assert
        Assert.NotNull(dddAtualizado);
        Assert.Equal("Belo Horizonte", dddAtualizado.Regiao);
    }
    [Fact]
    public async Task DDDRepository_ListarDDD_ComSucesso()
    {

        // Arrange
        var ddd = new DDD(32, "MG", "Minas Gerais");
        await repository.Adicionar(ddd);
        context.Entry(ddd).State = EntityState.Detached;
        var ddd2 = new DDD(33, "SP", "São Paulo");
        await repository.Adicionar(ddd2);
        context.Entry(ddd2).State = EntityState.Detached;
        // Act
        var lista = await repository.Listar();

        // Assert
        Assert.NotNull(lista);
        Assert.Equal(2, lista.Count());
    }

}

