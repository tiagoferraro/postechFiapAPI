using AutoMapper;
using Microsoft.Extensions.WebEncoders.Testing;
using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Application.Interfaces;
using PosTech.Fase1.Contatos.Application.Model;
using PosTech.Fase1.Contatos.Application.Result;
using PosTech.Fase1.Contatos.Domain.Entities;
using PosTech.Fase1.Contatos.Infra.Interfaces;

namespace PosTech.Fase1.Contatos.Application.Services;

public class ContatoService(
    IContatoRepository _contatoRepository,
    IMapper _mapper,
    IDDDRepository _dddRepository,
    IContatoAddFila _contatoAddFila,
    IContatoUpdateFila _contatoUpdateFila,
    IContatoDeleteFila _contatoDeleteFila
    ) : IContatoService
{
    public async Task<ServiceResult<ContatoDto>> Adicionar(ContatoDto c)
    {
        try
        {
            var contato = _mapper.Map<Contato>(c);

            var ddd = await _dddRepository.Obter(c.DddId);
            if (ddd is null)
                return new ServiceResult<ContatoDto>(new ValidacaoException("DDD não existe"));


            if (await _contatoRepository.Existe(contato))
                return new ServiceResult<ContatoDto>(new ValidacaoException("Cadastro de contato ja existe"));
    
            await _contatoAddFila.AdicionarAsync(contato);

            return new ServiceResult<ContatoDto>(_mapper.Map<ContatoDto>(contato));
        }
        catch (Exception ex)
        {
            return new ServiceResult<ContatoDto>(ex);
        }
    }

    public async Task<ServiceResult<bool>> Atualizar(ContatoDto c)
    {
        try
        {
            var ddd = await _dddRepository.Obter(c.DddId);
            if (ddd is null)
                return new ServiceResult<bool>(new ValidacaoException("DDD não existe"));

            var contatoExiste = await _contatoRepository.Obter(c.ContatoId!.Value);
            if (contatoExiste is null)
                return new ServiceResult<bool>(new ValidacaoException("Contato não existe"));

            var contato = _mapper.Map<Contato>(c);
            await _contatoUpdateFila.AtualizarAsync(contato);

            return new ServiceResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ServiceResult<bool>(ex);
        }
    }

    public async Task<ServiceResult<bool>> Excluir(Guid contatoId)
    {
        try
        {
            var contato = await _contatoRepository.Obter(contatoId);
            if (contato is null)
                return new ServiceResult<bool>(new ValidacaoException("Contato não existe"));
            contato.DesativarContato();
            await _contatoDeleteFila.DeletarAsync(contato);

            return new ServiceResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ServiceResult<bool>(ex);
        }
    }

    public async Task<ServiceResult<IEnumerable<ContatoDto>>> Listar()
    {
        try
        {
            var contatos = await _contatoRepository.Listar();
            var listaContatosDto = _mapper.Map<IEnumerable<Contato>, IEnumerable<ContatoDto>>(contatos);

            return new ServiceResult<IEnumerable<ContatoDto>>(listaContatosDto);
        }
        catch (Exception ex)
        {
            return new ServiceResult<IEnumerable<ContatoDto>>(ex);
        }
    }

    public async Task<ServiceResult<IEnumerable<ContatoDto>>> ListarComDdd(int ddd)
    {
        try
        {
            var contatos = await _contatoRepository.ListarComDDD(ddd);
            var listaContatosDto = _mapper.Map<IEnumerable<ContatoDto>>(contatos);
            return new ServiceResult<IEnumerable<ContatoDto>>(listaContatosDto);
        }
        catch (Exception ex)
        {
            return new ServiceResult<IEnumerable<ContatoDto>>(ex);
        }
    }

    public async Task<ServiceResult<ContatoDto>> Obter(Guid contatoId)
    {
        try
        {
            var contato = await _contatoRepository.Obter(contatoId);
            if (contato is null)
                return new ServiceResult<ContatoDto>(new ValidacaoException("Contato não encontrado"));

            var contatoDto = _mapper.Map<ContatoDto>(contato);
            return new ServiceResult<ContatoDto>(contatoDto);
        }
        catch (Exception ex)
        {
            return new ServiceResult<ContatoDto>(ex);
        }
    }
}

