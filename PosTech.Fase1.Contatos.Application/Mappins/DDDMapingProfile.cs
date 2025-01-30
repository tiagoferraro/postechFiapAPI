using AutoMapper;
using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Application.Mappins;

public class DDDMapingProfile : Profile
{
    public DDDMapingProfile()
    {
        CreateMap<DDD, DDDDto>()
            .ConstructUsing(x => new DDDDto()
            {
                DddId = x.DddId,
                Regiao = x.Regiao,
                UfNome = x.UnidadeFederativa.Nome,
                UfSigla = x.UnidadeFederativa.Sigla,
            });
        CreateMap<DDDDto, DDD>()
            .ConstructUsing(x => 
                new DDD(x.DddId, x.UfSigla, x.Regiao));


    }
}

