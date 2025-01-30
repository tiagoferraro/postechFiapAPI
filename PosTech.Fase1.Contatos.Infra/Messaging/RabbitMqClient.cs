using System.Text;
using Polly.Retry;
using Polly;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using PosTech.Fase1.Contatos.Infra.Interfaces;

namespace PosTech.Fase1.Contatos.Infra.Messaging;

public class RabbitMqClient : IRabbitMqClient
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly AsyncRetryPolicy _retryPolicy;

    public RabbitMqClient(IConfiguration configuration)
    {
        var rabbitMqConfig = configuration.GetSection("RabbitMq");
        _connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConfig["HostName"],
            UserName = rabbitMqConfig["UserName"],
            Password = rabbitMqConfig["Password"]
        };

        // Configura uma política de retry com Polly
        _retryPolicy = Policy
            .Handle<BrokerUnreachableException>() // Reexecuta em caso de falha ao conectar ao broker
            .Or<AlreadyClosedException>()        // Ou se a conexão for encerrada prematuramente
            .Or<Exception>()                     // Ou qualquer outra exceção
            .WaitAndRetryAsync(
                retryCount: 3,                   // Número de tentativas de retry
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(2 * attempt), // Tempo entre tentativas
                onRetry: (exception, timespan, retryAttempt, context) =>
                {
                    Console.WriteLine($"Tentativa de envio {retryAttempt} falhou. Tentando novamente em {timespan.Seconds} segundos devido a: {exception.Message}");
                }
            );
    }

    public async Task SendMessage(string message, string exchange)
    {
        // Executa o envio com a política de retry
        await _retryPolicy.ExecuteAsync(async () =>
        {
            await using var connection = await _connectionFactory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            var body = Encoding.UTF8.GetBytes(message);


            // Publica a mensagem na fila
            await channel.BasicPublishAsync(
                                exchange: exchange,
                                routingKey: "",
                                body: body);

            Console.WriteLine($"Mensagem enviada: {message}");
        });
    }
}

