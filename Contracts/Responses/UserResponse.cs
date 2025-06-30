namespace SistemaOrganizacaoEstudantil.Contracts.Responses;

using Entities;

public class UserResponse
{
    public required int Id { get; set; }
    public required String Name { get; set; }
    public required String Email { get; set; }

    public static UserResponse FromUser(User user)
    {
        return new () {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}
