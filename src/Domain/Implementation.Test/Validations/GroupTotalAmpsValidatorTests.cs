using Ninstance;
using NSubstitute;
using SmartCharging.Data.Contract.Repositories;
using SmartCharging.Domain.Contract.ChargeStations;
using SmartCharging.Domain.Contract.Connectors;
using SmartCharging.Domain.Contract.Exceptions;
using SmartCharging.Domain.Contract.Groups;
using SmartCharging.Domain.Implementation.Validations;

namespace Domain.Implementation.Test.Validations;

public class GroupTotalAmpsValidatorTests 
{
    private readonly IConnectorRepository _connectorRepository;
    private readonly IChargeStationRepository _chargeStationRepository;
    private readonly IGroupRepository _groupRepository;

    public GroupTotalAmpsValidatorTests()
    {
        _connectorRepository = Substitute.For<IConnectorRepository>();
        _chargeStationRepository = Substitute.For<IChargeStationRepository>();
        _groupRepository = Substitute.For<IGroupRepository>();
    }

    [InlineData(3,2)]
    [InlineData(1200,1199)]
    [Theory(DisplayName = "Service should throw validationException if desired capacity is lower than the actual value")]
    public async Task Service_ThrowEx_ValidateForCapacityInAmpsUpdate(int actualTotalCapacity, int newCapacityToBeUpdated )
    {
        _connectorRepository.GetTotalMaxCurrentInAmpsOfGroup(Arg.Any<Guid>())
            .Returns(actualTotalCapacity);
         
        var service = Instance.Of<GroupTotalAmpsValidator>(_connectorRepository);
        
        await Assert.ThrowsAsync<ValidationException>(() => service.ValidateForCapacityInAmpsUpdate(default, newCapacityToBeUpdated));
    }
    
    [InlineData(2000,1200,2000)]
    [InlineData(100,85,16)]
    [InlineData(10,8,3)]
    [Theory(DisplayName = "Service should throw validationException if new charge station addition violates the maximum group capacity rule")]
    public async Task Service_ThrowEx_ValidateForChargeStationAddition(int groupMaxCapacity, int actualTotalGroupCapacity, int workloadFromNewChargeStation)
    {
        _groupRepository.Read(Arg.Any<Guid>()).Returns(new Group
        {
            CapacityInAmps = groupMaxCapacity
        });
        
        _connectorRepository.GetTotalMaxCurrentInAmpsOfGroup(Arg.Any<Guid>())
            .Returns(actualTotalGroupCapacity);
        
        _connectorRepository.GetTotalMaxCurrentInAmpsOfChargeStation(Arg.Any<Guid>())
            .Returns(workloadFromNewChargeStation);
         
        var service = Instance.Of<GroupTotalAmpsValidator>(_groupRepository,_connectorRepository);
        
        await Assert.ThrowsAsync<ValidationException>(() => service.ValidateForCapacityInAmpsUpdate(default, default));
    }
    
    [InlineData(2000,1200,2000)]
    [InlineData(100,85,16)]
    [InlineData(10,8,3)]
    [Theory(DisplayName = "Service should throw validationException if new connector addition violates the maximum group capacity rule")]
    public async Task Service_ThrowEx_ValidateForConnectorAddition(int groupMaxCapacity, int actualTotalGroupCapacity, int workloadFromNewConnector)
    {
        _chargeStationRepository.Read(Arg.Any<Guid>()).Returns(new ChargeStation
        {
            GroupId = Guid.NewGuid()
        });
        
        _groupRepository.Read(Arg.Any<Guid>()).Returns(new Group
        {
            CapacityInAmps = groupMaxCapacity
        });
        
        _connectorRepository.GetTotalMaxCurrentInAmpsOfGroup(Arg.Any<Guid>())
            .Returns(actualTotalGroupCapacity);

        var service = Instance.Of<GroupTotalAmpsValidator>(_groupRepository,_connectorRepository,_chargeStationRepository);
        
        await Assert.ThrowsAsync<ValidationException>(() => service.ValidateForConnectorAddition(workloadFromNewConnector, default));
    }
    
    [InlineData(2000,1200,100,1000)]
    [InlineData(100,85,3,20)]
    [InlineData(10,8,1,4)]
    [Theory(DisplayName = "Service should throw validationException if new connector update violates the maximum group capacity rule")]
    public async Task Service_ThrowEx_ValidateForConnectorUpdate(int groupMaxCapacity, int actualTotalGroupCapacity, int currentWorkloadOfConnector, int workloadFromUpdatingConnector)
    {
        _chargeStationRepository.Read(Arg.Any<Guid>()).Returns(new ChargeStation
        {
            GroupId = Guid.NewGuid()
        });
        
        _groupRepository.Read(Arg.Any<Guid>()).Returns(new Group
        {
            CapacityInAmps = groupMaxCapacity
        });

        _connectorRepository.Read(Arg.Any<Guid>(), Arg.Any<int>()).Returns(new Connector
        {
            MaxCurrentInAmps = currentWorkloadOfConnector
        });
        
        _connectorRepository.GetTotalMaxCurrentInAmpsOfGroup(Arg.Any<Guid>())
            .Returns(actualTotalGroupCapacity);

        var service = Instance.Of<GroupTotalAmpsValidator>(_groupRepository,_connectorRepository,_chargeStationRepository);
        
        await Assert.ThrowsAsync<ValidationException>(() => service.ValidateForConnectorUpdate(workloadFromUpdatingConnector, default, default));
    }
}