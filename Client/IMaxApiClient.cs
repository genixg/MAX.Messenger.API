using MAX.Messenger.API.Objects;
using MAX.Messenger.API.Requests;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MAX.Messenger.API.Client
{
    /// <summary>
    /// Контракт клиента MAX Bot API
    /// </summary>
    public interface IMaxApiClient
    {
        // Bots
        Task<MaxBotInfo> GetMyInfoAsync(CancellationToken cancellationToken = default);
        Task<MaxBotCommandsInfo> SetMyCommandsAsync(IEnumerable<MaxBotCommand> commands, CancellationToken cancellationToken = default);

        // Chats
        Task<MaxChat> GetChatAsync(long chatId, CancellationToken cancellationToken = default);
        Task<MaxChat> EditChatAsync(long chatId, MaxChatPatch patch, CancellationToken cancellationToken = default);
        Task SendChatActionAsync(long chatId, string action, CancellationToken cancellationToken = default);
        Task<MaxMessage?> GetPinnedMessageAsync(long chatId, CancellationToken cancellationToken = default);
        Task PinMessageAsync(long chatId, string messageId, bool? notify = null, CancellationToken cancellationToken = default);
        Task UnpinMessageAsync(long chatId, CancellationToken cancellationToken = default);
        Task<MaxChatMember> GetBotMembershipAsync(long chatId, CancellationToken cancellationToken = default);
        Task LeaveChatAsync(long chatId, CancellationToken cancellationToken = default);
        Task<MaxChatMembersList> GetChatMembersAsync(long chatId, IEnumerable<long>? userIds = null, long? marker = null, int? count = null, CancellationToken cancellationToken = default);
        Task<MaxChatMembersList> GetChatAdminsAsync(long chatId, CancellationToken cancellationToken = default);
        Task SetChatAdminsAsync(long chatId, IEnumerable<MaxChatAdmin> admins, CancellationToken cancellationToken = default);
        Task RevokeChatAdminAsync(long chatId, long userId, CancellationToken cancellationToken = default);
        Task<MaxModifyMembersResult> AddChatMembersAsync(long chatId, IEnumerable<long> userIds, CancellationToken cancellationToken = default);
        Task RemoveChatMemberAsync(long chatId, long userId, bool block = false, CancellationToken cancellationToken = default);

        // Messages
        Task<MaxMessage> SendMessageAsync(NewMessageBody body, long? chatId = null, long? userId = null, bool? disableLinkPreview = null, CancellationToken cancellationToken = default);
        Task<MaxMessageList> GetMessagesAsync(long chatId, long? before = null, long? after = null, int? count = null, CancellationToken cancellationToken = default);
        Task<MaxMessageList> GetMessagesByIdsAsync(IEnumerable<string> messageIds, CancellationToken cancellationToken = default);
        Task<MaxMessage> GetMessageAsync(string messageId, CancellationToken cancellationToken = default);
        Task EditMessageAsync(string messageId, NewMessageBody body, CancellationToken cancellationToken = default);
        Task DeleteMessageAsync(string messageId, CancellationToken cancellationToken = default);
        Task<MaxVideoAttachmentDetails> GetVideoDetailsAsync(string videoToken, CancellationToken cancellationToken = default);
        Task AnswerCallbackAsync(string callbackId, NewMessageBody? body = null, string? notification = null, bool? disableLinkPreview = null, CancellationToken cancellationToken = default);

        // Comments
        Task<MaxCommentMessage> SendCommentAsync(string messageId, NewCommentBody body, bool? disableLinkPreview = null, CancellationToken cancellationToken = default);
        Task<MaxCommentMessageList> GetCommentsAsync(string messageId, IEnumerable<string>? commentIds = null, long? before = null, long? after = null, int? count = null, CancellationToken cancellationToken = default);
        Task<MaxCommentMessage> GetCommentAsync(string messageId, string commentId, CancellationToken cancellationToken = default);
        Task EditCommentAsync(string messageId, string commentId, NewCommentBody body, CancellationToken cancellationToken = default);
        Task DeleteCommentAsync(string messageId, string commentId, CancellationToken cancellationToken = default);

        // Subscriptions
        Task<List<MaxSubscription>> GetSubscriptionsAsync(CancellationToken cancellationToken = default);
        Task<MaxOperationResult> SubscribeAsync(string webhookUrl, IEnumerable<string>? updateTypes = null, string? secret = null, CancellationToken cancellationToken = default);
        Task<MaxOperationResult> CreateSubscriptionAsync(string webhookUrl, IEnumerable<string> updateTypes, string? secret = null, CancellationToken cancellationToken = default);
        Task<MaxOperationResult> UnsubscribeAsync(string url, CancellationToken cancellationToken = default);
        Task DeleteSubscriptionAsync(string url, CancellationToken cancellationToken = default);

        // Updates (Long Polling)
        Task<MaxUpdateList> GetUpdatesAsync(int? limit = null, int? timeout = null, long? marker = null, IEnumerable<string>? types = null, CancellationToken cancellationToken = default);

        // Upload
        Task<MaxUploadEndpoint> GetUploadEndpointAsync(string type, CancellationToken cancellationToken = default);
        Task<MaxUploadResult> UploadAsync(byte[] data, string fileName, string type = Enums.UploadType.File, CancellationToken cancellationToken = default);
    }
}
