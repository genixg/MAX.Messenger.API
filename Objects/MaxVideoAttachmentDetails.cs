using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Детальная информация о видео-вложении (ответ GET /videos/{videoToken}):
    /// прямые URL для воспроизведения/скачивания и метаданные
    /// </summary>
    public class MaxVideoAttachmentDetails
    {
        /// <summary>Токен видео-вложения</summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = null!;

        /// <summary>URL для скачивания/воспроизведения. Может быть null, если видео недоступно</summary>
        [JsonPropertyName("urls")]
        public MaxVideoUrls? Urls { get; set; }

        /// <summary>Миниатюра видео</summary>
        [JsonPropertyName("thumbnail")]
        public MaxPhotoAttachmentPayload? Thumbnail { get; set; }

        /// <summary>Ширина видео</summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }

        /// <summary>Высота видео</summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>Длительность в секундах</summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }
    }

    /// <summary>
    /// Доступные URL видео по разрешениям
    /// </summary>
    public class MaxVideoUrls
    {
        [JsonPropertyName("mp4_1080")]
        public string? Mp4_1080 { get; set; }

        [JsonPropertyName("mp4_720")]
        public string? Mp4_720 { get; set; }

        [JsonPropertyName("mp4_480")]
        public string? Mp4_480 { get; set; }

        [JsonPropertyName("mp4_360")]
        public string? Mp4_360 { get; set; }

        [JsonPropertyName("mp4_240")]
        public string? Mp4_240 { get; set; }

        [JsonPropertyName("mp4_144")]
        public string? Mp4_144 { get; set; }

        /// <summary>URL трансляции (HLS), если доступен</summary>
        [JsonPropertyName("hls")]
        public string? Hls { get; set; }
    }
}
