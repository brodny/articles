using FleetManagement;
using NUnit.Framework;

namespace ConfigProvider.Tests
{
    [TestFixture]
    public class ConfigurationProviderTests
    {
        private ConfigurationProvider _providerUnderTest;

        [SetUp]
        public void Setup()
        {
            _providerUnderTest = new ConfigurationProvider();
        }

        [Test]
        public void CanCreateConfigurationProvider()
        {
            Assert.NotNull(_providerUnderTest);
        }

        [Test]
        public void CanReadTrafficCodeSection()
        {
            Assert.NotNull(_providerUnderTest.TrafficCode);
        }
    }
}