namespace DoYouOwnIt.Client.Services.Interface
{
    public interface IUserTimeZoneService
    {
        string TimeZoneId { get; set; }
        DateTimeOffset ConvertToUserTime(DateTime utcDateTime);
    }
}
