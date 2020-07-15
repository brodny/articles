namespace FleetManagement.Configuration
{
    public interface ISpeedLimits
    {
        ISpeedLimit this[string countryName] { get; }
    }
}