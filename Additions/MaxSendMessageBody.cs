using MAX.Messenger.API.Keyboards;
using MAX.Messenger.API.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAX.Messenger.API.Additions
{
    /// <summary>
    /// Расширение NewMessageBody для отправки в конкретный чат + клавиатура.
    /// Не меняем MAX.Messenger.API.Requests/NewMessageBody, чтобы не ломать публичный контракт.
    /// </summary>
    public class MaxSendMessageBody : NewMessageBody
    {
        [JsonPropertyName("chat_id")]
        public long ChatId { get; set; }

        [JsonPropertyName("keyboard")]
        public MaxKeyboard? Keyboard { get; set; }
    }
}
