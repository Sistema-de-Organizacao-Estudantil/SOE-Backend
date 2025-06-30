namespace SistemaOrganizacaoEstudantil.Exceptions;

public class HttpException : Exception
{
    public int Status { get; }

    public HttpException(int status, string message)
        : base(message)
    {
        this.Status = status;
    }
}
