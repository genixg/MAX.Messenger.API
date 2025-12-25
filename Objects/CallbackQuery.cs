using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Объект callback из update_type = message_callback
    /// </summary>
    public class CallbackQuery
    {
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("callback_id")]
        public string CallbackId { get; set; } = null!;

        [JsonPropertyName("user")]
        public MaxUser? User { get; set; }

        [JsonPropertyName("payload")]
        public string? Payload { get; set; }
    }
}