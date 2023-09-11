using FluentValidation;
using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Domain.Implementation.Validations;

public sealed class GroupCommandValidator : AbstractValidator<GroupCommand>
{
    public GroupCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty");

        RuleFor(x => x.Capacity).NotEmpty().GreaterThan(0).WithMessage("CapacityInAmps can not be lower than zero");;
    }
}