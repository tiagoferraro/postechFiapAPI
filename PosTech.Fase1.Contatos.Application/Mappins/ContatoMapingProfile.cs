using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Application.Mappins;

public class ContatoMapingProfile : Profile
{
    public ContatoMapingProfile()
    {
        CreateMap<Contato, ContatoDto>()
            .ConstructUsing(x => new ContatoDto()
            {
                DddId = x.DddId,
                Nome = x.Nome,
                Ativo = x.Ativo,
                ContatoId = x.ContatoId,
                Email = x.Email,
                Telefone = x.Telefone,
                Ddd = x.Ddd == null ? null : new DDDDto() { UfNome = x.Ddd.UnidadeFederativa.Nome, UfSigla = x.Ddd.UnidadeFederativa.Sigla, DddId = x.DddId, Regiao = x.Ddd.Regiao }
            });
        CreateMap<ContatoDto, Contato>()
            .ConstructUsing(x =>
                new Contato(x.ContatoId,x.Nome,x.Telefone,x.Email,x.DddId));
        CreateMap<ContatoDto, Contato>()
            .ConvertUsing(x =>
               new Contato(x.ContatoId,x.Nome,x.Telefone,x.Email,x.DddId));
    }
}

