using FluentValidation;
using PosTech.Fase1.Contatos.Application.DTO;

namespace PosTech.Fase1.Contatos.Application.Validators;

public class DDDValidator : AbstractValidator<DDDDto>
{
    public DDDValidator()
    {

        RuleFor(x => x.UfSigla)
            .Must(ufSigla => !string.IsNullOrEmpty(ufSigla) && ufSigla.Length == 2)
            .WithMessage("UfSigla precisa ser informada e conter exatamente 2 caracteres ex:SP");

        RuleFor(x => x.Regiao)
            .Must(regiao => !string.IsNullOrEmpty(regiao) && regiao.Length <= 100)
            .WithMessage("UfRegiao é a principal cidade da área de abrangência, precisa ser informada, e conter no máximo 100 caracteres");

    }
}

