using FleetManagement;
using FleetManagement.Configuration;
using NSubstitute;
using NUnit.Framework;

namespace ConfigProvider.Tests
{
    [TestFixture]
    public class PolandTicketCalculatorTests
    {
        private PolandTicketCalculator _calculatorUnderTest;

        private IConfigurationProvider _configurationProviderMock;

        private ITrafficCode _trafficCodeMock;

        private ISpeedLimits _speedLimitsMock;

        private ISpeedLimit _polandSpeedLimitMock;

        [SetUp]
        public void Setup()
        {
            _polandSpeedLimitMock = Substitute.For<ISpeedLimit>();
            _polandSpeedLimitMock.CountryName.Returns("Poland");
            _polandSpeedLimitMock.Limit.Returns(90);
            _polandSpeedLimitMock.Unit.Returns(SpeedUnit.Kmh);

            _speedLimitsMock = Substitute.For<ISpeedLimits>();
            _speedLimitsMock["Poland"].Returns(_polandSpeedLimitMock);
            
            _trafficCodeMock = Substitute.For<ITrafficCode>();
            _trafficCodeMock.SpeedLimits.Returns(_speedLimitsMock);

            _configurationProviderMock = Substitute.For<IConfigurationProvider>();
            _configurationProviderMock.TrafficCode.Returns(_trafficCodeMock);

            _calculatorUnderTest = new PolandTicketCalculator(_configurationProviderMock);
        }
    }
}