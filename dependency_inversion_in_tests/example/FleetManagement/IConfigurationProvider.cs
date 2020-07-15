using FleetManagement.Configuration;

namespace FleetManagement
{
    public interface IConfigurationProvider
    {
        ITrafficCode TrafficCode { get; }
    }
}