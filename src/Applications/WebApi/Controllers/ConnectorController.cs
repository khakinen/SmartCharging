using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Application.WebApi.Models;
using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Application.WebApi.Controllers;

[ApiController]
[Route("api/v1")]
public class ConnectorController : ControllerBase
{
    private readonly  IMediator _mediator;

    public ConnectorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("chargestations/{chargeStationId}/connectors")]
    public async Task<IActionResult> CreateConnector(Guid chargeStationId, CreateConnectorRequest request)
    {
        var command = new CreateConnectorCommand
        {
            MaxCurrentInAmps = request.MaxCurrentInAmps,
            ChargeStationId = chargeStationId,
        };
        
        command.ChargeStationId = chargeStationId;
        
        var connector = await _mediator.Send(command);
        
        return Ok(connector);
    }

    [HttpPut]
    [Route("chargestations/{chargeStationId}/connectors/{connectorNumber}")]
    public async Task<IActionResult> UpdateConnector(Guid chargeStationId, int connectorNumber, UpdateConnectorRequest request)
    {
        var command = new UpdateConnectorCommand
        {
            MaxCurrentInAmps = request.MaxCurrentInAmps,
            ChargeStationId = chargeStationId,
            ConnectorNumber = connectorNumber
        };
        
        command.ChargeStationId = chargeStationId;
        command.ConnectorNumber = connectorNumber;
          
        var connector = await _mediator.Send(command);

        return Ok(connector);
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
}