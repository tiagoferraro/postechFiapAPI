using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using PosTech.Fase1.Contatos.Api.Filter;
using PosTech.Fase1.Contatos.Application.Model;
using PosTech.Fase1.Contatos.Api.Extension;
using System.Net;
using Xunit;

namespace PosTech.Fase1.Contatos.Tests.Presentation
{
    public class ModelStateValidatorFilterTests
    {
        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext();
            return new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
                ActionDescriptor = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor(),
            };
        }

        [Fact]
        public void OnActionExecuting_ModelStateInvalid_BadRequestResult()
        {
            // Arrange
            var context = new ActionExecutingContext(
                GetActionContext(),
                new List<IFilterMetadata>(),
                new Dictionary<string, object>()!,
                new Mock<Controller>().Object
            );
            context.ModelState.AddModelError("Error", "Invalid model state");

            var filter = new ModelStateValidatorFilter();

            // Act
            filter.OnActionExecuting(context);

            // Assert
            var result = Assert.IsType<BadRequestObjectResult>(context.Result);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void OnActionExecuting_ModelStateValid_SemResultado()
        {
            // Arrange
            var context = new ActionExecutingContext(
                GetActionContext(),
                new List<IFilterMetadata>(),
                new Dictionary<string, object>()!,
                new Mock<Controller>().Object
            );

            var filter = new ModelStateValidatorFilter();

            // Act
            filter.OnActionExecuting(context);

            // Assert
            Assert.Null(context.Result);
        }

        [Fact]
        public void OnActionExecuted_BadRequestWithValidacaoException_BadRequestWithComConversor()
        {
            // Arrange
            var validacaoException = new ValidacaoException("Validation error");
            var context = new ActionExecutedContext(
                GetActionContext(),
                new List<IFilterMetadata>(),
                new Mock<Controller>().Object
            )
            {
                Result = new BadRequestObjectResult(validacaoException)
            };

            var filter = new ModelStateValidatorFilter();

            // Act
            filter.OnActionExecuted(context);

            // Assert
            var result = Assert.IsType<BadRequestObjectResult>(context.Result);
            var expectedError = validacaoException.Message.ConverteParaErro();
            Assert.Equivalent( expectedError, result.Value);
        }

        [Fact]
        public void OnActionExecuted_BadRequestWithException_InternalServerErro()
        {
            // Arrange
            var exception = new Exception("General error");
            var context = new ActionExecutedContext(
                GetActionContext(),
                new List<IFilterMetadata>(),
                new Mock<Controller>().Object
            )
            {
                Result = new BadRequestObjectResult(exception)
            };

            var filter = new ModelStateValidatorFilter();

            // Act
            filter.OnActionExecuted(context);

            // Assert
            var result = Assert.IsType<StatusCodeResult>(context.Result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
