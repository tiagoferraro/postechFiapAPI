using Microsoft.EntityFrameworkCore;
using PosTech.Fase1.Contatos.Domain.Entities;
using PosTech.Fase1.Contatos.Infra.Context;
using PosTech.Fase1.Contatos.Infra.Repository;
using PostTech.Fase2.Contatos.Integracao.Tests.Fixture;
using Xunit.Extensions.Ordering;

namespace PostTech.Fase2.Contatos.Integracao.Tests.Infra;
[Collection(nameof(ContextDbCollection)), Order(2)]
public class ContatoRepositoryTest
{
    private readonly AppDBContext context;
    private readonly ContatoRepository repository;
    public ContatoRepositoryTest(ContextDbFixture fixture)
    {
        context = fixture.Context!;
        repository = new ContatoRepository(context);
        fixture.IncializaDadosContatos();


    }

  


    [Fact]
    public async Task Listar_DeveRetornarContatosAtivos()
    {

        var contato = new Contato(null, "Nome 1", "11999878587", "teste@email.com.br", 11);
        contato.DesativarContato();
        context.Contatos.Add(contato);

        context.Contatos.Add(new Contato(null, "Nome 3", "11999878582", "teste2@email.com.br", 12));
        await context.SaveChangesAsync();

        var result = await repository.Listar();

        Assert.Single(result);
        Assert.Equal("Nome 3", result.First().Nome);
    }

    [Fact]
    public async Task ListarComDDD_DeveRetornarContatosComDDD()
    {

        context.Contatos.Add(new Contato(null, "Nome 3", "11999878582", "teste2@email.com.br", 12));
        context.Contatos.Add(new Contato(null, "Nome 1", "11999878587", "teste@email.com.br", 11));
        await context.SaveChangesAsync();

        var result = await repository.ListarComDDD(11);

        Assert.Single(result);
        Assert.Equal(11, result.First().DddId);
    }

    [Fact]
    public async Task Obter_DeveRetornarContatoPorId()
    {

        var contato = new Contato(null, "Nome 3", "11999878582", "teste2@email.com.br", 12);
        context.Contatos.Add(contato);
        await context.SaveChangesAsync();

        var result = await repository.Obter(contato.ContatoId!.Value);

        Assert.NotNull(result);
        Assert.Equal(contato.ContatoId, result.ContatoId);
    }

    [Fact]
    public async Task Existe_DeveRetornarTrueSeContatoExiste()
    {
        var contato = new Contato(null, "Nome 3", "11999878582", "teste2@email.com.br", 12);
        context.Contatos.Add(contato);
        await context.SaveChangesAsync();

        var result = await repository.Existe(contato);

        Assert.True(result);
    }
}

