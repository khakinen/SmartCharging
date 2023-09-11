using SmartCharging.Domain.Contract.ChargeStations;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Validations;

namespace SmartCharging.Domain.Implementation.ChargeStations;

public class ChargeStationServiceValidationDecorator : IChargeStationService
{
    private readonly IChargeStationService _chargeStationService;
    private readonly IGroupTotalAmpsValidator _groupTotalAmpsValidator;

    public ChargeStationServiceValidationDecorator(IGroupTotalAmpsValidator groupTotalAmpsValidator, IChargeStationService chargeStationService)
    {
        _groupTotalAmpsValidator = groupTotalAmpsValidator;
        _chargeStationService = chargeStationService;
    }

    public async Task<ChargeStation> CreateChargeStation(CreateChargeStationCommand command)
    {
        return await _chargeStationService.CreateChargeStation(command);
    }

    public async Task RemoveChargeStation(Guid chargeStationId)
    {
        await _chargeStationService.RemoveChargeStation(chargeStationId);
    }

    public async Task<ChargeStation> UpdateChargeStation(UpdateChargeStationCommand command)
    {
        await _groupTotalAmpsValidator.ValidateForChargeStationAddition(command.GroupId, command.ChargeStationId);

        return await _chargeStationService.UpdateChargeStation(command);
    }

    public async Task<ICollection<ChargeStation>> GetByGroupId(Guid groupId)
    {
        return await _chargeStationService.GetByGroupId(groupId);
    }

    public async Task<ChargeStation> GetChargeStation(Guid id)
    {
        return await _chargeStationService.GetChargeStation(id);
    }
}