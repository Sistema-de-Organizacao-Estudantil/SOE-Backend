namespace SistemaOrganizacaoEstudantil.Business.Auth.Exceptions;

using SistemaOrganizacaoEstudantil.Exceptions;

public class InvalidCredentialsException : HttpException
{
    public InvalidCredentialsException()
        : base(StatusCodes.Status401Unauthorized, "Invalid user credentials were given.") {}
}
