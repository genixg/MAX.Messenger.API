using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Пересланное или ответное сообщение (LinkedMessage)
    /// </summary>
    public class MaxMessageLink
    {
        /// <summary>
        /// Тип связанного сообщения Enum: "forward" "reply". См. <see cref="Enums.MessageLinkType"/>
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;

        /// <summary>
        /// Пользователь, отправивший сообщение. Может быть null, если пост опубликован от имени канала
        /// </summary>
        [JsonPropertyName("sender")]
        public MaxUser? Sender { get; set; } = null;

        /// <summary>
        /// Чат, в котором сообщение было изначально опубликовано. Только для пересланных сообщений
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long? ChatId { get; set; }

        /// <summary>
        /// Схема, представляющая тело сообщения
        /// </summary>
        [JsonPropertyName("message")]
        public MaxMessageBody Message { get; set; } = null!;
    }

    /// <summary>
    /// Ссылка на сообщение для исходящего reply/forward (NewMessageLink)
    /// </summary>
    public class MaxNewMessageLink
    {
        /// <summary>"forward" или "reply". См. <see cref="Enums.MessageLinkType"/></summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;

        /// <summary>ID исходного сообщения (mid)</summary>
        [JsonPropertyName("mid")]
        public string MessageId { get; set; } = null!;
    }
}
