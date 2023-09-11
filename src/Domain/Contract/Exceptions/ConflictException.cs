namespace SmartCharging.Domain.Contract.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) :base(message)
    {
    }
}