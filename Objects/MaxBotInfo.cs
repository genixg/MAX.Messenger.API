using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Объект, описывающий информацию о боте
    /// </summary>
    public class MaxBotInfo : MaxUserWithPhoto
    {
        /// <summary>
        /// Команды, поддерживаемые ботом
        /// </summary>
        [JsonPropertyName("commands")]
        public IEnumerable<MaxBotCommand>? Commands { get; set; } = null;
    }
}
