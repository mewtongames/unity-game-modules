using System;
using UnityEngine;

namespace MewtonGames.Time
{
    public static class TimeHelper
    {
        public static string FormatToString(int seconds, TimeFormat format)
        {
            string text;

            switch (format)
            {
                case TimeFormat.HoursMinutesSeconds:
                {
                    text = "00:00:00";
                    break;
                }
                case TimeFormat.MinutesSeconds:
                {
                    if (seconds <= 0)
                    {
                        text = "00:00";
                    }
                    else if (seconds >= 3599)
                    {
                        text = "59:59";
                    }
                    else
                    {
                        var m = Mathf.FloorToInt(seconds / 60f);
                        var s = seconds - m * 60;
                        text = $"{(m < 10 ? "0" + m : m)}:{(s < 10 ? "0" + s : s)}";
                    }
                    
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }

            return text;
        }
        
        public static string FormatToString(float seconds, TimeFormat format)
        {
            return FormatToString(Mathf.FloorToInt(seconds), format);
        }
        
        public static string SecondsToHoursMinutesFormat(int seconds, string hourChar, string minuteChar)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);

            var hours = (int)timeSpan.TotalHours;
            var minutes = timeSpan.Minutes;

            return $"{hours}{hourChar} {minutes}{minuteChar}";
        }

        public static int FormatToTimestamp(DateTime dateTime)
        {
            return (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static DateTime TimestampToDateTime(int timestamp)
        {
            return new DateTime(1970, 1, 1).AddSeconds(timestamp);
        }
    }
}