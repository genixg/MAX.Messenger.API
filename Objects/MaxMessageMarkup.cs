using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Элемент текстовой разметки (MarkupElement): тип + позиция + длина.
    /// Типы: strong, emphasized, monospaced, link, strikethrough, underline,
    /// user_mention, heading, highlighted, quote. См. <see cref="Enums.MarkupType"/>
    /// </summary>
    public class MaxMessageMarkup
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = null!; // strong, emphasized, link, ...

        /// <summary>Индекс начала элемента в тексте (с нуля)</summary>
        [JsonPropertyName("from")]
        public int From { get; set; }

        /// <summary>Длина элемента</summary>
        [JsonPropertyName("length")]
        public int Length { get; set; }

        /// <summary>URL ссылки (type=link)</summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; } = null;

        /// <summary>@username упомянутого пользователя (type=user_mention)</summary>
        [JsonPropertyName("user_link")]
        public string? UserLink { get; set; } = null;

        /// <summary>ID упомянутого пользователя без username (type=user_mention)</summary>
        [JsonPropertyName("user_id")]
        public long? UserId { get; set; } = null;
    }
}
