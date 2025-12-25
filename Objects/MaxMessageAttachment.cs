using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxMessageAttachment
    {
        /// <summary>
        /// image, video, file, inline_keyboard, etc
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Payload зависит от type.
        /// Например:
        /// - image/file/video: MaxAttachmentPayload
        /// - inline_keyboard: InlineKeyboardAttachmentRequestPayload
        /// </summary>
        [JsonPropertyName("payload")]
        public object? Payload { get; set; }
    }

    /// <summary>
    /// Payload для вложений с token/url/photos (картинки/файлы и т.п.)
    /// (поля взаимоисключающие, поэтому nullable)
    /// </summary>
    public class MaxAttachmentPayload
    {
        [JsonPropertyName("photos")]
        public IEnumerable<string>? Photos { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
