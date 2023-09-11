namespace SmartCharging.Domain.Contract.Commands;

public class ConnectorCommand :  IValidatableCommand
{
    public int  MaxCurrentInAmps { get; set; }
}