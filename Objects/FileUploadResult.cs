using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class FileUploadResult
    {
        [JsonPropertyName("file_id")]
        public string? FileId { get; set; } = null!;
    }
}
