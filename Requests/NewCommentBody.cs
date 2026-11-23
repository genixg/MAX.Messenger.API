using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Requests
{
    /// <summary>
    /// Тело комментария к посту в канале (NewCommentBody).
    /// Вложения в комментариях не поддерживаются
    /// </summary>
    public class NewCommentBody
    {
        /// <summary>Текст комментария (до 4000 символов)</summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        /// <summary>Ответ на комментарий (type=reply) или пересылка</summary>
        [JsonPropertyName("link")]
        public Objects.MaxNewMessageLink? Link { get; set; } = null;

        /// <summary>"markdown" или "html" (см. Enums.TextFormat)</summary>
        [JsonPropertyName("format")]
        public string? TextFormat { get; set; }
    }
}
