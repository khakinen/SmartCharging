using FluentValidation;
using MediatR;
using SmartCharging.Domain.Contract.Commands;
using ValidationException = SmartCharging.Domain.Contract.Exceptions.ValidationException;

namespace SmartCharging.Domain.Implementation.Validations;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IValidatableCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errors = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();

        if (errors.Any())
        {
            throw new ValidationException(string.Join(". ", errors.Select(e => e.ErrorMessage)));
        }

        return await next();
    }
}