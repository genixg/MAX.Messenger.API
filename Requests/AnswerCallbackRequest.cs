using MAX.Messenger.API.Objects;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Requests
{
    /// <summary>
    /// Ответ бота на нажатие кнопки (CallbackAnswer).
    /// callback_id передаётся только query-параметром метода POST /answers
    /// </summary>
    public class AnswerCallbackRequest
    {
        /// <summary>
        /// Устарело: по актуальной спецификации callback_id передаётся только как query-параметр.
        /// Свойство сохранено для обратной совместимости, но не сериализуется в тело запроса
        /// </summary>
        [JsonIgnore]
        public string CallbackId { get; set; } = null!;

        /// <summary>Одноразовое уведомление пользователя</summary>
        [JsonPropertyName("notification")]
        public string? Notification { get; set; }

        /// <summary>Обновление сообщения с кнопками</summary>
        [JsonPropertyName("message")]
        public NewMessageBody? Message { get; set; }
    }
}
