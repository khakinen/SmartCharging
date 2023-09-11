namespace SmartCharging.Domain.Contract.Commands;

public class GroupCommand :  IValidatableCommand
{
    public string Name { get; set; }
    public int CapacityInAmps { get; set; }
}