using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Application.Result;

namespace PosTech.Fase1.Contatos.Application.Interfaces
{
    public interface IContatoService
    {
        Task<ServiceResult<ContatoDto>> Adicionar(ContatoDto c);
        Task<ServiceResult<bool>> Atualizar(ContatoDto c);
        Task<ServiceResult<bool>> Excluir(Guid contatoId);
        Task<ServiceResult<IEnumerable<ContatoDto>>> Listar();
        Task<ServiceResult<IEnumerable<ContatoDto>>> ListarComDdd(int ddd);
        Task<ServiceResult<ContatoDto>> Obter(Guid contatoId);


    }
}
