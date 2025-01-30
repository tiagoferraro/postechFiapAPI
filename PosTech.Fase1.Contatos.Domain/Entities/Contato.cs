namespace PosTech.Fase1.Contatos.Domain.Entities;

public class Contato
{
    public Guid? ContatoId { get; private set; }
    public string Nome { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public int DddId { get; private set; }
    public DDD? Ddd { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime DataInclusao { get; private set; }

    public Contato(Guid? contatoId, string nome, string telefone, string email, int dddId)
    {
        ContatoId = contatoId ?? Guid.NewGuid();
        Nome = nome;
        Telefone = telefone;
        Email = email;
        DddId = dddId;
        DataInclusao = DateTime.Now;
        Ativo = true; 
    }
    

    public void DesativarContato()
    {
        Ativo = false;
    }
}

