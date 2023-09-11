using MediatR;
using SmartCharging.Data.Contract;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Connectors;

namespace SmartCharging.Domain.Implementation.CommandHandlers;

public class RemoveConnectorCommandHandler : IRequestHandler<RemoveConnectorCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConnectorService _connectorService;

    public RemoveConnectorCommandHandler(IUnitOfWork unitOfWork, IConnectorService connectorService)
    {
        _unitOfWork = unitOfWork;
        _connectorService = connectorService;
    }

    public async Task Handle(RemoveConnectorCommand request, CancellationToken cancellationToken)
    {
        await _connectorService.RemoveConnector(request.ChargeStationId,request.ConnectorNumber);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}