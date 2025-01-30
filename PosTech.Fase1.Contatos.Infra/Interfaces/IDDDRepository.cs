using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Infra.Interfaces;

public interface IDDDRepository
{
    Task<DDD> Adicionar(DDD d);
    Task Atualizar(DDD d);
    Task<IEnumerable<DDD>> Listar();
    Task<DDD?> Obter(int DDDId);
}

