namespace SmartCharging.Domain.Contract.Commands;

public class ChargeStationCommand : IValidatableCommand
{
    public string Name { get; set; }
    public Guid GroupId { get; set; }
}