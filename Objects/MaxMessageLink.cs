using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxMessageLink
    {
        /// <summary>
        /// Тип связанного сообщения Enum: "forward" "reply"
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } // forward / reply

        /// <summary>
        /// Пользователь, отправивший сообщение.
        /// </summary>
        [JsonPropertyName("sender")]
        public MaxUser Sender { get; set; }

        /// <summary>
        /// Чат, в котором сообщение было изначально опубликовано. Только для пересланных сообщений
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long? ChatId { get; set; }

        /// <summary>
        /// Схема, представляющая тело сообщения
        /// </summary>
        [JsonPropertyName("message")]
        public MaxMessageBody Message { get; set; }
    }

    public class MaxNewMessageLink
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } // forward / reply

        [JsonPropertyName("mid")]
        public string MessageId { get; set; }
    }
}
