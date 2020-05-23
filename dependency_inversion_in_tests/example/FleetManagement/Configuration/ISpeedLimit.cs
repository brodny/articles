namespace FleetManagement.Configuration
{
    public interface ISpeedLimit
    {
        string CountryName { get; }
        int Limit { get; }
        SpeedUnit Unit { get; }
    }
}