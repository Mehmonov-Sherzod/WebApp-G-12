namespace WebApp.Application.Exceptions
{
    public class UnprocessableRequestException(string message) : Exception(message);
}
