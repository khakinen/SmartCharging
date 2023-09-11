using FluentValidation;
using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Domain.Implementation.Validations;

public sealed class ChargeStationCommandValidator : AbstractValidator<ChargeStationCommand>
{
    public ChargeStationCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty");

        RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId can not be empty");
    }
}