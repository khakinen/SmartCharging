namespace SmartCharging.Application.WebApi.Models;

public class Connector
{
    public int ConnectorNumber{ get; set; }
    public int MaxCurrent { get; set; }
    public Guid ChargeStationId { get; set; }
}