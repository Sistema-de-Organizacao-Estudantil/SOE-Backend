namespace SistemaOrganizacaoEstudantil.Contracts.Requests.Auth;

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public required String Name { get; set; }

    [Required]
    public required String Email { get; set; }

    [Required]
    [MinLength(6)]
    public required String Password { get; set; }
}
