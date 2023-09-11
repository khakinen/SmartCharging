using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Application.WebApi.Models;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Queries;

namespace SmartCharging.Application.WebApi.Controllers;

[ApiController]
[Route("api/v1")]
public class ChargeStationController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChargeStationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("chargestations/{chargeStationId}")]
    public async Task<IActionResult> GetChargeStation(Guid chargeStationId)
    {
        var query = new ChargeStationQuery
        {
            Id = chargeStationId
        };
        
        var chargeStation = await _mediator.Send(query);

        return Ok(chargeStation);
    }

    [HttpPost]
    [Route("groups/{groupId}/chargestations")]
    public async Task<IActionResult> CreateChargeStation(Guid groupId, CreateChargeStationRequest request)
    {
        var command = new CreateChargeStationCommand
        {
            Name = request.Name,
            GroupId = groupId
        };
        
        var chargeStation = await _mediator.Send(command);
        
        return Ok(chargeStation);
    }
    
    [HttpPut]
    [Route("chargestations/{chargeStationId}")]
    public async Task<IActionResult> UpdateChargeStation(Guid chargeStationId, UpdateChargeStationRequest request)
    {
        var command = new UpdateChargeStationCommand
        {
            Name = request.Name,
            GroupId = request.GroupId,
            ChargeStationId = chargeStationId,
        };

        var chargeStation = await _mediator.Send(command);
        
        return Ok(chargeStation);
    }
    
    [HttpDelete]
    [Route("chargestations/{chargeStationId}")]
    public async Task<IActionResult> RemoveChargeStation(Guid chargeStationId)
    {
        var command = new RemoveChargeStationCommand()
        {
            Id = chargeStationId
        };
        
        await _mediator.Send(command);
        
        return Ok();
    }
}