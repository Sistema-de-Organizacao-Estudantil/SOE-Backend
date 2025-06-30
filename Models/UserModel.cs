namespace SistemaOrganizacaoEstudantil.Models;

using Entities;

public class UserModel
{
    public required int Id { get; set; }
    public required String Name { get; set; }
    public required String Email { get; set; }

    public static UserModel FromUser(User user)
    {
        return new () {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}
