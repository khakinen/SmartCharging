using MediatR;
using SmartCharging.Data.Contract;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Connectors;

namespace SmartCharging.Domain.Implementation.CommandHandlers;

public class UpdateConnectorCommandHandler : IRequestHandler<UpdateConnectorCommand, Connector>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConnectorService _connectorService;

    public UpdateConnectorCommandHandler(IUnitOfWork unitOfWork, IConnectorService connectorService)
    {
        _unitOfWork = unitOfWork;
        _connectorService = connectorService;
    }

    public async Task<Connector> Handle(UpdateConnectorCommand request, CancellationToken cancellationToken)
    {
        var connector = await _connectorService.UpdateConnector(request);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return connector;
    }
}