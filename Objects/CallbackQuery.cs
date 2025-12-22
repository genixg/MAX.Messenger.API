using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class CallbackQuery
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("from")]
        public MaxUser From { get; set; } = null!;

        [JsonPropertyName("message")]
        public MaxMessage Message { get; set; } = null!;

        [JsonPropertyName("payload")]
        public string Payload { get; set; } = null!;
    }
}