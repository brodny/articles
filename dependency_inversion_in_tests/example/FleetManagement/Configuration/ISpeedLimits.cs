namespace FleetManagement.Configuration
{
    public interface ISpeedLimits
    {
        SpeedLimitElement this[string countryName] { get; }
    }
}