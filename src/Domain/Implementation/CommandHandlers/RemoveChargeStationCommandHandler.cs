using MediatR;
using SmartCharging.Data.Contract;
using SmartCharging.Domain.Contract.ChargeStations;
using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Domain.Implementation.CommandHandlers;

public class RemoveChargeStationCommandHandler : IRequestHandler<RemoveChargeStationCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IChargeStationService _chargeStationService;

    public RemoveChargeStationCommandHandler(IUnitOfWork unitOfWork, IChargeStationService chargeStationService)
    {
        _unitOfWork = unitOfWork;
        _chargeStationService = chargeStationService;
    }

    public async Task Handle(RemoveChargeStationCommand request, CancellationToken cancellationToken)
    {
        await _chargeStationService.RemoveChargeStation(request.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}