namespace FleetManagement.Configuration
{
    public interface ISpeedLimits
    {
        ISpeedLimitElement this[string countryName] { get; }
    }
}