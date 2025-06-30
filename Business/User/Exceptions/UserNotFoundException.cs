namespace SistemaOrganizacaoEstudantil.Business.User.Exceptions;

using SistemaOrganizacaoEstudantil.Exceptions;

public class UserNotFoundException : HttpException
{
    public UserNotFoundException(int id)
        : base(StatusCodes.Status404NotFound, $"User with ID {id} was not found.")
    {}

    public UserNotFoundException(string email)
        : base(StatusCodes.Status404NotFound, $"User with email {email} was not found.")
    {}
}
