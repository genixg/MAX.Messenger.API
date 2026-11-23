namespace MAX.Messenger.API.Enums
{
    /// <summary>
    /// Действия бота, отправляемые в чат через POST /chats/{chatId}/actions.
    /// </summary>
    public static class SenderAction
    {
        public const string TypingOn = "typing_on";
        public const string SendingPhoto = "sending_photo";
        public const string SendingVideo = "sending_video";
        public const string SendingAudio = "sending_audio";
        public const string SendingFile = "sending_file";
        public const string MarkSeen = "mark_seen";
    }
}
