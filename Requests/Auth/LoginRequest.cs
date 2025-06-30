namespace SistemaOrganizacaoEstudantil.Requests.Auth;

using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public required String Email { get; set; }

    [Required]
    public required String Password { get; set; }
}
