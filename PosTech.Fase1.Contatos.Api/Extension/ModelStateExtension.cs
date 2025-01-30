using Microsoft.AspNetCore.Mvc.ModelBinding;
using PosTech.Fase1.Contatos.Api.Model;

namespace PosTech.Fase1.Contatos.Api.Extension
{
    public static class ModelStateExtension
    {
        public static MensagemErro RetornaErrosMessages(this ModelStateDictionary modelstate)
        {
            return new MensagemErro(modelstate
                .SelectMany(ms => ms.Value!.Errors)
                .Select(e => e.ErrorMessage)
                .ToList());
        }
    }
}
