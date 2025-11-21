using System;

namespace MewtonGames.Time
{
    public static class TimeFormatExtensions
    {
        public static string ConvertToStringFormat(this TimeFormat timeFormat)
        {
            return timeFormat switch
            {
                TimeFormat.HoursMinutesSeconds => @"hh\:mm\:ss",
                TimeFormat.MinutesSeconds => @"mm\:ss",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}