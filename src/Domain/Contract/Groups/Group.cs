using SmartCharging.Domain.Contract.ValueTypes;

namespace SmartCharging.Domain.Contract.Groups;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Amp Capacity { get; set; }
}