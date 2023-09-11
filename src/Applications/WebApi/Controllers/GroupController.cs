using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Application.WebApi.Models;
using SmartCharging.Domain.Contract.Commands;

namespace SmartCharging.Application.WebApi.Controllers;

[ApiController]
[Route("api/v1")]
public class GroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("groups")]
    public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
    {
        var command = new CreateGroupCommand
        {
            Name = request.Name,
            CapacityInAmps = request.CapacityInAmps
        };
        
        var group = await _mediator.Send(command);

        return Ok(new
        {
            group.Id,
            group.Name,
            group.CapacityInAmps
        });
    }

    [HttpPut]
    [Route("groups/{groupId}")]
    public async Task<IActionResult> UpdateGroup(Guid groupId, UpdateGroupRequest request)
    {
        var command = new UpdateGroupCommand
        {
            GroupId = groupId,
            Name = request.Name,
            CapacityInAmps = request.CapacityInAmps,
        };
        
        var group = await _mediator.Send(command);

        return Ok(new
        {
            group.Id,
            group.Name,
            group.CapacityInAmps
        });
    }

    [HttpDelete]
    [Route("groups/{groupId}")]
    public async Task<IActionResult> RemoveGroup(Guid groupId)
    {
        var command = new RemoveGroupCommand()
        {
            Id = groupId
        };
        
        await _mediator.Send(command);
        
        return Ok();
    }
}

