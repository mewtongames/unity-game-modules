using System;

namespace MewtonGames.PushNotifications.Providers
{
    public interface IPushNotificationsProvider
    {
        public void ScheduleNotification(string title, string text, DateTime dateTime);
        public void CancelAllScheduledNotifications();
    }
}