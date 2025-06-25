using System;
using System.Collections.Generic;

namespace Notifications
{
    public interface INotificationService
    {
        void RequestAuthorization();
        void ScheduleNotification(string title, string body, string subtitle, DateTime dateTime);
        bool IsNotificationAllowed();
        void OpenNotificationSettings(); }
}