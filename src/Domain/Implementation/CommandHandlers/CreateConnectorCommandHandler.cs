using MediatR;
using SmartCharging.Data.Contract;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Connectors;

namespace SmartCharging.Domain.Implementation.CommandHandlers;

public class CreateConnectorCommandHandler : IRequestHandler<CreateConnectorCommand, Connector>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConnectorService _connectorService;

    public CreateConnectorCommandHandler(IUnitOfWork unitOfWork, IConnectorService connectorService)
    {
        _unitOfWork = unitOfWork;
        _connectorService = connectorService;
    }

    public async Task<Connector> Handle(CreateConnectorCommand request, CancellationToken cancellationToken)
    {
        var connector = await _connectorService.CreateConnector(request);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return connector;
    }
}