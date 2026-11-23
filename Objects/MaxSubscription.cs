using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Webhook-подписка (Subscription)
    /// </summary>
    public class MaxSubscription
    {
        /// <summary>Webhook URL</summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;

        /// <summary>Unix-time создания подписки</summary>
        [JsonPropertyName("time")]
        public long Time { get; set; }

        /// <summary>Типы получаемых событий. См. <see cref="Enums.UpdateType"/></summary>
        [JsonPropertyName("update_types")]
        public IEnumerable<string>? UpdateTypes { get; set; }
    }

    /// <summary>
    /// Ответ GET /subscriptions (GetSubscriptionsResult)
    /// </summary>
    public class MaxSubscriptionsResponse
    {
        [JsonPropertyName("subscriptions")]
        public IEnumerable<MaxSubscription> Subscriptions { get; set; } = new List<MaxSubscription>();
    }

    /// <summary>
    /// Тело запроса POST /subscriptions (SubscriptionRequestBody)
    /// </summary>
    public class MaxSubscriptionRequestData
    {
        /// <summary>HTTPS-URL бота (обязательно порт 443)</summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;

        /// <summary>
        /// Секрет, добавляемый в заголовок "X-Max-Bot-Api-Secret" каждого webhook-запроса.
        /// 5-256 символов: A-Z, a-z, 0-9, _ и -
        /// </summary>
        [JsonPropertyName("secret")]
        public string? Secret { get; set; } = null;

        /// <summary>Типы получаемых событий. См. <see cref="Enums.UpdateType"/></summary>
        [JsonPropertyName("update_types")]
        public IEnumerable<string>? UpdateTypes { get; set; }
    }
}
