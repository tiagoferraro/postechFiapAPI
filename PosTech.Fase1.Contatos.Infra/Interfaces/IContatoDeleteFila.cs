using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Infra.Interfaces;

public interface IContatoDeleteFila
{
    Task DeletarAsync(Contato contato);
}

