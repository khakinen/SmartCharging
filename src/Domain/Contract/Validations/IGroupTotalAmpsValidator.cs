namespace SmartCharging.Domain.Contract.Validations;

public interface IGroupTotalAmpsValidator
{
    Task ValidateForCapacityInAmpsUpdate(Guid groupId, int newCapacityInAmps);
    Task ValidateForChargeStationAddition(Guid groupId, Guid addingChargeStation);
    Task ValidateForConnectorAddition(int commandMaxCurrentInAmps, Guid commandChargeStationId);
    Task ValidateForConnectorUpdate(int commandMaxCurrentInAmps, Guid commandChargeStationId, int commandConnectorNumber);
}