using MediatR;
using SmartCharging.Data.Contract;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Groups;

namespace SmartCharging.Domain.Implementation.CommandHandlers;

public class RemoveGroupCommandHandler : IRequestHandler<RemoveGroupCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGroupService _groupService;

    public RemoveGroupCommandHandler(IUnitOfWork unitOfWork, IGroupService groupService)
    {
        _unitOfWork = unitOfWork;
        _groupService = groupService;
    }

    public async Task Handle(RemoveGroupCommand request, CancellationToken cancellationToken)
    {
        await _groupService.RemoveGroup(request.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}