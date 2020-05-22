namespace FleetManagement.Configuration
{
    public interface ITrafficCode
    {
        SpeedLimitCollection SpeedLimits { get; }
    }
}