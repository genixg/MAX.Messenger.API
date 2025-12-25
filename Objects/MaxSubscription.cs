using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxSubscription
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("update_types")]
        public IEnumerable<string> UpdateTypes { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
    }

    public class MaxSubscriptionsResponse
    {
        public IEnumerable<MaxSubscription> Subscriptions { get; set; }
    }

    public class MaxCreateSubscriptionRequest
    {
        [JsonPropertyName("request")]
        public MaxSubscriptionRequestData Request { get; set; }
    }

    public class MaxSubscriptionRequestData
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("update_types")]
        public IEnumerable<string> UpdateTypes { get; set; }
    }
}
