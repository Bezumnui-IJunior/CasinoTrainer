namespace Notifications
{
    public interface INotificationsFactory
    {
        void ScheduleRewardReminder();
        void ScheduleAdvertReadyReminder();
    }
}