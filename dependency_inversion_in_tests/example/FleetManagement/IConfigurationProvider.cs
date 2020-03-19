using FleetManagement.Configuration;

namespace FleetManagement
{
    public interface IConfigurationProvider
    {
        TrafficCode TrafficCode { get; }
    }
}