namespace MAX.Messenger.API.Enums
{
    /// <summary>
    /// Права администратора чата/канала (ChatMember.permissions, ChatAdmin.permissions).
    /// </summary>
    public static class ChatAdminPermission
    {
        public const string ReadAllMessages = "read_all_messages";
        public const string AddRemoveMembers = "add_remove_members";
        public const string AddAdmins = "add_admins";
        public const string ChangeChatInfo = "change_chat_info";
        public const string PinMessage = "pin_message";
        public const string EditLink = "edit_link";
        public const string Write = "write";
        public const string Edit = "edit";
        public const string Delete = "delete";
        public const string CanCall = "can_call";
        public const string ViewStats = "view_stats";
    }
}
