using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Application.WebApi.Models;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Queries;

using Api = SmartCharging.Application.WebApi.Models;
using Domain = SmartCharging.Domain.Contract.Groups;

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
    
    [HttpGet]
    [Route("groups")]
    public async Task<IActionResult> GetAllGroups()
    {
        var query = new GroupsQuery();
        
        var groups = await _mediator.Send(query);

        return Ok(groups.Select(Map));
    }
    
    [HttpGet]
    [Route("groups/{groupId}")]
    public async Task<IActionResult> GetGroup(Guid groupId)
    {
        var query = new GroupQuery
        {
            Id = groupId
        };
        
        var group = await _mediator.Send(query);

        return Ok(Map(group));
    }

    [HttpPost]
    [Route("groups")]
    public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
    {
        var command = new CreateGroupCommand
        {
            Name = request.Name,
            Capacity = request.CapacityInAmps
        };
        
        var group = await _mediator.Send(command);

        return Ok(Map(group));
    }

    [HttpPut]
    [Route("groups/{groupId}")]
    public async Task<IActionResult> UpdateGroup(Guid groupId, UpdateGroupRequest request)
    {
        var command = new UpdateGroupCommand
        {
            GroupId = groupId,
            Name = request.Name,
            Capacity = request.CapacityInAmps,
        };
        
        var group = await _mediator.Send(command);

        return Ok(Map(group));
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
    
    private Api::Group Map(Domain::Group group) =>
        new Api::Group
        {
            Id = group.Id,
            Name = group.Name,
            Capacity = group.Capacity
        };
}

