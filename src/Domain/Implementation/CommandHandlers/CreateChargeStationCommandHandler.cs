using MediatR;
using SmartCharging.Data.Contract;
using SmartCharging.Domain.Contract.ChargeStations;
using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Domain.Implementation.CommandHandlers;

public class CreateChargeStationCommandHandler : IRequestHandler<CreateChargeStationCommand, ChargeStation>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IChargeStationService _chargeStationService;

    public CreateChargeStationCommandHandler(IUnitOfWork unitOfWork, IChargeStationService chargeStationService)
    {
        _unitOfWork = unitOfWork;
        _chargeStationService = chargeStationService;
    }

    public async Task<ChargeStation> Handle(CreateChargeStationCommand request, CancellationToken cancellationToken)
    {
        var chargeStation = await _chargeStationService.CreateChargeStation(request);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return chargeStation;
    }
}