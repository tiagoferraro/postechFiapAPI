using FluentValidation;
using PosTech.Fase1.Contatos.Application.DTO;

namespace PosTech.Fase1.Contatos.Application.Validators
{

    public class ContatoValidator  : AbstractValidator<ContatoDto>
    {
        public ContatoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Informe o nome do contato")
                .MaximumLength(150);
            
            //RuleFor(x => x.Nome).NotEmpty().MaximumLength(150).WithMessage("Informe o nome do contato");

            RuleFor(x => x.Telefone ?? "" )
                .NotEmpty().WithMessage("Informe o número do telefone de contato") 
                .Must(x => int.TryParse(x.Replace(" ","").Replace("-",""), out var val) && val > 9999999 && val <= 999999999 );

            RuleFor(x => x.Email).NotEmpty().WithMessage("Informe o e-mail do cliente")  
                                .EmailAddress().WithMessage("E-mail inválido");

            RuleFor(x => x.Ativo).Must(x => x == false || x == true).WithMessage("O campo ativo deve ter o valor falso ou true") ;

            RuleFor(x => x.DddId).NotEmpty().WithMessage("o código de área deve ser um inteiro de 2 dígitos.")
                .InclusiveBetween(11,99).WithMessage("o código de área deve ser um inteiro de 2 dígitos.")
                            ;
        }        

    }
}