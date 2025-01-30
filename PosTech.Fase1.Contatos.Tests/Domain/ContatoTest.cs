using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Tests.Domain
{
    public class ContatoTest
    {
        [Fact]
        public void Contato_Excluir_VerificaContatoExcluido()
        {
            //Arrange
            var contato = new Contato(null, "tiago", "321321312", "tiago@gmail.com", 11);
            //Action
            contato.DesativarContato();
            //Assert
            Assert.False(contato.Ativo);

        }
        [Fact]
        public void Contato_Criar_VerificaDataInclusao()
        {
            //Arrange
            var contato = new Contato( null,"tiago", "321321312", "tiago@gmail.com", 11);
            //Action
          
            //Assert
            Assert.IsType<DateTime>(contato.DataInclusao);


        }
    }
}
