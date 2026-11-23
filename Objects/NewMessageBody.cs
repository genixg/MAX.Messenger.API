using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Requests
{
    /// <summary>
    /// Тело нового сообщения для отправки/редактирования (NewMessageBody)
    /// </summary>
    public class NewMessageBody
    {
        /// <summary>Текст сообщения (до 4000 символов)</summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("attachments")]
        public IEnumerable<Objects.MaxMessageAttachment>? Attachments { get; set; }

        [JsonPropertyName("link")]
        public Objects.MaxNewMessageLink? Link { get; set; } = null;

        /// <summary>Если false — участники чата не получат уведомление. Для каналов нельзя false</summary>
        [JsonPropertyName("notify")]
        public bool Notify { get; set; } = true;

        /// <summary>"markdown" или "html" (см. Enums.TextFormat). null — обычный текст</summary>
        [JsonPropertyName("format")]
        public string? TextFormat { get; set; }
    }
}
