using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PosTech.Fase1.Contatos.Api.Extension;
using PosTech.Fase1.Contatos.Application.Model;
using System.Net;

namespace PosTech.Fase1.Contatos.Api.Filter;

public class ModelStateValidatorFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        
        if (context.Result is BadRequestObjectResult { Value: ValidacaoException } result)
        {
            context.Result = new BadRequestObjectResult(((ValidacaoException)(result.Value)).Message.ConverteParaErro());
        }
        if (context.Result is BadRequestObjectResult { Value: Exception })
        {
            context.Result = new  StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if(!context.ModelState.IsValid)
        {
            var erros = context.ModelState.RetornaErrosMessages();
            context.Result = new BadRequestObjectResult(erros);
        }

   

    }
}


