using MediatR;
using SmartCharging.Domain.Contract.ChargeStations;

namespace SmartCharging.Domain.Contract.Commands;

public class CreateChargeStationCommand : ChargeStationCommand, IRequest<ChargeStation>
{
}