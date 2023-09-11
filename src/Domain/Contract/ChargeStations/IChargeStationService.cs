using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Domain.Contract.ChargeStations;

public interface IChargeStationService
{
    Task<ChargeStation> CreateChargeStation(CreateChargeStationCommand chargeStation);
    Task RemoveChargeStation(Guid chargeStationId);
    Task<ChargeStation> UpdateChargeStation(UpdateChargeStationCommand chargeStation);
    Task<ICollection<ChargeStation>> GetByGroupId(Guid groupId);
    Task<ChargeStation> GetChargeStation(Guid id);
}