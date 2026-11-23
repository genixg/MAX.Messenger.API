namespace MAX.Messenger.API.Enums
{
    public static class AttachmentType
    {
        public const string InlineKeyboard = "inline_keyboard";
        /// <summary>Ранее назывался "photo" — тип photo больше не поддерживается, используйте image</summary>
        public const string Image = "image";
        public const string Video = "video";
        public const string Audio = "audio";
        public const string File = "file";
        public const string Sticker = "sticker";
        public const string Location = "location";
        /// <summary>Контакт (карточка пользователя)</summary>
        public const string Contact = "contact";
        /// <summary>Превью ссылки (link preview)</summary>
        public const string Share = "share";
    }
}
