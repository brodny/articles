namespace FleetManagement.Configuration
{
    public interface ISpeedLimitElement
    {
        string CountryName { get; }
        int Limit { get; }
        SpeedUnit Unit { get; }
    }
}