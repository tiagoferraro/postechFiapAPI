using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Infra.Interfaces
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> Listar();
        Task<IEnumerable<Contato>> ListarComDDD(int DDD);
        Task<Contato?> Obter(Guid ContatoId);
        Task<bool> Existe(Contato c);
    }
}
