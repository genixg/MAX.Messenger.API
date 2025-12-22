using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Keyboards
{
    public class MaxKeyboard
    {
        [JsonPropertyName("inline")]
        public bool Inline { get; set; } = true;

        [JsonPropertyName("buttons")]
        public List<List<MaxButton>> Buttons { get; set; }
    }
}
