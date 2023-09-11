using MediatR;
using SmartCharging.Data.Contract;
using SmartCharging.Domain.Contract.ChargeStations;
using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Domain.Implementation.CommandHandlers;

public class UpdateChargeStationCommandHandler : IRequestHandler<UpdateChargeStationCommand, ChargeStation>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IChargeStationService _chargeStationService;

    public UpdateChargeStationCommandHandler(IUnitOfWork unitOfWork, IChargeStationService chargeStationService)
    {
        _unitOfWork = unitOfWork;
        _chargeStationService = chargeStationService;
    }

    public async Task<ChargeStation> Handle(UpdateChargeStationCommand request, CancellationToken cancellationToken)
    {

        var chargeStation = await _chargeStationService.UpdateChargeStation(request);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return chargeStation;
    }
}