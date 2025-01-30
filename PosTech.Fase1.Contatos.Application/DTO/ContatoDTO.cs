namespace PosTech.Fase1.Contatos.Application.DTO;


public class ContatoDto
{
    public Guid? ContatoId { get; set; } 
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public bool Ativo { get; set; }
    public int DddId { get; set; }
    public DDDDto? Ddd { get; set; }
}




