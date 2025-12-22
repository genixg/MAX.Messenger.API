using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxMessageMarkup
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } // strong, italic, link, etc

        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }
    }
}
