using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Application.Validators;


namespace PosTech.Fase1.Contatos.Tests.Application
{
    public class ContatoValidatorTest
    {
        [Fact]
        public void ContatoValidator_Contato_OK()
        {
            //arrange
            ContatoDto contatoDto = new ContatoDto()
            {
                Nome = "João de Barro",
                Telefone = "9 88808182",
                Email = "Joao.Barro@acme.com",
                Ativo = true,
                DddId = 27
            };
            //act
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contatoDto);
            //assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ContatoValidator_NomeVazio_Error()
        {
            //arrange
            ContatoDto contatoDto = new ContatoDto()
            {
                Nome = "",
                Telefone = "988808182",
                Email = "Joao.Barro@acme.com",
                Ativo = true,
                DddId = 27
            };
            //act
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contatoDto);
            //assert
            Assert.False(result.IsValid);
            Assert.Equal("NotEmptyValidator", result.Errors[0].ErrorCode);
            Assert.Equal("Informe o nome do contato", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void ContatoValidator_NomeInformed_Error()
        {
            //arrange
            ContatoDto contatoDto = new ContatoDto()
            {
                
                Telefone = "988808182",
                Email = "Joao.Barro@acme.com",
                Ativo = true,
                DddId = 27
            };
            //act
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contatoDto);
            //assert
            Assert.False(result.IsValid);
            Assert.Equal("NotEmptyValidator", result.Errors[0].ErrorCode);
            Assert.Equal("Informe o nome do contato", result.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData("9 9788 8081")]
        [InlineData("9 9788-8081")]
        [InlineData("3327 6108")]
        [InlineData("33276108")]
        [InlineData("3327-6108")]
        public void ContatoValidator_Telefone_OK(string tel)
        {
            //arrange
            ContatoDto contatoDto = new ContatoDto()
            {
                Nome = "Joao de Barro",
                Telefone = tel,
                Ativo = true,
                Email = "Joao.Barro@acme.com",
                DddId = 27
            };
            //act
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contatoDto);
            //assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ContatoValidator_TelefoneEmpty_Error()
        {
            //arrange
            ContatoDto contatoDto = new ContatoDto()
            {
                Nome = "Joao de Barro",
                Telefone = "",
                Email = "Joao.Barro@acme.com",
                DddId = 27
            };
            //act
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contatoDto);
            //assert
            Assert.False(result.IsValid);
            Assert.Equal("NotEmptyValidator", result.Errors[0].ErrorCode);
            Assert.Equal("Informe o número do telefone de contato", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void ContatoValidator_TelefoneNotInformed_Error()
        {
            //arrange
            ContatoDto contatoDto = new ContatoDto()
            {
                Nome = "Joao de Barro",
                Email = "Joao.Barro@acme.com",
                DddId = 27
            };
            //act
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contatoDto);
            //assert
            Assert.False(result.IsValid);
            Assert.Equal("NotEmptyValidator", result.Errors[0].ErrorCode);
            Assert.Equal("Informe o número do telefone de contato", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void ContatoValidator_EmailEmpty_Error()
        {
            //arrange
            ContatoDto contatoDto = new ContatoDto()
            {
                Nome = "Joao de Barro",
                Telefone = "988808182",
                Email = "",
                DddId = 27
            };
            //act
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contatoDto);
            //assert
            Assert.False(result.IsValid);
            // Assert.Equal("NotEmptyValidator", result.Errors[0].ErrorCode);        
            Assert.Equal("Informe o e-mail do cliente", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void ContatoValidator_EmailNotInformed_Error()
        {
            //arrange
            ContatoDto contatoDto = new ContatoDto()
            {
                Nome = "Joao de Barro",
                Telefone = "988808182",
                Ativo = true,
                DddId = 27
            };
            //act
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contatoDto);
            //assert
            Assert.False(result.IsValid);
            // Assert.Equal("NotEmptyValidator", result.Errors[0].ErrorCode);        
            Assert.Equal("Informe o e-mail do cliente", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void ContatoValidator_EmailInvalid_Error()
        {
            //arrange
            ContatoDto contatoDto = new ContatoDto()
            {
                Nome = "Joao de Barro",
                Telefone = "988808182",
                Email = "Joao.Barro",
                Ativo = true,
                DddId = 27
            };
            //act
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contatoDto);
            //assert
            Assert.False(result.IsValid);
            // Assert.Equal("NotEmptyValidator", result.Errors[0].ErrorCode);        
            Assert.Equal("E-mail inválido", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void ContatoValidator_DDDId_Invalid_Error()
        {
            //arrange
            ContatoDto contatoDto = new ContatoDto()
            {
                Nome = "Joao de Barro",
                Telefone = "988808182",
                Email = "Joao.Barro@acme.com",
                Ativo = true,
                DddId = 127
            };
            //act
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contatoDto);
            //assert
            Assert.False(result.IsValid);
            // Assert.Equal("NotEmptyValidator", result.Errors[0].ErrorCode);        
            Assert.Equal("o código de área deve ser um inteiro de 2 dígitos.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void ContatoValidator_DDDId_NotInformed_Error()
        {
            //arrange
            ContatoDto contatoDto = new ContatoDto()
            {
                Nome = "Joao de Barro",
                Telefone = "988808182",
                Email = "Joao.Barro@acme.com",
                Ativo = true,
            };
            //act
            var contatoValidator = new ContatoValidator();
            var result = contatoValidator.Validate(contatoDto);
            //assert
            Assert.False(result.IsValid);
            // Assert.Equal("NotEmptyValidator", result.Errors[0].ErrorCode);        
            Assert.Equal("o código de área deve ser um inteiro de 2 dígitos.", result.Errors[0].ErrorMessage);
        }
    }
}