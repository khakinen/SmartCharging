using MediatR;
using SmartCharging.Domain.Contract.ChargeStations;
using SmartCharging.Domain.Contract.Queries;

namespace SmartCharging.Domain.Implementation.QueryHandlers;

public class ChargeStationQueryHandler : IRequestHandler<ChargeStationQuery, ChargeStation>
{
    private readonly IChargeStationService _chargeStationService;

    public ChargeStationQueryHandler(IChargeStationService chargeStationService)
    {
        _chargeStationService = chargeStationService;
    }

    public async Task<ChargeStation> Handle(ChargeStationQuery request, CancellationToken cancellationToken)
    {
        return await _chargeStationService.GetChargeStation(request.Id);
    }
}