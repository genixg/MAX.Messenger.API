using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    public class MaxUserWithPhoto : MaxUser
    {
        /// <summary>
        /// до 16000 символов. Описание пользователя.Может быть null, если пользователь его не заполнил
        /// </summary>
        [JsonPropertyName("description")]
        [MaxLength(16000)]
        public string Description { get; set; } = null;

        /// <summary>
        /// URL аватара
        /// </summary>
        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; } = null;

        /// <summary>
        /// URL аватара большего размера
        /// </summary>
        [JsonPropertyName("full_avatar_url")]
        public string FullAvatarUrl { get; set; } = null;
    }
}
