using MAX.Messenger.API.Objects;
using MAX.Messenger.API.Requests;
using System.Threading.Tasks;

namespace MAX.Messenger.API.Client
{
    public interface IMaxApiClient
    {
        Task SendMessageAsync(NewMessageBody body, long? chatId = null, long? userId = null, bool? disableLinkPreview = null);
        Task AnswerCallbackAsync(string callbackId, NewMessageBody? body = null, string? notification = null);
        Task<FileUploadResult> UploadAsync(byte[] data, string fileName);
    }
}
