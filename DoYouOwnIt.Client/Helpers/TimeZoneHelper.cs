using System;

namespace DoYouOwnIt.Client.Helpers
{
    public class TimezoneHelper
    {
        public static DateTimeOffset ConvertUtcToUserTime(DateTime utcDateTime, string userTimeZoneId)
        {
            if (utcDateTime.Kind != DateTimeKind.Utc)
                utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

            TimeZoneInfo userZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);
            DateTime userDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, userZone);
            return new DateTimeOffset(userDateTime, userZone.GetUtcOffset(userDateTime));
        }

        public static string FormatUserFriendly(DateTime utcDateTime, string userTimeZoneId, string format = "G")
        {
            var localDateTime = ConvertUtcToUserTime(utcDateTime, userTimeZoneId);
            return localDateTime.ToString(format);
        }
    }
}
