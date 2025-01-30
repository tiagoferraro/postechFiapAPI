using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Infra.Context;
using PostTech.Fase2.Contatos.Integracao.Tests.Fixture;
using Xunit.Extensions.Ordering;

namespace PostTech.Fase2.Contatos.Integracao.Tests.Api;
[Order(3)]
public class DDDControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>,IClassFixture<ContextDbFixture>
{
    private readonly HttpClient _client;
    private readonly AppDBContext _context;


    public DDDControllerTest(CustomWebApplicationFactory<Program> factory, ContextDbFixture contextDbFixture)
    {
        factory.conectionString = contextDbFixture.sqlConection;
        _context = contextDbFixture.Context! ;
            
           
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = true
        });

    }
    [Fact,Order(1)]
    public async Task DDDController_Adicionar_DeveAdicionarDDD()
    {
        // Arrange
        _context.Database.ExecuteSqlRaw("DELETE FROM DDD");
        var response = await _client.PostAsJsonAsync("/api/ddd", new DDDDto(){DddId = 11,Regiao = "São Paulo",UfSigla = "SP"});
        response.EnsureSuccessStatusCode();
        // Act
        var responseString = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Contains("SP", responseString);
    }
    [Fact,Order(2)]
    public async Task DDDController_ObterTodos_DeveRetornarListaDeDDDs()
    {
        // Arrange
        var response = await _client.GetAsync("/api/ddd");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Contains("SP", responseString);
    }
    [Fact, Order(3)]
    public async Task DDDController_Obter_DeveRetornarDDD()
    {
        // Arrange
        var response = await _client.GetAsync("/api/ddd/11");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Contains("SP", responseString);
    }
    [Fact, Order(4)]
    public async Task DDDController_Atualizar_DeveAtualizarDDD()
    {
        // Arrange
        var response = await _client.PutAsJsonAsync("/api/ddd", new DDDDto() { DddId = 11, Regiao = "São Paulo", UfSigla = "SP" });
        response.EnsureSuccessStatusCode();
        // Assert
       Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
   
    [Fact, Order(6)]
    public async Task DDDController_Obter_DeveRetornarDDDInexistente()
    {
        // Arrange
        var response = await _client.GetAsync("/api/ddd/12");
        var responseString = await response.Content.ReadAsStringAsync();
        // Assert
        Assert.Contains("DDD não encontrado", responseString,StringComparison.OrdinalIgnoreCase);
    }
}

