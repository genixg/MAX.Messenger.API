using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Комментарий к посту в канале (CommentMessage).
    /// В отличие от Message — не имеет публичного url, а body не содержит вложений
    /// </summary>
    public class MaxCommentMessage
    {
        /// <summary>
        /// Автор комментария. Может быть null, если комментарий опубликован от имени канала
        /// </summary>
        [JsonPropertyName("sender")]
        public MaxUser? Sender { get; set; }

        /// <summary>
        /// Получатель комментария (для комментариев — только канал, содержит post_id)
        /// </summary>
        [JsonPropertyName("recipient")]
        public MaxRecipient? Recipient { get; set; }

        /// <summary>
        /// Время создания комментария, Unix-time в миллисекундах
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// Пересланный или прокомментированный ответом комментарий
        /// </summary>
        [JsonPropertyName("link")]
        public MaxCommentLinkedMessage? Link { get; set; }

        /// <summary>
        /// Тело комментария (текст без вложений)
        /// </summary>
        [JsonPropertyName("body")]
        public MaxCommentMessageBody? Body { get; set; }

        /// <summary>
        /// Статистика (только для каналов в контексте GET /messages)
        /// </summary>
        [JsonPropertyName("stat")]
        public MaxMessageStat? Stat { get; set; }
    }

    /// <summary>
    /// Тело комментария. В отличие от MaxMessageBody — без attachments
    /// </summary>
    public class MaxCommentMessageBody
    {
        /// <summary>Уникальный ID комментария</summary>
        [JsonPropertyName("mid")]
        public string Mid { get; set; } = null!;

        /// <summary>ID сообщения в чате</summary>
        [JsonPropertyName("seq")]
        public long Seq { get; set; }

        /// <summary>Текст комментария</summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        /// <summary>Разметка текста комментария</summary>
        [JsonPropertyName("markup")]
        public IEnumerable<MaxMessageMarkup>? Markup { get; set; }
    }

    /// <summary>
    /// Связь комментария с исходным комментарием (forward/reply)
    /// </summary>
    public class MaxCommentLinkedMessage
    {
        /// <summary>"forward" или "reply". См. <see cref="Enums.MessageLinkType"/></summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;

        /// <summary>Автор исходного комментария</summary>
        [JsonPropertyName("sender")]
        public MaxUser? Sender { get; set; }

        /// <summary>Канал, где был опубликован исходный комментарий</summary>
        [JsonPropertyName("chat_id")]
        public long? ChatId { get; set; }

        /// <summary>Исходный комментарий</summary>
        [JsonPropertyName("message")]
        public MaxCommentMessageBody Message { get; set; } = null!;
    }
}
