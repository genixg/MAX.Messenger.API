using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxUpdate
    {
        [JsonPropertyName("update_type")]
        public string UpdateType { get; set; } = null!;

        [JsonPropertyName("timestamp")]
        public long Timestamp;

        [JsonPropertyName("message")]
        public MaxMessage Message { get; set; }

        [JsonPropertyName("user_locale")]
        public string UserLocale { get; set; }
    }
}
