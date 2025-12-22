using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxRecipient
    {
        [JsonPropertyName("chat_id")]
        public long ChatId { get; set; }

        [JsonPropertyName("chat_type")]
        public string ChatType { get; set; } // chat / channel / etc

        [JsonPropertyName("user_id")]
        public long? UserId { get; set; }
    }
}
