using System.Configuration;

namespace FleetManagement.Configuration
{
    public class TrafficCode : ConfigurationSection, ITrafficCode
    {
        private const string SpeedLimitsPropertyName = "speedLimits";

        public ISpeedLimits SpeedLimits
        {
            get
            {
                return SpeedLimitsImpl;
            }
        }

        [ConfigurationProperty(SpeedLimitsPropertyName)]
        private SpeedLimitCollection SpeedLimitsImpl
        {
            get
            {
                return (SpeedLimitCollection)base[SpeedLimitsPropertyName];
            }
        }
    }
}