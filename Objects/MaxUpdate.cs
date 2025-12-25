using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxUpdate
    {
        [JsonPropertyName("update_type")]
        public string? UpdateType { get; set; }

        [JsonPropertyName("timestamp")]
        public long? Timestamp;

        [JsonPropertyName("message")]
        public MaxMessage? Message { get; set; }

        [JsonPropertyName("user_locale")]
        public string? UserLocale { get; set; }

        /// <summary>
        /// User Для bot_created
        /// </summary>
        [JsonPropertyName("user")]
        public MaxUserWithPhoto? User { get; set; }

        /// <summary>
        /// UserId Для bot_created
        /// </summary>
        [JsonPropertyName("user_id")]
        public long? UserId { get; set; } = null;

        /// <summary>
        /// ChatId Для bot_created
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long? ChatId { get; set; } = null;

        /// <summary>
        /// Payload diplinking
        /// </summary>
        [JsonPropertyName("payload")]
        public string? Payload { get; set; } = null;

        [JsonPropertyName("callback")]
        public CallbackQuery? Callback { get; set; }
    }
}
