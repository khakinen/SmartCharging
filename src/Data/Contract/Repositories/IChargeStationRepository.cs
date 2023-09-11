using SmartCharging.Domain.Contract.ChargeStations;
using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Data.Contract.Repositories;

public interface IChargeStationRepository
{
    Task Create(ChargeStation group);
    Task<ChargeStation> Update(Guid id, UpdateChargeStationCommand updateChargeStationCommand);
    Task Delete(Guid id);
    Task<ICollection<ChargeStation>> GetByGroupId(Guid groupId);
    Task<ChargeStation> Read(Guid id);
}