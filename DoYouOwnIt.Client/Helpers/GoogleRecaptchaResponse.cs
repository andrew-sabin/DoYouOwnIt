using System.Text.Json.Serialization;

namespace DoYouOwnIt.Client.Helpers
{
    public class GoogleRecaptchaResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("challenge_ts")]
        public DateTime ChallengeTimestamp { get; set; }

        [JsonPropertyName("hostname")]
        public string Hostname { get; set; } = string.Empty;

        [JsonPropertyName("error-codes")]
        public List<string> ErrorCodes { get; set; } = new List<string>();
    }
}
