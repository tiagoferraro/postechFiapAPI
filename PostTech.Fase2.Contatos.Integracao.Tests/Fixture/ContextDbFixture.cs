﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using PosTech.Fase1.Contatos.Infra.Context;
using Testcontainers.MsSql;

namespace PostTech.Fase2.Contatos.Integracao.Tests.Fixture;

[CollectionDefinition(nameof(ContextDbCollection))]
public class ContextDbCollection : ICollectionFixture<ContextDbFixture>;

public class ContextDbFixture : IAsyncLifetime
{

    public AppDBContext? Context { get; private set; }
    public string sqlConection { get; private set; } = "";
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPortBinding(1434, true) // Changed port to 1434
        .Build();
    public async Task InitializeAsync()
    {

        await _msSqlContainer.StartAsync();
        sqlConection = _msSqlContainer.GetConnectionString();
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseSqlServer(sqlConection)
            .Options;

        Context = new AppDBContext(options);
        await Context.Database.MigrateAsync();


    }
    public void IncializaDadosContatos()
    {
        Context!.Database.ExecuteSqlRaw("DELETE FROM CONTATO");
        Context!.Database.ExecuteSqlRaw("DELETE FROM DDD");
        Context!.Database.ExecuteSqlRaw("INSERT DDD(DddId,Regiao,UfSigla,UfNome) VALUES (11,'São Paulo','SP','São Paulo') , (12,'S. José dos Campos','SP','São Paulo')");
    }
    public async Task DisposeAsync()
    {
        await _msSqlContainer.StopAsync();
    }

  

}


