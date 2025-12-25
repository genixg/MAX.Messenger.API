using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Keyboards
{
    public class InlineKeyboardAttachmentRequestPayload
    {
        /// <summary>
        /// Двумерный массив кнопок (ряды -> кнопки в ряду)
        /// </summary>
        [JsonPropertyName("buttons")]
        public List<List<InlineKeyboardButton>> Buttons { get; set; } = new();
    }
}