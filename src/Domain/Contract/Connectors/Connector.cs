namespace SmartCharging.Domain.Contract.Connectors;

public class Connector
{
    public int ConnectorNumber{ get; set; }
    public int MaxCurrentInAmps { get; set; }
    public Guid ChargeStationId { get; set; }
}