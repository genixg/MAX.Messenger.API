using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Команда, поддерживаемая ботом
    /// </summary>
    public class MaxBotCommand
    {
        /// <summary>
        /// Название команды, от 1 до 64 символов
        /// </summary>
        [JsonPropertyName("name")]
        [MaxLength(64)]
        [MinLength(1)]
        public string Name { get; set; }

        /// <summary>
        /// Описание команды (по желанию)
        /// </summary>
        [JsonPropertyName("description")]
        [MaxLength(128)]
        [MinLength(1)]
        public string? Description { get; set; } = null;
    }
}
