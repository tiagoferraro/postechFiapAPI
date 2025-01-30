using Microsoft.Extensions.Configuration;
using PosTech.Fase1.Contatos.Domain.Entities;
using PosTech.Fase1.Contatos.Infra.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PosTech.Fase1.Contatos.Infra.Messaging;

public class ContatoDeleteFila(
IRabbitMqClient _rabbitMqClient,
IConfiguration _configuration
) : IContatoDeleteFila
{
    public async Task DeletarAsync(Contato contato)
    {
        var rabbitMqConfig = _configuration.GetSection("RabbitMq");
        var mensagem = JsonConvert.SerializeObject(contato, Formatting.Indented, new JsonSerializerSettings(){NullValueHandling = NullValueHandling.Ignore});

        await _rabbitMqClient.SendMessage(mensagem, rabbitMqConfig["ExchangeDelete"]);
    }
}


