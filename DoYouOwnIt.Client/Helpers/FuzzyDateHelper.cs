using System;

namespace DoYouOwnIt.Client.Helpers
{
    public static class FuzzyDateHelper
    {
        public static string ToFuzzyDate(this DateTime utcDateTime)
        {
            return ToFuzzyDate(utcDateTime, DateTime.UtcNow);
        }

        internal static string ToFuzzyDate(DateTime utcDateTime, DateTime utcNow)
        {
            if (utcDateTime.Kind != DateTimeKind.Utc)
                utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

            TimeSpan diff = utcNow - utcDateTime;

            if (diff.TotalSeconds < 0)
                return "In the year 3000";

            if (diff.TotalSeconds < 5)
                return "A few seconds ago";

            if (diff.TotalSeconds < 60)
                return $"{diff.Seconds} seconds ago";

            if (diff.TotalMinutes < 60)
            {
                int mins = (int)diff.TotalMinutes;
                return mins == 1 ? "1 minute ago" : $"{mins} minutes ago";
            }

            if (diff.TotalHours < 24)
            {
                int hours = (int)diff.TotalHours;
                return hours == 1 ? "1 hour ago" : $"{hours} hours ago";
            }

            if (diff.TotalDays < 7)
            {
                int days = (int)diff.TotalDays;
                return days switch
                {
                    0 => "today",
                    1 => "yesterday",
                    _ => $"{days} days ago"
                };
            }

            if (diff.TotalDays < 30)
            {
                int weeks = ((int)diff.TotalDays / 7);
                return weeks == 1 ? "last week" : $"{weeks} weeks ago";
            }

            if (diff.TotalDays < 365)
            {
                int months = (int)(diff.TotalDays / 30);
                return months == 1 ? "last month" : $"{months} months ago";
            }

            int years = (int)(diff.TotalDays / 365);
            return years == 1 ? "last year" : $"{years} years ago";
        }
    }
}
