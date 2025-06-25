using System;
using System.Collections.Generic;
using Infrastructure;
using Unity.Notifications.iOS;
using VContainer;

namespace Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly ICoroutineExecutor _coroutineExecutor;

        [Inject]
        public NotificationService(ICoroutineExecutor coroutineExecutor)
        {
            _coroutineExecutor = coroutineExecutor;
        }

        public bool IsNotificationAllowed() =>
            iOSNotificationCenter.GetNotificationSettings().BadgeSetting == NotificationSetting.Enabled;

        public void RequestAuthorization()
        {
            _coroutineExecutor.StartCoroutine(RequestAuthorizationEnumerator());
        }

        public void OpenNotificationSettings()
        {
            iOSNotificationCenter.OpenNotificationSettings();
        }

        public void ScheduleNotification(string title, string body, string subtitle, DateTime dateTime)
        {
            iOSNotificationCalendarTrigger trigger = new iOSNotificationCalendarTrigger
            {
                Year = dateTime.Year,
                Month = dateTime.Month,
                Minute = dateTime.Minute,
                Second = dateTime.Second,
                Repeats = false,
            };

            ScheduleNotification(title, body, subtitle, trigger);
        }
        

        private IEnumerator<bool?> RequestAuthorizationEnumerator()
        {
            using AuthorizationRequest request = new AuthorizationRequest(AuthorizationOption.Alert | AuthorizationOption.Badge | AuthorizationOption.Sound, true);

            while (!request.IsFinished)
                yield return null;

            yield return request.Granted;
        }

        private void ScheduleNotification(string title, string body, string subtitle, iOSNotificationTrigger trigger)
        {
            iOSNotification iOSNotification = new iOSNotification
            {
                Title = title,
                Body = body,
                Subtitle = subtitle,
                ShowInForeground = true,
                ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Sound,
                Trigger = trigger,
            };

            iOSNotificationCenter.ScheduleNotification(iOSNotification);
        }
    }
}