using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Application.Validators;

namespace PosTech.Fase1.Contatos.Tests.Application
{
    public class DDDValidatorTest
    {
        private readonly DDDValidator _dddValidator;

        public DDDValidatorTest()
        {
            _dddValidator = new DDDValidator();
        }

        [Theory]
        [InlineData("SP", true, null)]
        [InlineData("", false, "UfSigla precisa ser informada e conter exatamente 2 caracteres ex:SP")]
        [InlineData("S", false, "UfSigla precisa ser informada e conter exatamente 2 caracteres ex:SP")]
        [InlineData("SP1", false, "UfSigla precisa ser informada e conter exatamente 2 caracteres ex:SP")]
        public void DDDValidator_UfSigla_DeveRetornarResultadoEsperado(string ufSigla, bool expectedIsValid, string? expectedErrorMessage)
        {
            // Arrange
            var dddDto = new DDDDto
            {
                DddId = 11,
                Regiao = "São Paulo",
                UfNome = "São Paulo",
                UfSigla = ufSigla
            };

            // Act
            var result = _dddValidator.Validate(dddDto);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
            if (!expectedIsValid)
            {
                Assert.Equal(expectedErrorMessage, result.Errors[0].ErrorMessage);
            }
        }

        [Theory]
        [InlineData("São Paulo", true, null)]
        [InlineData("", false, "UfRegiao é a principal cidade da área de abrangência, precisa ser informada, e conter no máximo 100 caracteres")]
        [InlineData("São Paulo, Campinas, Jundiaí, São José dos Campos, Guarulhos, Osasco, Santo André, São Bernardo do Campo", false, "UfRegiao é a principal cidade da área de abrangência, precisa ser informada, e conter no máximo 100 caracteres")]
        public void DDDValidator_UfRegiao_DeveRetornarResultadoEsperado(string regiao, bool expectedIsValid, string? expectedErrorMessage)
        {
            // Arrange
            var dddDto = new DDDDto
            {
                DddId = 11,
                Regiao = regiao,
                UfNome = "São Paulo",
                UfSigla = "SP"
            };

            // Act
            var result = _dddValidator.Validate(dddDto);

            // Assert
            Assert.Equal(expectedIsValid, result.IsValid);
            if (!expectedIsValid)
            {
                Assert.Equal(expectedErrorMessage, result.Errors[0].ErrorMessage);
            }
        }

        [Fact]
        public void DDDDto_VerificaNome_DeveRetornarSucesso()
        {
            // Arrange
            var dddDto = new DDDDto
            {
                DddId = 11,
                Regiao = "São Paulo, Campinas, Jundiaí, São José dos Campos, Guarulhos, Osasco, Santo André, São Bernardo do Campo",
                UfNome = "São Paulo",
                UfSigla = "SP"
            };

            // Assert
            Assert.Equal("São Paulo", dddDto.UfNome);
        }
    }
}
