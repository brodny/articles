namespace FleetManagement.Configuration
{
    public interface ISpeedLimitCollection
    {
        SpeedLimitElement this[string countryName] { get; }
    }
}