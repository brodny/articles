using System.Configuration;

namespace FleetManagement.Configuration
{

    public class SpeedLimitElement : ConfigurationElement, ISpeedLimitElement
    {
        private const string NamePropertyName = "name";
        private const string LimitPropertyName = "limit";
        private const string UnitPropertyName = "unit";

        [ConfigurationProperty(NamePropertyName, IsRequired = true, IsKey = true)]
        public string CountryName
        {
            get { return (string)base[NamePropertyName]; }
        }

        [ConfigurationProperty(LimitPropertyName, IsRequired = true)]
        public int Limit
        {
            get { return (int)base[LimitPropertyName]; }
        }

        [ConfigurationProperty(UnitPropertyName, IsRequired = true)]
        public SpeedUnit Unit
        {
            get { return (SpeedUnit)base[UnitPropertyName]; }
        }
    }
}