namespace MAX.Messenger.API.Enums
{
    /// <summary>
    /// Тип загружаемого файла для POST /uploads?type=...
    /// </summary>
    public static class UploadType
    {
        public const string Image = "image";
        public const string Video = "video";
        public const string Audio = "audio";
        public const string File = "file";
    }
}
