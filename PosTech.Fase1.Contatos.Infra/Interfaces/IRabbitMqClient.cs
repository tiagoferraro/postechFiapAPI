namespace PosTech.Fase1.Contatos.Infra.Interfaces;

public interface IRabbitMqClient
{
    Task SendMessage(string message, string exchange);
}

