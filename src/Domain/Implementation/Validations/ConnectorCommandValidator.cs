using FluentValidation;
using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Domain.Implementation.Validations;

public sealed class ConnectorCommandValidator : AbstractValidator<ConnectorCommand>
{
    public ConnectorCommandValidator()
    {
        RuleFor(x => x.MaxCurrent).NotEmpty().GreaterThan(0).WithMessage("MaxCurrentInAmps can not be lower than zero");
    }
}