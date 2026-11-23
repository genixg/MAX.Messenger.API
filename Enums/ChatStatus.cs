namespace MAX.Messenger.API.Enums
{
    /// <summary>
    /// Статус чата для текущего бота (MaxChat.status).
    /// </summary>
    public static class ChatStatus
    {
        /// <summary>Бот активно состоит в чате</summary>
        public const string Active = "active";

        /// <summary>Бот был удалён из чата</summary>
        public const string Removed = "removed";

        /// <summary>Бот вышел из чата самостоятельно</summary>
        public const string Left = "left";

        /// <summary>Чат закрыт</summary>
        public const string Closed = "closed";

        /// <summary>Бот остановлен пользователем (только для диалогов)</summary>
        public const string Suspended = "suspended";
    }
}
