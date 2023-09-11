using Microsoft.EntityFrameworkCore;
using SmartCharging.Data.Contract.Repositories;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Exceptions;
using Group = SmartCharging.Domain.Contract.Groups.Group;
using GroupRecord = SmartCharging.Data.Contract.Models.Group;

namespace Data.Implementation.Repositories;

public class GroupRepository:  IGroupRepository
{
    private readonly SmartChargingDbContext _smartChargingDbContext;

    public GroupRepository(SmartChargingDbContext smartChargingDbContext)
    {
        _smartChargingDbContext = smartChargingDbContext;
    }
    public async Task Create(Group group)
    {
        var record = Map(group);

        await _smartChargingDbContext.AddAsync(record);
    }

    private GroupRecord Map(Group group)
    {
        return new GroupRecord
        {
            Id = group.Id,
            Name = group.Name,
            CapacityInAmps = group.Capacity,
        };
    }

    public async Task<Group> Update(Guid id, UpdateGroupCommand updateGroupCommand)
    {
        var record = await _smartChargingDbContext.Groups
            .Where(cs => cs.Id == id)
            .FirstOrDefaultAsync();

        if (record == null)
        {
            throw new NotFoundException($"{nameof(Group)}");
        }

        record.Name = updateGroupCommand.Name;
        record.CapacityInAmps = updateGroupCommand.Capacity;
        
        return Map(record);
    }

    private Group Map(GroupRecord record)
    {
        return new Group
        {
            Id = record.Id,
            Name = record.Name,
            Capacity = record.CapacityInAmps,
        };
    }

    public async Task Delete(Guid groupId)
    {
        var record = await _smartChargingDbContext.Groups
            .Where(e => e.Id == groupId)
            .FirstOrDefaultAsync();
        
        if (record == null)
        {
            throw new NotFoundException($"{nameof(Group)}");
        }

        _smartChargingDbContext.Remove(record);
    }

    public async Task<Group> Read(Guid id)
    {
        var record = await _smartChargingDbContext.Groups
            .Where(cs => cs.Id == id)
            .FirstOrDefaultAsync();

        if (record == null)
        {
            throw new NotFoundException($"{nameof(Group)}");
        }
        
        return Map(record);
    }

    public async Task<ICollection<Group>> ReadAll()
    {
        var records = await _smartChargingDbContext.Groups
            .ToListAsync();

        return records.Select(Map).ToList();
    }
}