using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Простой результат запроса (SimpleQueryResult).
    /// Некоторые методы при ошибке возвращают HTTP 200 с success=false, code и message
    /// </summary>
    public class MaxOperationResult
    {
        [JsonPropertyName("success")]
        public bool? Success { get; set; }

        /// <summary>Пояснение, если запрос неуспешен</summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>Код ошибки в формате "some.error" (например, not.found, chat.denied, proto.payload)</summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }
    }

    /// <summary>
    /// Тело ошибки, возвращаемое с HTTP-кодом 4xx (Error)
    /// </summary>
    public class MaxApiError
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        /// <summary>Код ошибки в формате "some.error"</summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        /// <summary>Человекочитаемое описание</summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }

    /// <summary>
    /// Результат добавления/удаления участников чата (ModifyMembersResult)
    /// </summary>
    public class MaxModifyMembersResult : MaxOperationResult
    {
        /// <summary>ID пользователей, для которых операция не удалась</summary>
        [JsonPropertyName("failed_user_ids")]
        public IEnumerable<long>? FailedUserIds { get; set; }

        /// <summary>Детали причин отказа</summary>
        [JsonPropertyName("failed_user_details")]
        public IEnumerable<MaxFailedUserDetails>? FailedUserDetails { get; set; }
    }

    /// <summary>
    /// Причина, по которой пользователя нельзя добавить/удалить из чата
    /// </summary>
    public class MaxFailedUserDetails
    {
        /// <summary>
        /// Код ошибки: "add.participant.privacy" — конфликт с настройками приватности,
        /// "add.participant.not.found" — пользователи не найдены
        /// </summary>
        [JsonPropertyName("error_code")]
        public string ErrorCode { get; set; } = null!;

        [JsonPropertyName("user_ids")]
        public IEnumerable<long> UserIds { get; set; } = null!;
    }

    /// <summary>
    /// Ответ POST /messages — созданное сообщение (SendMessageResult)
    /// </summary>
    public class MaxSendMessageResult
    {
        [JsonPropertyName("message")]
        public MaxMessage? Message { get; set; }
    }

    /// <summary>
    /// Ответ POST /messages/{messageId}/comments — созданный комментарий (SendCommentResult)
    /// </summary>
    public class MaxSendCommentResult
    {
        [JsonPropertyName("message")]
        public MaxCommentMessage? Message { get; set; }
    }

    /// <summary>
    /// Ответ GET /chats/{chatId}/pin (GetPinnedMessageResult)
    /// </summary>
    public class MaxPinnedMessageResult
    {
        /// <summary>Закреплённое сообщение. null, если ничего не закреплено</summary>
        [JsonPropertyName("message")]
        public MaxMessage? Message { get; set; }
    }

    /// <summary>
    /// Список сообщений с пагинацией (MessageList)
    /// </summary>
    public class MaxMessageList
    {
        [JsonPropertyName("messages")]
        public IEnumerable<MaxMessage> Messages { get; set; } = new List<MaxMessage>();
    }

    /// <summary>
    /// Список комментариев с пагинацией (CommentMessageList)
    /// </summary>
    public class MaxCommentMessageList
    {
        [JsonPropertyName("messages")]
        public IEnumerable<MaxCommentMessage> Messages { get; set; } = new List<MaxCommentMessage>();
    }

    /// <summary>
    /// Список участников/администраторов чата с указателем следующей страницы (ChatMembersList)
    /// </summary>
    public class MaxChatMembersList
    {
        [JsonPropertyName("members")]
        public IEnumerable<MaxChatMember> Members { get; set; } = new List<MaxChatMember>();

        /// <summary>Указатель на следующую страницу данных</summary>
        [JsonPropertyName("marker")]
        public long? Marker { get; set; }
    }

    /// <summary>
    /// Страница обновлений для Long Polling (UpdateList).
    /// marker указывает на следующее событие; все события до marker считаются обработанными
    /// </summary>
    public class MaxUpdateList
    {
        [JsonPropertyName("updates")]
        public IEnumerable<MaxUpdate> Updates { get; set; } = new List<MaxUpdate>();

        /// <summary>Указатель на следующее непрочитанное событие</summary>
        [JsonPropertyName("marker")]
        public long? Marker { get; set; }
    }

    /// <summary>
    /// Ответ PATCH /me/commands — изменённый список команд бота (BotCommandsInfo)
    /// </summary>
    public class MaxBotCommandsInfo
    {
        [JsonPropertyName("commands")]
        public IEnumerable<MaxBotCommand>? Commands { get; set; }
    }
}
