using FleetManagement;
using NUnit.Framework;

namespace ConfigProvider.Tests
{
    [TestFixture]
    public class SampleTest
    {
        [Test]
        public void CanCreateClass()
        {
            var configProvider = new ConfigurationProvider();
            Assert.NotNull(configProvider);
        }
    }
}