using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartCharging.Domain.Contract.ChargeStations;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Connectors;
using SmartCharging.Domain.Contract.Groups;
using SmartCharging.Domain.Contract.Settings;
using SmartCharging.Domain.Contract.Validations;
using SmartCharging.Domain.Implementation.ChargeStations;
using SmartCharging.Domain.Implementation.Connectors;
using SmartCharging.Domain.Implementation.Groups;
using SmartCharging.Domain.Implementation.Settings;
using SmartCharging.Domain.Implementation.Validations;

namespace SmartCharging.Domain.Implementation;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<IAppSettings, AppSettings>();

        services.AddScoped<IGroupService, GroupService>();
        services.Decorate<IGroupService, GroupServiceValidationDecorator>();

        services.AddScoped<IChargeStationService, ChargeStationService>();
        services.Decorate<IChargeStationService, ChargeStationServiceValidationDecorator>();

        services.AddScoped<IConnectorService, ConnectorService>();
        services.Decorate<IConnectorService, ConnectorServiceValidationDecorator>();

        services.AddScoped<IGroupTotalAmpsValidator, GroupTotalAmpsValidator>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IValidator<CreateGroupCommand>, GroupCommandValidator>();
        services.AddScoped<IValidator<UpdateGroupCommand>, GroupCommandValidator>();
        services.AddScoped<IValidator<CreateChargeStationCommand>, ChargeStationCommandValidator>();
        services.AddScoped<IValidator<UpdateChargeStationCommand>, ChargeStationCommandValidator>();
        services.AddScoped<IValidator<CreateConnectorCommand>, ConnectorCommandValidator>();
        services.AddScoped<IValidator<UpdateConnectorCommand>, ConnectorCommandValidator>();

        return services;
    }
}