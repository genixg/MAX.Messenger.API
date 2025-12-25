using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAX.Messenger.API.Requests
{
    public class AnswerCallbackRequest
    {
        [JsonPropertyName("callback_id")]
        public string CallbackId { get; set; } = null!;

        [JsonPropertyName("notification")]
        public string? Notification { get; set; }

        [JsonPropertyName("message")]
        public NewMessageBody? Message { get; set; }
    }
}
