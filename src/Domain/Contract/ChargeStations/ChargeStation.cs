namespace SmartCharging.Domain.Contract.ChargeStations;

public class ChargeStation  
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid GroupId { get; set; }
}