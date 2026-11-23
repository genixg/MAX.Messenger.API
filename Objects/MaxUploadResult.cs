using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Ответ POST /uploads?type=... — URL, на который нужно выложить файл (UploadEndpoint)
    /// </summary>
    public class MaxUploadEndpoint
    {
        /// <summary>URL для загрузки файла</summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;

        /// <summary>Токен видео/аудио для отправки сообщения (если возвращается)</summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }

    /// <summary>
    /// JSON-ответ, полученный после фактической загрузки файла на сервер.
    /// Для video/audio/file содержит token; для image — словарь photos
    /// </summary>
    public class MaxUploadResult
    {
        /// <summary>Токен загруженного медиа (video, audio, file)</summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>Токены загруженных изображений (image)</summary>
        [JsonPropertyName("photos")]
        public Dictionary<string, MaxPhotoToken>? Photos { get; set; }

        /// <summary>URL загруженного файла (если возвращается)</summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>Первый токен: token или из photos</summary>
        [JsonIgnore]
        public string? FirstToken =>
            Token ?? (Photos is { Count: > 0 } photos ? photos.Values.First().Token : null);
    }
}
