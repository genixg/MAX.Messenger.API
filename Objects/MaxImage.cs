using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxImage
    {
        /// <summary>
        /// URL изображения
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
