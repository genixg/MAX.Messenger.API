using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxMessageStat
    {
        [JsonPropertyName("views")]
        public int Views { get; set; }
    }
}
