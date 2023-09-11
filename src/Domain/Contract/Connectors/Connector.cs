using SmartCharging.Domain.Contract.ValueTypes;

namespace SmartCharging.Domain.Contract.Connectors;

public class Connector
{
    public int ConnectorNumber{ get; set; }
    public Amp MaxCurrent { get; set; }
    public Guid ChargeStationId { get; set; }
}