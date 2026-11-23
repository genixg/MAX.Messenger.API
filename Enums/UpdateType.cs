namespace MAX.Messenger.API.Enums
{
    /// <summary>
    /// Типы событий Update (см. объект Update в спецификации MAX API).
    /// Для Webhook подписки выбирайте нужные типы через POST /subscriptions.
    /// </summary>
    public static class UpdateType
    {
        /// <summary>Новое сообщение (в групповых чатах — только если бот админ с правом read_all_messages)</summary>
        public const string MessageCreated = "message_created";

        /// <summary>Пользователь нажал кнопку inline-клавиатуры</summary>
        public const string MessageCallback = "message_callback";

        /// <summary>Сообщение отредактировано</summary>
        public const string MessageEdited = "message_edited";

        /// <summary>Сообщение удалено</summary>
        public const string MessageRemoved = "message_removed";

        /// <summary>Комментарий к посту создан (нужны права администратора канала)</summary>
        public const string CommentCreated = "comment_created";

        /// <summary>Комментарий к посту отредактирован</summary>
        public const string CommentEdited = "comment_edited";

        /// <summary>Комментарий к посту удалён</summary>
        public const string CommentRemoved = "comment_removed";

        /// <summary>Бот добавлен в чат/канал. С ноября 2026 поле user в этом событии не возвращается</summary>
        public const string BotAdded = "bot_added";

        /// <summary>Бот удалён из чата/канала</summary>
        public const string BotRemoved = "bot_removed";

        /// <summary>Пользователь добавлен в чат, где бот — администратор</summary>
        public const string UserAdded = "user_added";

        /// <summary>Пользователь удалён из чата, где бот — администратор</summary>
        public const string UserRemoved = "user_removed";

        /// <summary>Пользователь нажал кнопку "Начать" в диалоге с ботом</summary>
        public const string BotStarted = "bot_started";

        /// <summary>Пользователь остановил бота</summary>
        public const string BotStopped = "bot_stopped";

        /// <summary>История диалога очищена</summary>
        public const string DialogCleared = "dialog_cleared";

        /// <summary>Диалог удалён</summary>
        public const string DialogRemoved = "dialog_removed";

        /// <summary>Диалог заглушён (muted)</summary>
        public const string DialogMuted = "dialog_muted";

        /// <summary>Заглушение диалога снято</summary>
        public const string DialogUnmuted = "dialog_unmuted";

        /// <summary>Изменено название чата</summary>
        public const string ChatTitleChanged = "chat_title_changed";

        /// <summary>Изменены права бота-администратора в канале или групповом чате (только Webhook)</summary>
        public const string BotAdminPermissionsChanged = "bot_admin_permissions_changed";
    }
}
