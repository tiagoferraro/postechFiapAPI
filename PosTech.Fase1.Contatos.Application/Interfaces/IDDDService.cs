using PosTech.Fase1.Contatos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Application.Result;

namespace PosTech.Fase1.Contatos.Application.Interfaces
{
    public interface IDDDService
    {
        Task<ServiceResult<DDDDto>> Adicionar(DDDDto c);
        Task<ServiceResult<bool>> Atualizar(DDDDto c);
        Task<ServiceResult<IEnumerable<DDDDto>>> Listar();
        Task<ServiceResult<DDDDto>> Obter(int DDDId);
    }
}
