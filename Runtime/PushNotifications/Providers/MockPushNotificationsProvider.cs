using System;
#if DEV_MODE && MEWTONGAMES_MODULES_LOGS
using UnityEngine;
#endif

namespace MewtonGames.PushNotifications.Providers
{
    public class MockPushNotificationsProvider : IPushNotificationsProvider
    {
        public void ScheduleNotification(string title, string text, DateTime dateTime)
        {
#if DEV_MODE && MEWTONGAMES_MODULES_LOGS
            Debug.Log($"UnityNotificationsProvider.ScheduleNotification, title: { title }, text: { text }, date: { dateTime.Day }.{ dateTime.Month }.{ dateTime.Year } { dateTime.Hour }:{ dateTime.Minute }");
#endif
        }

        public void CancelAllScheduledNotifications()
        {
#if DEV_MODE && MEWTONGAMES_MODULES_LOGS
            Debug.Log("MockNotificationsProvider.CancelAllScheduledNotifications");
#endif
        }
    }
}