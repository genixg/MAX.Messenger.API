using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Keyboards
{
    public class MaxButton
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = null!;

        [JsonPropertyName("action")]
        public MaxButtonAction Action { get; set; } = null!;

        [JsonPropertyName("style")]
        public string? Style { get; set; }
        // primary | positive | negative (необязательно)
    }
}