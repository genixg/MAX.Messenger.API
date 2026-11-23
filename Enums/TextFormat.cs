namespace MAX.Messenger.API.Enums
{
    public static class TextFormat
    {
        public const string Markdown = "markdown";
        public const string Html = "html";
        /// <summary>
        /// Устарело: в актуальной спецификации значения "plain" нет — отсутствие format означает простой текст.
        /// </summary>
        public const string Plain = "plain";
    }
}