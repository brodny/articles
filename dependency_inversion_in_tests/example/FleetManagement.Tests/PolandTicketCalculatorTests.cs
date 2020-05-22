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

        [SetUp]
        public void Setup()
        {
            _trafficCodeMock = Substitute.For<ITrafficCode>();

            _configurationProviderMock = Substitute.For<IConfigurationProvider>();
            _configurationProviderMock.TrafficCode.Returns(_trafficCodeMock);

            _calculatorUnderTest = new PolandTicketCalculator(_configurationProviderMock);
        }
    }
}