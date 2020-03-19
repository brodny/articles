using System.Configuration;
using FleetManagement.Configuration;

namespace FleetManagement
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public TrafficCode TrafficCode
        {
            get
            {
                return ConfigurationManager.GetSection("trafficCode") as TrafficCode;
            }
        }
    }
}