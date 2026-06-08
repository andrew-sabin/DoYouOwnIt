using DoYouOwnIt.Client.Services.Interface;

namespace DoYouOwnIt.Client.Services
{
    public class UserTimeZoneService : IUserTimeZoneService
    {
        public string TimeZoneId { get; set; } = "UTC";

        public DateTimeOffset ConvertToUserTime(DateTime utcDateTime)
        {
            if (TimeZoneId == "UTC")
                return new DateTimeOffset(utcDateTime, TimeSpan.Zero);

            var tz = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
            var local = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tz);
            var offset = tz.GetUtcOffset(local);
            return new DateTimeOffset(local, offset);
        }
    }
}
