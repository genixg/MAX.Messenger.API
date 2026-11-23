using MAX.Messenger.API.Objects;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Requests
{
    /// <summary>
    /// Патч информации о чате/канале (ChatPatch) для PATCH /chats/{chatId}
    /// </summary>
    public class MaxChatPatch
    {
        /// <summary>Иконка чата (payload загруженного изображения)</summary>
        [JsonPropertyName("icon")]
        public MaxPhotoAttachmentRequestPayload? Icon { get; set; } = null;

        /// <summary>Новое название (1-200 символов)</summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; } = null;

        /// <summary>
        /// Новое описание до 16000 символов. Пустая строка — удалить описание
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; } = null;

        /// <summary>ID сообщения для закрепления (для снятия закрепления используйте UnpinMessageAsync)</summary>
        [JsonPropertyName("pin")]
        public string? Pin { get; set; } = null;

        /// <summary>Уведомить участников системным сообщением (по умолчанию true)</summary>
        [JsonPropertyName("notify")]
        public bool? Notify { get; set; } = true;
    }

    /// <summary>
    /// Тело закрепления сообщения (PinMessageBody) для PUT /chats/{chatId}/pin
    /// </summary>
    public class MaxPinMessageBody
    {
        /// <summary>ID закрепляемого сообщения</summary>
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; } = null!;

        /// <summary>Уведомить участников системным сообщением</summary>
        [JsonPropertyName("notify")]
        public bool? Notify { get; set; } = true;
    }

    /// <summary>
    /// Тело действия бота в чате (ActionRequestBody) для POST /chats/{chatId}/actions
    /// </summary>
    public class MaxActionRequestBody
    {
        /// <summary>Действие. См. <see cref="Enums.SenderAction"/></summary>
        [JsonPropertyName("action")]
        public string Action { get; set; } = null!;
    }

    /// <summary>
    /// Список ID пользователей (UserIdsList) для POST /chats/{chatId}/members
    /// </summary>
    public class MaxUserIdsList
    {
        /// <summary>До 100 идентификаторов</summary>
        [JsonPropertyName("user_ids")]
        public long[] UserIds { get; set; } = System.Array.Empty<long>();
    }

    /// <summary>
    /// Администратор чата с правами (ChatAdmin)
    /// </summary>
    public class MaxChatAdmin
    {
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        /// <summary>Права. См. <see cref="Enums.ChatAdminPermission"/></summary>
        [JsonPropertyName("permissions")]
        public string[] Permissions { get; set; } = System.Array.Empty<string>();

        /// <summary>Псевдоним администратора в чате</summary>
        [JsonPropertyName("alias")]
        public string? Alias { get; set; } = null;
    }

    /// <summary>
    /// Список администраторов (ChatAdminsList) для POST /chats/{chatId}/members/admins
    /// </summary>
    public class MaxChatAdminsList
    {
        [JsonPropertyName("admins")]
        public MaxChatAdmin[] Admins { get; set; } = System.Array.Empty<MaxChatAdmin>();
    }

    /// <summary>
    /// Тело PATCH /me/commands — установка команд бота (BotCommandsPatch)
    /// </summary>
    public class MaxBotCommandsPatchRequest
    {
        /// <summary>Команды бота (до 32)</summary>
        [JsonPropertyName("commands")]
        public Objects.MaxBotCommand[] Commands { get; set; } = System.Array.Empty<Objects.MaxBotCommand>();
    }
}
