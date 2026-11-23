using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxRecipient
    {
        [JsonPropertyName("chat_id")]
        public long ChatId { get; set; }

        [JsonPropertyName("chat_type")]
        public string? ChatType { get; set; } // dialog / chat / channel

        [JsonPropertyName("user_id")]
        public long? UserId { get; set; }

        /// <summary>
        /// ID поста — заполняется у комментариев (Recipient.post_id)
        /// </summary>
        [JsonPropertyName("post_id")]
        public string? PostId { get; set; } = null;
    }
}
