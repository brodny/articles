using FleetManagement;
using FleetManagement.Configuration;
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

        [Test]
        public void CanReadSpeedLimitsSection()
        {
            Assert.NotNull(_providerUnderTest.TrafficCode.SpeedLimits);
        }

        [Test]
        public void ShouldReadSpeedLimitsForPoland()
        {
            ISpeedLimitElement speedLimitInPoland = _providerUnderTest.TrafficCode.SpeedLimits["Poland"];

            Assert.NotNull(speedLimitInPoland);

            Assert.AreEqual("Poland", speedLimitInPoland.CountryName);
            Assert.AreEqual(90, speedLimitInPoland.Limit);
            Assert.AreEqual(SpeedUnit.Kmh, speedLimitInPoland.Unit);
        }

        [Test]
        public void ShouldReadSpeedLimitsForUnitedKingdom()
        {
            ISpeedLimitElement speedLimitInPoland = _providerUnderTest.TrafficCode.SpeedLimits["UK"];

            Assert.NotNull(speedLimitInPoland);

            Assert.AreEqual("UK", speedLimitInPoland.CountryName);
            Assert.AreEqual(60, speedLimitInPoland.Limit);
            Assert.AreEqual(SpeedUnit.Mph, speedLimitInPoland.Unit);
        }
    }
}