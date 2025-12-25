using MAX.Messenger.API.Objects;
using MAX.Messenger.API.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAX.Messenger.API.Client
{
    public class MaxApiClient : IMaxApiClient
    {
        private readonly HttpClient _http;
        private readonly string token;
        private readonly JsonSerializerOptions _json;

        public MaxApiClient(string token, MaxApiOptions? options = null)
        {
            options ??= new MaxApiOptions();

            _http = new HttpClient
            {
                BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/"),
                Timeout = options.Timeout
            };

            this.token = token ?? throw new ArgumentNullException(nameof(token));

            _http.DefaultRequestHeaders.Add("Authorization", this.token);

            _json = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        public async Task SendMessageAsync(NewMessageBody body, long? chatId = null, long? userId = null, bool? disableLinkPreview = null)
        {
            if (chatId == null && userId == null)
                throw new ArgumentException("MAX: нужно указать chatId или userId для /messages");

            var qs = new List<string>();
            if (userId != null) qs.Add($"user_id={userId.Value}");
            if (chatId != null) qs.Add($"chat_id={chatId.Value}");
            if (disableLinkPreview != null) qs.Add($"disable_link_preview={disableLinkPreview.Value.ToString().ToLowerInvariant()}");

            var url = "messages" + (qs.Count > 0 ? "?" + string.Join("&", qs) : "");

            var json = JsonSerializer.Serialize(body, _json);
            var resp = await _http.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            await EnsureSuccess(resp);
        }

        public async Task AnswerCallbackAsync(string callbackId, NewMessageBody? body = null, string? notification = null)
        {
            if (string.IsNullOrWhiteSpace(callbackId))
                throw new ArgumentException("callbackId is empty");

            var req = new AnswerCallbackRequest
            {
                CallbackId = callbackId,
                Notification = notification,
                Message = body
            };
            var json = JsonSerializer.Serialize(req, _json);

            var resp = await _http.PostAsync(
                "answers?callback_id=" + callbackId,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!resp.IsSuccessStatusCode)
            {
                var respBody = await resp.Content.ReadAsStringAsync();
                throw new Exception($"MAX /answers error {(int)resp.StatusCode}: {respBody}");
            }

            await EnsureSuccess(resp);
        }

        public async Task<FileUploadResult> UploadAsync(byte[] data, string fileName)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(data), "file", fileName);

            var resp = await _http.PostAsync("upload", content);
            await EnsureSuccess(resp);

            var json = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<FileUploadResult>(json, _json)!;
        }

        private static async Task EnsureSuccess(HttpResponseMessage resp)
        {
            if (resp.IsSuccessStatusCode)
                return;

            var body = await resp.Content.ReadAsStringAsync();
            throw new Exception($"MAX API error {(int)resp.StatusCode}: {body}");
        }

        // --- ваши доп. методы оставляю как есть (на всякий), но чиню критичное ---

        #region Subscriptions
        public async Task<List<MaxSubscription>> GetSubscriptionsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "subscriptions");
            var response = await SendAsync<MaxSubscriptionsResponse>(request);
            return response.Subscriptions?.ToList() ?? new List<MaxSubscription>();
        }

        public async Task DeleteSubscriptionAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"subscriptions?url={Uri.EscapeDataString(url)}");
            await SendAsync<object>(request);
        }

        public async Task<MaxSubscription> CreateSubscriptionAsync(string webhookUrl, IEnumerable<string> updateTypes)
        {
            var body = new MaxSubscriptionRequestData
            {
                Url = webhookUrl,
                UpdateTypes = updateTypes.ToList()
            };

            var json = JsonSerializer.Serialize(body, _json);

            var request = new HttpRequestMessage(HttpMethod.Post, "subscriptions")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            return await SendAsync<MaxSubscription>(request);
        }
        #endregion

        private async Task<T> SendAsync<T>(HttpRequestMessage request)
        {
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("Access token MAX не задан");

            var response = await _http.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"MAX API error {(int)response.StatusCode}: {content}");

            if (typeof(T) == typeof(string))
                return (T)(object)content;

            return JsonSerializer.Deserialize<T>(content, _json)!;
        }
    }

    public class UploadResponse
    {
        [JsonPropertyName("file_id")]
        public string FileId { get; set; } = null!;
    }
}

