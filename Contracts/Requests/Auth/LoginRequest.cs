namespace SistemaOrganizacaoEstudantil.Contracts.Requests.Auth;

using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required]
    public required String Email { get; set; }

    [Required]
    public required String Password { get; set; }
}
