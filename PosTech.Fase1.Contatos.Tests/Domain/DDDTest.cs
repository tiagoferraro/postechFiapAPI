using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Tests.Domain;
    public class DDDTest
    {
        [Fact]
        public void DDDConstruir()
        {
            //Arrange
            //Action
            var ddd = new DDD(25,"ES","Linhares");
            //Assert
            Assert.Equal(25,ddd.DddId);
            Assert.Equal("Linhares", ddd.Regiao); 
            Assert.Equal("ES", ddd.UnidadeFederativa.Sigla);           
            Assert.Equal("Espírito Santo", ddd.UnidadeFederativa.Nome);     

            



        }
    }
