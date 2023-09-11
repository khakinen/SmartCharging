using SmartCharging.Domain.Contract.ValueTypes;

namespace SmartCharging.Domain.Contract.Commands;

public class GroupCommand :  IValidatableCommand
{
    public string Name { get; set; }
    public Amp Capacity { get; set; }
}