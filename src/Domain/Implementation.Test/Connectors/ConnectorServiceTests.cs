using Ninstance;
using NSubstitute;
using SmartCharging.Data.Contract.Repositories;
using SmartCharging.Domain.Contract.Commands;
using SmartCharging.Domain.Contract.Connectors;
using SmartCharging.Domain.Contract.Exceptions;
using SmartCharging.Domain.Contract.Settings;
using SmartCharging.Domain.Implementation.Connectors;

namespace Domain.Implementation.Test.Connectors;

public class ConnectorServiceTests
{
    private readonly IConnectorRepository _connectorRepository;
    private readonly IAppSettings _appSettings;


    public ConnectorServiceTests()
    {
        _connectorRepository = Substitute.For<IConnectorRepository>();
        _appSettings = Substitute.For<IAppSettings>();
    }

    [InlineData(66,7,1,2,3,4,5,6,8,9,10,17,29)]
    [InlineData(5,1,2,3,4,5)]
    [InlineData(5,3,1,2,4,5)]
    [InlineData(95,2,1,4,54,55)]
    [Theory(DisplayName = "Valid connector number should be given while creating a new one")]
    public async Task Service_Should_GiveValidNumber(int maxAllowedConnectorNumber, int expectedConnectorNumber,params int[] existingConnectorNumbers)
    {
        var fakeChargeStationId = Guid.NewGuid();

        var fakeConnectors = new List<Connector>();

        for (int i = 0; i < existingConnectorNumbers.Length; i++)
        {
            fakeConnectors.Add(new Connector
            {
                ConnectorNumber = existingConnectorNumbers[i],
                MaxCurrentInAmps = 77,
                ChargeStationId = fakeChargeStationId
            });
        }
        
        _connectorRepository.ReadByChargeStationId(Arg.Any<Guid>())
            .Returns(fakeConnectors);

        _appSettings.MaxConnectorCountOfChargeStation.Returns(maxAllowedConnectorNumber);

        var service = Instance.Of<ConnectorService>(_connectorRepository, _appSettings);

        await service.CreateConnector(new CreateConnectorCommand());
            
        await _connectorRepository.Received().CreateConnector(Arg.Is<CreateConnectorCommand>(c=>c.ConnectorNumber==expectedConnectorNumber));
    }
    
    [InlineData(5,5)]
    [InlineData(555,555)]
    [InlineData(4,4)]
    [InlineData(23,23)]
    [Theory(DisplayName = "while creating a new connector, a conflict exception should be thrown if there is already maximum allowed of connectors")]
    public async Task Service_Should_ThrowException(int maxAllowedConnectorNumber, int existingConnectorCount)
    {
        var fakeChargeStationId = Guid.NewGuid();

        var fakeConnectors = Enumerable.Range(1, existingConnectorCount).Select(i => new Connector
        {
            ConnectorNumber = i,
        }).ToList();

        _connectorRepository.ReadByChargeStationId(Arg.Any<Guid>())
            .Returns(fakeConnectors);

        _appSettings.MaxConnectorCountOfChargeStation.Returns(maxAllowedConnectorNumber);

        var service = Instance.Of<ConnectorService>(_connectorRepository, _appSettings);

        await Assert.ThrowsAsync<ConflictException>(() =>  service.CreateConnector(new CreateConnectorCommand()));
    }
}