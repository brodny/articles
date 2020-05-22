namespace FleetManagement.Configuration
{
    public interface ITrafficCode
    {
        ISpeedLimitCollection SpeedLimits { get; }
    }
}