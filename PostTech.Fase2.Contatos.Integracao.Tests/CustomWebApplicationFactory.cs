using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PosTech.Fase1.Contatos.Infra.Context;
using PosTech.Fase1.Contatos.IoC;
using PostTech.Fase2.Contatos.Integracao.Tests.Fixture;

namespace PostTech.Fase2.Contatos.Integracao.Tests;

[Collection(nameof(ContextDbCollection))]
public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    public string conectionString { get; set; } = "";
 
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AppDBContext>));

            services.Remove(dbContextDescriptor!);

            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(conectionString);
            });
        });

        builder.UseEnvironment("Development"); 
    }
}

