namespace SmartCharging.Domain.Contract.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string notFoundItem) :base($"{notFoundItem} could not be found")
    {
    }
}