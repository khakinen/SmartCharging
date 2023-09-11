using MediatR;
using SmartCharging.Data.Contract;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Groups;

namespace SmartCharging.Domain.Implementation.CommandHandlers;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Group>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGroupService _groupService;

    public UpdateGroupCommandHandler(IUnitOfWork unitOfWork, IGroupService groupService)
    {
        _unitOfWork = unitOfWork;
        _groupService = groupService;
    }

    public async Task<Group> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupService.UpdateGroup(request);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return group;
    }
}