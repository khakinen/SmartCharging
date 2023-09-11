using Ninstance;
using NSubstitute;
using SmartCharging.Domain.Contract.Connectors;
using SmartCharging.Domain.Implementation.ChargeStations;

namespace Domain.Implementation.Test.ChargeStations;

public class ChargeStationServiceTests
{
    private readonly IConnectorService _connectorService;

    public ChargeStationServiceTests()
    {
        _connectorService = Substitute.For<IConnectorService>();
    }

    [Fact(DisplayName = "All dependent connectors should be removed when chargeStation is being removed")]
    public async Task Service_Should_RemoveConnectors()
    {
        var fakeChargeStationId = Guid.NewGuid();

        var fakeConnectors = Enumerable.Range(0, 5).Select(i => new Connector
        {
            ConnectorNumber = i,
            MaxCurrentInAmps = 77,
            ChargeStationId = fakeChargeStationId
        }).ToList();

        _connectorService.GetByChargeStationId(Arg.Any<Guid>())
            .Returns(fakeConnectors);

        var service = Instance.Of<ChargeStationService>(_connectorService);

        await service.RemoveChargeStation(fakeChargeStationId);

        foreach (var fakeConnector in fakeConnectors)
        {
            await _connectorService.Received().RemoveConnector(fakeConnector.ChargeStationId, fakeConnector.ConnectorNumber);
        }
    }
}