using FleetManagement;
using NSubstitute;
using NUnit.Framework;

namespace ConfigProvider.Tests
{
    [TestFixture]
    public class PolandTicketCalculatorTests
    {
        private PolandTicketCalculator _calculatorUnderTest;

        private IConfigurationProvider _configurationProviderMock;

        [SetUp]
        public void Setup()
        {
            _configurationProviderMock = Substitute.For<IConfigurationProvider>();

            _calculatorUnderTest = new PolandTicketCalculator(_configurationProviderMock);
        }
    }
}