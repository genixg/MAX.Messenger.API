using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxChat
    {
        /// <summary>
        /// ID чата
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long Id { get; set; }

        /// <summary>
        /// Тип чата: "dialog", "chat" или "channel". См. <see cref="Enums.ChatType"/>
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;

        /// <summary>
        /// Статус чата для бота: Enum: "active" "removed" "left" "closed" "suspended".
        /// См. <see cref="Enums.ChatStatus"/>
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = null!;
        //"active" — Бот является активным участником чата.
        //"removed" — Бот был удалён из чата.
        //"left" — Бот покинул чат.
        //"closed" — Чат был закрыт.
        //"suspended" — Бот остановлен пользователем (только для диалогов).

        /// <summary>
        /// Отображаемое название чата. Может быть null для диалогов
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Иконка чата
        /// </summary>
        [JsonPropertyName("icon")]
        public MaxImage? Icon { get; set; }

        /// <summary>
        /// Время последнего события в чате
        /// </summary>
        [JsonPropertyName("last_event_time")]
        public long LastEventTimestamp { get; set; }

        /// <summary>
        /// Количество участников чата. Для диалогов всегда 2
        /// </summary>
        [JsonPropertyName("participants_count")]
        public int ParticipantsCount { get; set; }

        /// <summary>
        /// Количество сообщений в чате. Только для групповых чатов и каналов, для диалогов недоступно
        /// </summary>
        [JsonPropertyName("messages_count")]
        public int? MessagesCount { get; set; } = null;

        /// <summary>
        /// ID владельца чата
        /// </summary>
        [JsonPropertyName("owner_id")]
        public long? OwnerId { get; set; } = null;

        /// <summary>
        /// Участники чата: ключ — user_id, значение — время последней активности (last_access_time).
        /// Видно только администраторам; может быть null, если запрашивается список чатов
        /// </summary>
        [JsonPropertyName("participants")]
        public Dictionary<long, long>? Participants { get; set; } = null;

        /// <summary>
        /// Доступен ли чат публично (для диалогов всегда false)
        /// </summary>
        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// Ссылка на чат
        /// </summary>
        [JsonPropertyName("link")]
        public string? Link { get; set; } = null;

        /// <summary>
        /// Описание чата
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; } = null;


        /// <summary>
        /// Данные о пользователе в диалоге (только для чатов типа "dialog")
        /// </summary>
        [JsonPropertyName("dialog_with_user")]
        public MaxUserWithPhoto? DialogWithUser { get; set; } = null;

        /// <summary>
        /// ID сообщения, содержащего кнопку, через которую был инициирован чат
        /// </summary>
        [JsonPropertyName("chat_message_id")]
        public string? ChatMessageId { get; set; } = null;

        /// <summary>
        /// Закреплённое сообщение в чате (возвращается только при запросе конкретного чата)
        /// </summary>
        [JsonPropertyName("pinned_message")]
        public MaxMessage? PinnedMessage { get; set; } = null;
    }
}
