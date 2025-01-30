
using Microsoft.Extensions.Configuration;
using PosTech.Fase1.Contatos.Domain.Entities;
using PosTech.Fase1.Contatos.Infra.Interfaces;
using Newtonsoft.Json;

namespace PosTech.Fase1.Contatos.Infra.Messaging;

public class ContatoUpdateFila(
      IRabbitMqClient _rabbitMqClient,
    IConfiguration _configuration
    ) : IContatoUpdateFila
{
    public async Task AtualizarAsync(Contato contato)
    {
        var rabbitMqConfig = _configuration.GetSection("RabbitMq");
        var mensagem = JsonConvert.SerializeObject(contato, Formatting.Indented, new JsonSerializerSettings(){NullValueHandling = NullValueHandling.Ignore});

        await _rabbitMqClient.SendMessage(mensagem, rabbitMqConfig["ExchangeUpdate"]);
    }
}

