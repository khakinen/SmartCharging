using MediatR;
using SmartCharging.Domain.Contract.Connectors;
using SmartCharging.Domain.Contract.Queries;

namespace SmartCharging.Domain.Implementation.QueryHandlers;

public class ConnectorsOfChargeStationQueryHandler : IRequestHandler<ConnectorsOfChargeStationQuery, ICollection<Connector>>
{
    private readonly IConnectorService _connectorService;

    public ConnectorsOfChargeStationQueryHandler(IConnectorService connectorService)
    {
        _connectorService = connectorService;
    }

    public async Task<ICollection<Connector>> Handle(ConnectorsOfChargeStationQuery request, CancellationToken cancellationToken)
    {
        return await _connectorService.GetByChargeStationId(request.ChargeStationId);
    }
}