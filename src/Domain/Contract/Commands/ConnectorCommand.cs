using SmartCharging.Domain.Contract.ValueTypes;

namespace SmartCharging.Domain.Contract.Commands;

public class ConnectorCommand :  IValidatableCommand
{
    public Amp MaxCurrent { get; set; }
}