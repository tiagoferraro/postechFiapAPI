using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Infra.Interfaces;
public interface IContatoUpdateFila
{
    Task AtualizarAsync(Contato contato);
}
