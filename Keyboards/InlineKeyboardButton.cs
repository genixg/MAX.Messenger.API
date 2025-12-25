using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Keyboards
{
    /// <summary>
    /// Кнопка inline клавиатуры. Набор полей зависит от Type.
    /// По документации MAX: type + опциональные поля (text, payload, intent, url, quick, web_app, contact_id ...)
    /// </summary>
    public class InlineKeyboardButton
    {
        /// <summary>
        /// callback | link | open_app | request_geo_location | request_contact (и др. если появятся)
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;

        /// <summary>
        /// Текст на кнопке
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        /// <summary>
        /// Для callback, до 1024 символов
        /// </summary>
        [JsonPropertyName("payload")]
        public string? Payload { get; set; }

        /// <summary>
        /// Намерение кнопки. Влияет на отображение клиентом. Enum: "positive" "negative" "default"
        /// </summary>
        [JsonPropertyName("intent")]
        public string? Intent { get; set; }

        /// <summary>
        /// Для link
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// Для request_geo_location (быстрый запрос)
        /// </summary>
        [JsonPropertyName("quick")]
        public bool? Quick { get; set; }

        /// <summary>
        /// Для open_app (mini app)
        /// Публичное имя (username) бота или ссылка на него, чьё мини-приложение надо запустить
        /// </summary>
        [JsonPropertyName("web_app")]
        public string? WebApp { get; set; }

        /// <summary>
        /// Для request_contact и open_app Идентификатор бота, чьё мини-приложение надо запустить
        /// </summary>
        [JsonPropertyName("contact_id")]
        public long? ContactId { get; set; }
    }
}