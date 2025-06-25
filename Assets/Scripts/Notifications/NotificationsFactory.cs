using System;
using Progress;
using VContainer;

namespace Notifications
{
    public class NotificationsFactory : INotificationsFactory
    {
        private readonly INotificationService _notificationService;
        private readonly IPlayerData _playerData;

        [Inject]
        public NotificationsFactory(INotificationService notificationService, IPlayerData playerData)
        {
            _notificationService = notificationService;
            _playerData = playerData;
        }

        public void ScheduleRewardReminder()
        {
            if (_notificationService.IsNotificationAllowed() == false)
                return;
            
            DateTime dateTime = _playerData.WhenCanReceiveBonus;
            
            if (dateTime < DateTime.Now)
                return;
            
            _notificationService.ScheduleNotification(
                SettingsConstants.NotificationName,
                "\ud83d\udcb0\ud83e\udd11Hey gambler! Your free bonus is ready. Come here to collect it!\ud83d\udcb0\ud83e\udd11", 
                "", 
                dateTime);
        }
        
        public void ScheduleAdvertReadyReminder()
        {
            if (_notificationService.IsNotificationAllowed() == false)
                return;
            
            DateTime dateTime = _playerData.WhenCanWatchAdvert;
            
            if (dateTime < DateTime.Now)
                return;
            
            _notificationService.ScheduleNotification(
                SettingsConstants.NotificationName,
                "\ud83d\udcb0\ud83e\udd11You can watch advert to get some extra money!\ud83c\udf9f", 
                "", 
                dateTime);
        }
        
    }
}