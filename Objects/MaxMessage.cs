using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Сообщение в чате
    /// </summary>
    public class MaxMessage
    {
        /// <summary>
        /// Пользователь, отправивший сообщение
        /// </summary>
        [JsonPropertyName("sender")]
        public MaxUser Sender { get; set; } = null;

        /// <summary>
        /// Получатель сообщения. Может быть пользователем или чатом
        /// </summary>
        [JsonPropertyName("recipient")]
        public MaxRecipient Recipient { get; set; }

        /// <summary>
        /// Время создания сообщения в формате Unix-time
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// Пересланное или ответное сообщение
        /// </summary>
        [JsonPropertyName("link")]
        public MaxMessageLink Link { get; set; } = null;

        /// <summary>
        /// Содержимое сообщения. Текст + вложения. Может быть null, если сообщение содержит только пересланное сообщение
        /// </summary>
        [JsonPropertyName("body")]
        public MaxMessageBody Body { get; set; }

        /// <summary>
        /// Статистика сообщения.
        /// </summary>
        [JsonPropertyName("stat")]
        public MaxMessageStat Stat { get; set; } = null;

        /// <summary>
        /// Публичная ссылка на сообщение. Может быть null для диалогов или не публичных чатов
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = null;
    }
}
