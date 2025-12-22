using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Объект, описывающий участника чата
    /// </summary>
    public class MaxChatMember : MaxUserWithPhoto
    {
        /// <summary>
        /// Время последней активности пользователя в чате. Может быть устаревшим для суперчатов (равно времени вступления)
        /// </summary>
        [JsonPropertyName("last_access_time")]
        public long LastAccessTime { get; set; }

        /// <summary>
        /// Является ли пользователь владельцем чата
        /// </summary>
        [JsonPropertyName("is_owner")]
        public bool IsOwner { get; set; }

        /// <summary>
        /// Является ли пользователь администратором чата
        /// </summary>
        [JsonPropertyName("is_admin")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Дата присоединения к чату в формате Unix time
        /// </summary>
        [JsonPropertyName("join_time")]
        public long JoinTime { get; set; }

        /// <summary>
        /// Перечень прав пользователя. Возможные значения: "read_all_messages" "add_remove_members" "add_admins" "change_chat_info" "pin_message" "write" "edit_link"
        /// </summary>
        [JsonPropertyName("permissions")]
        public IEnumerable<string> Permissions { get; set; }

        /// <summary>
        /// Заголовок, который будет показан на клиенте
        /// Если пользователь администратор или владелец и ему не установлено это название, то поле не передаётся, клиенты на своей стороне подменят на "владелец" или "админ"
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; } = null;
    }
}
