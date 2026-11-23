namespace MAX.Messenger.API.Enums
{
    /// <summary>
    /// Типы элементов текстовой разметки (MaxMessageMarkup.type) из объекта MarkupElement.
    /// </summary>
    public static class MarkupType
    {
        /// <summary>**жирный**</summary>
        public const string Strong = "strong";

        /// <summary>*курсив*</summary>
        public const string Emphasized = "emphasized";

        /// <summary>`моноширинный` / ```блок кода```</summary>
        public const string Monospaced = "monospaced";

        /// <summary>Ссылка (есть поле url)</summary>
        public const string Link = "link";

        /// <summary>~зачёркнутый~</summary>
        public const string Strikethrough = "strikethrough";

        /// <summary>++подчёркнутый++</summary>
        public const string Underline = "underline";

        /// <summary>Упоминание пользователя (есть поля user_link / user_id)</summary>
        public const string UserMention = "user_mention";

        /// <summary>Заголовок</summary>
        public const string Heading = "heading";

        /// <summary>Выделенный текст</summary>
        public const string Highlighted = "highlighted";

        /// <summary>Цитата</summary>
        public const string Quote = "quote";
    }
}
