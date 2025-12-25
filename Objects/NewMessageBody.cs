using MAX.Messenger.API.Objects;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Requests
{
    public class NewMessageBody
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("attachments")]
        public IEnumerable<MaxMessageAttachment> Attachments { get; set; }

        [JsonPropertyName("link")]
        public MaxNewMessageLink? Link { get; set; } = null;

        [JsonPropertyName("notify")]
        public bool Notify { get; set; } = true;

        [JsonPropertyName("format")]
        public string? TextFormat { get; set; } // "markdown", "html"
    }
}
