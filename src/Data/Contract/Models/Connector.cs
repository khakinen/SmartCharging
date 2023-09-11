namespace SmartCharging.Data.Contract.Models;

public class Connector
{
    public Guid Id { get; set; }
    public int ConnectorNumber{ get; set; }
    public int MaxCurrentInAmps { get; set; }
    public Guid ChargeStationId { get; set; }
    
    public virtual ChargeStation ChargeStation { get; set; }
}