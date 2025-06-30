namespace SistemaOrganizacaoEstudantil.Entities;

public class User
{
    public int Id { get; set; }
    public required String Name { get; set; }
    public required String Email { get; set; }
    public required String Password { get; set; }
}
