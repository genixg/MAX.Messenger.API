using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Схема, представляющая тело сообщения
    /// </summary>
    public class MaxMessageBody
    {
        /// <summary>
        /// Уникальный ID сообщения
        /// </summary>
        [JsonPropertyName("mid")]
        public string Mid { get; set; }

        /// <summary>
        /// ID последовательности сообщения в чате
        /// </summary>
        [JsonPropertyName("seq")]
        public long Seq { get; set; }

        /// <summary>
        /// Новый текст сообщения
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Вложения сообщения. Могут быть одним из типов Attachment. Смотрите описание схемы
        /// </summary>
        [JsonPropertyName("attachments")]
        public IEnumerable<MaxMessageAttachment> Attachments { get; set; }

        /// <summary>
        /// Разметка текста сообщения. Для подробной информации загляните в раздел Форматирование документации
        /// </summary>
        [JsonPropertyName("markup")]
        public IEnumerable<MaxMessageMarkup> Markup { get; set; }
    }
}
