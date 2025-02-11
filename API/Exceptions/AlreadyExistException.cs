namespace API.Exceptions;

public class AlreadyExistException(string message) : Exception(message)
{
}