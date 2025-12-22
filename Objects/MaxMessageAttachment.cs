using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxMessageAttachment
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } // image, video, file, etc

        /// <summary>
        /// Запрос на прикрепление изображения (все поля являются взаимоисключающими)
        /// </summary>
        [JsonPropertyName("payload")]
        public MaxAttachmentPayload? Payload { get; set; }
    }

    /// <summary>
    /// Запрос на прикрепление изображения (все поля являются взаимоисключающими)
    /// </summary>
    public class MaxAttachmentPayload
    {
        /// <summary>
        /// Токены, полученные после загрузки изображений
        /// </summary>
        [JsonPropertyName("photos")]
        public IEnumerable<string>? Photos { get; set; }

        /// <summary>
        /// Токен существующего вложения
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>
        /// Любой внешний URL изображения, которое вы хотите прикрепить
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
