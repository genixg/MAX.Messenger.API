using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Keyboards
{
    public class MaxButtonAction
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;
        // callback | message | link

        [JsonPropertyName("payload")]
        public string? Payload { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}