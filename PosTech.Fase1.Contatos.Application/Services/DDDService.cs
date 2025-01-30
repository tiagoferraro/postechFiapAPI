using AutoMapper;
using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Application.Interfaces;
using PosTech.Fase1.Contatos.Application.Model;
using PosTech.Fase1.Contatos.Application.Result;
using PosTech.Fase1.Contatos.Domain.Entities;
using PosTech.Fase1.Contatos.Infra.Interfaces;

namespace PosTech.Fase1.Contatos.Application.Services;

public class DDDService(IDDDRepository _dddRepository, IMapper _mapper) : IDDDService
{
    public async Task<ServiceResult<DDDDto>> Adicionar(DDDDto c)
    {
        try
        {
            var ddd = _mapper.Map<DDD>(c);
            var dddExiste = await _dddRepository.Obter(c.DddId);
            if (dddExiste is not null)
                return new ServiceResult<DDDDto>(new ValidacaoException("DDD Já Existe"));


            var novoDdd = await _dddRepository.Adicionar(ddd);
            var dddDto = _mapper.Map<DDDDto>(novoDdd);
            return new ServiceResult<DDDDto>(dddDto);

        }
        catch (Exception ex)
        {
            return new ServiceResult<DDDDto>(ex);
        }
    }

    public async Task<ServiceResult<bool>> Atualizar(DDDDto c)
    {
        try
        {
            var ddd = _mapper.Map<DDD>(c);
            await _dddRepository.Atualizar(ddd);
            return new ServiceResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ServiceResult<bool>(ex);
        }
    }

    public async Task<ServiceResult<IEnumerable<DDDDto>>> Listar()
    {
        try
        {
            var ddds = await _dddRepository.Listar();
            var dddsDto = _mapper.Map<IEnumerable<DDDDto>>(ddds);
            return new ServiceResult<IEnumerable<DDDDto>>(dddsDto);
        }
        catch (Exception ex)
        {
            return new ServiceResult<IEnumerable<DDDDto>>(ex);
        }
    }

    public async Task<ServiceResult<DDDDto>> Obter(int dddId)
    {
        try
        {
            var ddd = await _dddRepository.Obter(dddId);
            if (ddd is null)
                return new ServiceResult<DDDDto>(new ValidacaoException("DDD Não Encontrado"));

            var dddDto = _mapper.Map<DDDDto>(ddd);
            return new ServiceResult<DDDDto>(dddDto);
        }
        catch (Exception ex)
        {
            return new ServiceResult<DDDDto>(ex);
        }
    }
}

