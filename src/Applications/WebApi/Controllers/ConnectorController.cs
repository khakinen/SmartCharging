using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Application.WebApi.Models;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Queries;
using Api = SmartCharging.Application.WebApi.Models;
using Domain = SmartCharging.Domain.Contract.Connectors;

namespace SmartCharging.Application.WebApi.Controllers;

[ApiController]
[Route("api/v1")]
public class ConnectorController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConnectorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("chargestations/{chargeStationId}/connectors")]
    public async Task<IActionResult> GetConnectorsOfChargeStation(Guid chargeStationId)
    {
        var query = new ConnectorsOfChargeStationQuery
        {
            ChargeStationId = chargeStationId
        };

        var connectors = await _mediator.Send(query);

        return Ok(connectors.Select(Map));
    }

    [HttpPost]
    [Route("chargestations/{chargeStationId}/connectors")]
    public async Task<IActionResult> CreateConnector(Guid chargeStationId, CreateConnectorRequest request)
    {
        var command = new CreateConnectorCommand
        {
            MaxCurrent = request.MaxCurrentInAmps,
            ChargeStationId = chargeStationId,
        };

        command.ChargeStationId = chargeStationId;

        var connector = await _mediator.Send(command);

        return Ok(Map(connector));
    }

    [HttpPut]
    [Route("chargestations/{chargeStationId}/connectors/{connectorNumber}")]
    public async Task<IActionResult> UpdateConnector(Guid chargeStationId, int connectorNumber, UpdateConnectorRequest request)
    {
        var command = new UpdateConnectorCommand
        {
            MaxCurrent = request.MaxCurrentInAmps,
            ChargeStationId = chargeStationId,
            ConnectorNumber = connectorNumber
        };

        command.ChargeStationId = chargeStationId;
        command.ConnectorNumber = connectorNumber;

        var connector = await _mediator.Send(command);

        return Ok(Map(connector));
    }

    [HttpDelete]
    [Route("chargestations/{chargeStationId}/connectors/{connectorNumber}")]
    public async Task<IActionResult> RemoveConnector(Guid chargeStationId, int connectorNumber)
    {
        var command = new RemoveConnectorCommand()
        {
            ChargeStationId = chargeStationId,
            ConnectorNumber = connectorNumber
        };

        await _mediator.Send(command);

        return Ok();
    }

    private Api::Connector Map(Domain::Connector connector) =>
        new Api::Connector
        {
            ConnectorNumber = connector.ConnectorNumber,
            MaxCurrent = connector.MaxCurrent,
            ChargeStationId = connector.ChargeStationId,
        };
}