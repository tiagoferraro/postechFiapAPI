using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Infra.Interfaces;

public interface IContatoAddFila
{
    Task AdicionarAsync(Contato contato);
}

