using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Объект Update: событие, приходящее через Webhook или GET /updates (Long Polling).
    /// Набор заполненных полей зависит от UpdateType.
    /// </summary>
    public class MaxUpdate
    {
        /// <summary>
        /// Тип события. См. <see cref="Enums.UpdateType"/>
        /// </summary>
        [JsonPropertyName("update_type")]
        public string? UpdateType { get; set; }

        /// <summary>
        /// Время наступления события, Unix-time в миллисекундах
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }

        /// <summary>
        /// Сообщение (message_created, message_edited, message_callback, comment_created, comment_edited)
        /// </summary>
        [JsonPropertyName("message")]
        public MaxMessage? Message { get; set; }

        /// <summary>
        /// Локаль пользователя в формате IETF BCP 47
        /// </summary>
        [JsonPropertyName("user_locale")]
        public string? UserLocale { get; set; }

        /// <summary>
        /// Пользователь — источник события (bot_started, bot_stopped, dialog_*, user_added, user_removed, chat_title_changed).
        /// С ноября 2026 не возвращается для bot_added
        /// </summary>
        [JsonPropertyName("user")]
        public MaxUser? User { get; set; }

        /// <summary>
        /// ID пользователя (bot_admin_permissions_changed)
        /// </summary>
        [JsonPropertyName("user_id")]
        public long? UserId { get; set; }

        /// <summary>
        /// ID чата/диалога, где произошло событие
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long? ChatId { get; set; }

        /// <summary>
        /// Payload из deep-link, переданный при старте бота (bot_started)
        /// </summary>
        [JsonPropertyName("payload")]
        public string? Payload { get; set; }

        /// <summary>
        /// Объект callback (message_callback)
        /// </summary>
        [JsonPropertyName("callback")]
        public CallbackQuery? Callback { get; set; }

        /// <summary>
        /// ID удалённого сообщения (message_removed, comment_removed)
        /// </summary>
        [JsonPropertyName("message_id")]
        public string? MessageId { get; set; }

        /// <summary>
        /// ID поста, к которому относится комментарий (comment_removed)
        /// </summary>
        [JsonPropertyName("post_id")]
        public string? PostId { get; set; }

        /// <summary>
        /// Новое название чата (chat_title_changed)
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Событие произошло в канале (true) или групповом чате (false):
        /// bot_added, bot_removed, user_added, user_removed, bot_admin_permissions_changed
        /// </summary>
        [JsonPropertyName("is_channel")]
        public bool? IsChannel { get; set; }

        /// <summary>
        /// Является ли бот администратором после изменения прав (bot_admin_permissions_changed)
        /// </summary>
        [JsonPropertyName("is_admin")]
        public bool? IsAdmin { get; set; }

        /// <summary>
        /// ID бота, у которого изменились права (bot_admin_permissions_changed)
        /// </summary>
        [JsonPropertyName("bot_id")]
        public long? BotId { get; set; }

        /// <summary>
        /// ID пользователя, добавившего участника (user_added; null, если присоединился по ссылке)
        /// </summary>
        [JsonPropertyName("inviter_id")]
        public long? InviterId { get; set; }

        /// <summary>
        /// ID администратора, удалившего участника (user_removed; null, если вышел сам)
        /// </summary>
        [JsonPropertyName("admin_id")]
        public long? AdminId { get; set; }

        /// <summary>
        /// До какого времени заглушён диалог, Unix-time (dialog_muted)
        /// </summary>
        [JsonPropertyName("muted_until")]
        public long? MutedUntil { get; set; }

        /// <summary>
        /// Новые права бота-администратора (bot_admin_permissions_changed). См. <see cref="Enums.ChatAdminPermission"/>
        /// </summary>
        [JsonPropertyName("permissions")]
        public IEnumerable<string>? Permissions { get; set; }
    }
}
