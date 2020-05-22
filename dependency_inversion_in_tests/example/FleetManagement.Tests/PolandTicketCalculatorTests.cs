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

        private SpeedLimitCollection _speedLimitCollectionMock;

        [SetUp]
        public void Setup()
        {
            _speedLimitCollectionMock = new SpeedLimitCollection();
            
            _trafficCodeMock = Substitute.For<ITrafficCode>();
            _trafficCodeMock.SpeedLimits.Returns(_speedLimitCollectionMock);

            _configurationProviderMock = Substitute.For<IConfigurationProvider>();
            _configurationProviderMock.TrafficCode.Returns(_trafficCodeMock);

            _calculatorUnderTest = new PolandTicketCalculator(_configurationProviderMock);
        }
    }
}