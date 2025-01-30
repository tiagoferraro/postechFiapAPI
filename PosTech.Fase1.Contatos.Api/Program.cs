using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PosTech.Fase1.Contatos.Api.Filter;
using PosTech.Fase1.Contatos.IoC;
using Prometheus;

#pragma warning disable S1118

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.
    AddControllers(options => options.Filters
    .Add(typeof(ModelStateValidatorFilter)))
    .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AdicionarDependencias();
builder.Services.AdicionarDBContext(configuration);

builder.Services.AddSwaggerGen();

builder.Services.Configure<AspNetCoreTraceInstrumentationOptions>(options =>
{
    // Filter out instrumentation of the Prometheus scraping endpoint.
    options.Filter = ctx => ctx.Request.Path != "/metrics";
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(b =>
    {
        b.AddService("PostechFase2");
    })
    .WithTracing(b => b
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter())
    .WithMetrics(b => b
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddRuntimeInstrumentation()
        .AddProcessInstrumentation()
        .AddPrometheusExporter());

builder.Services.UseHttpClientMetrics();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}



// Configure the HTTP request pipeline.
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapMetrics();

app.UseMetricServer();
app.UseHttpMetrics();
app.UseOpenTelemetryPrometheusScrapingEndpoint();

await app.RunAsync();

public partial class Program { }