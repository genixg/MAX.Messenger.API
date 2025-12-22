using MAX.Messenger.API.Objects;
using MAX.Messenger.API.Requests;
using System.Threading.Tasks;

namespace MAX.Messenger.API.Client
{
    public interface IMaxApiClient
    {
        Task SendMessageAsync(NewMessageBody body);
        Task AnswerCallbackAsync(string callbackId);
        Task<FileUploadResult> UploadAsync(byte[] data, string fileName);
    }
}
