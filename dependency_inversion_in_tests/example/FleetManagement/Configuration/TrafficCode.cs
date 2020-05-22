using System.Configuration;

namespace FleetManagement.Configuration
{
    public class TrafficCode : ConfigurationSection, ITrafficCode
    {
        private const string SpeedLimitsPropertyName = "speedLimits";

        [ConfigurationProperty(SpeedLimitsPropertyName)]
        public SpeedLimitCollection SpeedLimits
        {
            get
            {
                return (SpeedLimitCollection)base[SpeedLimitsPropertyName];
            }
        }
    }
}