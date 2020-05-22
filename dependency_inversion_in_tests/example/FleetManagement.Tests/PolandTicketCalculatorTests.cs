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

        private TrafficCode _trafficCodeMock;

        [SetUp]
        public void Setup()
        {
            _trafficCodeMock = new TrafficCode();

            _configurationProviderMock = Substitute.For<IConfigurationProvider>();
            _configurationProviderMock.TrafficCode.Returns(_trafficCodeMock);

            _calculatorUnderTest = new PolandTicketCalculator(_configurationProviderMock);
        }
    }
}