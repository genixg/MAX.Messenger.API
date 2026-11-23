using MAX.Messenger.API.Enums;
using MAX.Messenger.API.Objects;
using MAX.Messenger.API.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MAX.Messenger.API.Client
{
    /// <summary>
    /// Клиент MAX Bot API (https://platform-api2.max.ru).
    /// Авторизация — заголовок Authorization: &lt;access_token&gt; (query-параметр access_token больше не поддерживается)
    /// </summary>
    public class MaxApiClient : IMaxApiClient, IDisposable
    {
        private readonly HttpClient _http;
        private readonly bool _ownsHttpClient;
        private readonly string token;
        private readonly MaxApiOptions _options;
        private readonly JsonSerializerOptions _json;

        // Отдельный клиент для загрузки на CDN: не должен отправлять Authorization на чужие домены
        private static readonly HttpClient UploadHttp = new HttpClient();

        public MaxApiClient(string token, MaxApiOptions? options = null)
        {
            this.token = token ?? throw new ArgumentNullException(nameof(token));
            _options = options ?? new MaxApiOptions();

            _http = new HttpClient
            {
                BaseAddress = new Uri(_options.BaseUrl.TrimEnd('/') + "/"),
                Timeout = Timeout.InfiniteTimeSpan // таймаут ставится на каждый запрос отдельно
            };
            _ownsHttpClient = true;

            _http.DefaultRequestHeaders.Add("Authorization", this.token);

            _json = CreateJsonOptions();
        }

        /// <summary>
        /// Конструктор с внешним HttpClient (например, из IHttpClientFactory).
        /// BaseAddress у клиента должен указывать на https://platform-api2.max.ru
        /// </summary>
        public MaxApiClient(string token, HttpClient httpClient, MaxApiOptions? options = null)
        {
            this.token = token ?? throw new ArgumentNullException(nameof(token));
            _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options ?? new MaxApiOptions();
            _ownsHttpClient = false;

            if (!_http.DefaultRequestHeaders.Contains("Authorization"))
                _http.DefaultRequestHeaders.Add("Authorization", this.token);

            _json = CreateJsonOptions();
        }

        private static JsonSerializerOptions CreateJsonOptions() => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        #region Bots

        /// <summary>
        /// GET /me — информация о боте (имя, username, аватар, команды)
        /// </summary>
        public async Task<MaxBotInfo> GetMyInfoAsync(CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Get, "me", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxBotInfo>(content);
        }

        /// <summary>
        /// PATCH /me/commands — добавление, изменение и удаление команд бота (до 32 команд)
        /// </summary>
        public async Task<MaxBotCommandsInfo> SetMyCommandsAsync(IEnumerable<MaxBotCommand> commands, CancellationToken cancellationToken = default)
        {
            var body = new MaxBotCommandsPatchRequest { Commands = commands.ToArray() };
            var content = await RequestAsync(HttpMethod.Patch, "me/commands", body, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxBotCommandsInfo>(content);
        }

        #endregion

        #region Chats

        /// <summary>
        /// GET /chats/{chatId} — информация о чате или канале
        /// </summary>
        public async Task<MaxChat> GetChatAsync(long chatId, CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Get, $"chats/{chatId}", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxChat>(content);
        }

        /// <summary>
        /// PATCH /chats/{chatId} — изменение названия, описания, иконки, закрепления
        /// </summary>
        public async Task<MaxChat> EditChatAsync(long chatId, MaxChatPatch patch, CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Patch, $"chats/{chatId}", patch, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxChat>(content);
        }

        /// <summary>
        /// POST /chats/{chatId}/actions — действие бота в чате ("печатает…", см. Enums.SenderAction)
        /// </summary>
        public async Task SendChatActionAsync(long chatId, string action, CancellationToken cancellationToken = default)
        {
            await RequestAsync(HttpMethod.Post, $"chats/{chatId}/actions",
                new MaxActionRequestBody { Action = action }, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// GET /chats/{chatId}/pin — закреплённое сообщение (null, если ничего не закреплено)
        /// </summary>
        public async Task<MaxMessage?> GetPinnedMessageAsync(long chatId, CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Get, $"chats/{chatId}/pin", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxPinnedMessageResult>(content).Message;
        }

        /// <summary>
        /// PUT /chats/{chatId}/pin — закрепить сообщение в чате/канале
        /// </summary>
        public async Task PinMessageAsync(long chatId, string messageId, bool? notify = null, CancellationToken cancellationToken = default)
        {
            await RequestAsync(HttpMethod.Put, $"chats/{chatId}/pin",
                new MaxPinMessageBody { MessageId = messageId, Notify = notify }, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// DELETE /chats/{chatId}/pin — открепить сообщение
        /// </summary>
        public async Task UnpinMessageAsync(long chatId, CancellationToken cancellationToken = default)
        {
            await RequestAsync(HttpMethod.Delete, $"chats/{chatId}/pin", null, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// GET /chats/{chatId}/members/me — информация о членстве бота в чате
        /// </summary>
        public async Task<MaxChatMember> GetBotMembershipAsync(long chatId, CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Get, $"chats/{chatId}/members/me", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxChatMember>(content);
        }

        /// <summary>
        /// DELETE /chats/{chatId}/members/me — удалить бота из чата (бот выходит)
        /// </summary>
        public async Task LeaveChatAsync(long chatId, CancellationToken cancellationToken = default)
        {
            await RequestAsync(HttpMethod.Delete, $"chats/{chatId}/members/me", null, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// GET /chats/{chatId}/members — участники чата (постранично, count 1-100, default 20).
        /// Если переданы userIds — marker и count игнорируются
        /// </summary>
        public async Task<MaxChatMembersList> GetChatMembersAsync(long chatId, IEnumerable<long>? userIds = null, long? marker = null, int? count = null, CancellationToken cancellationToken = default)
        {
            var qs = new List<string>();
            if (userIds?.Any() == true)
                qs.Add("user_ids=" + string.Join(",", userIds));
            else
            {
                if (marker != null) qs.Add($"marker={marker.Value}");
                if (count != null) qs.Add($"count={count.Value}");
            }

            var content = await RequestAsync(HttpMethod.Get, $"chats/{chatId}/members{Query(qs)}", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxChatMembersList>(content);
        }

        /// <summary>
        /// POST /chats/{chatId}/members — добавить участников в чат (не более 100 за запрос).
        /// С 09.09.2026 метод ограничен. Бросает MaxApiException при success=false,
        /// возвращает детали неуспешных ID в результате
        /// </summary>
        public async Task<MaxModifyMembersResult> AddChatMembersAsync(long chatId, IEnumerable<long> userIds, CancellationToken cancellationToken = default)
        {
            var body = new MaxUserIdsList { UserIds = userIds.ToArray() };
            var content = await RequestAsync(HttpMethod.Post, $"chats/{chatId}/members", body, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxModifyMembersResult>(content);
        }

        /// <summary>
        /// DELETE /chats/{chatId}/members — удалить участника из чата; block=true — заблокировать (только для чатов со ссылкой)
        /// </summary>
        public async Task RemoveChatMemberAsync(long chatId, long userId, bool block = false, CancellationToken cancellationToken = default)
        {
            await RequestAsync(HttpMethod.Delete, $"chats/{chatId}/members?user_id={userId}&block={Bool(block)}", null, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// GET /chats/{chatId}/members/admins — администраторы чата (бот должен быть администратором)
        /// </summary>
        public async Task<MaxChatMembersList> GetChatAdminsAsync(long chatId, CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Get, $"chats/{chatId}/members/admins", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxChatMembersList>(content);
        }

        /// <summary>
        /// POST /chats/{chatId}/members/admins — назначить администраторов (см. Enums.ChatAdminPermission)
        /// </summary>
        public async Task SetChatAdminsAsync(long chatId, IEnumerable<MaxChatAdmin> admins, CancellationToken cancellationToken = default)
        {
            var body = new MaxChatAdminsList { Admins = admins.ToArray() };
            await RequestAsync(HttpMethod.Post, $"chats/{chatId}/members/admins", body, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// DELETE /chats/{chatId}/members/admins/{userId} — снять права администратора
        /// </summary>
        public async Task RevokeChatAdminAsync(long chatId, long userId, CancellationToken cancellationToken = default)
        {
            await RequestAsync(HttpMethod.Delete, $"chats/{chatId}/members/admins/{userId}", null, null, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region Messages

        /// <summary>
        /// POST /messages — отправить сообщение в чат, канал или диалог.
        /// Возвращает созданное сообщение (в нём есть body.mid для последующего редактирования)
        /// </summary>
        public async Task<MaxMessage> SendMessageAsync(NewMessageBody body, long? chatId = null, long? userId = null, bool? disableLinkPreview = null, CancellationToken cancellationToken = default)
        {
            if (chatId == null && userId == null)
                throw new ArgumentException("MAX: нужно указать chatId или userId для /messages");

            var qs = new List<string>();
            if (userId != null) qs.Add($"user_id={userId.Value}");
            if (chatId != null) qs.Add($"chat_id={chatId.Value}");
            if (disableLinkPreview != null) qs.Add($"disable_link_preview={Bool(disableLinkPreview.Value)}");

            var content = await RequestAsync(HttpMethod.Post, $"messages{Query(qs)}", body, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxSendMessageResult>(content).Message!;
        }

        /// <summary>
        /// GET /messages?chat_id= — история сообщений чата (последние первыми).
        /// before/after — границы по timestamp, count 1-100 (default 50)
        /// </summary>
        public async Task<MaxMessageList> GetMessagesAsync(long chatId, long? before = null, long? after = null, int? count = null, CancellationToken cancellationToken = default)
        {
            var qs = new List<string> { $"chat_id={chatId}" };
            if (before != null) qs.Add($"before={before.Value}");
            if (after != null) qs.Add($"after={after.Value}");
            if (count != null) qs.Add($"count={count.Value}");

            var content = await RequestAsync(HttpMethod.Get, $"messages{Query(qs)}", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxMessageList>(content);
        }

        /// <summary>
        /// GET /messages?message_ids= — сообщения по списку ID
        /// </summary>
        public async Task<MaxMessageList> GetMessagesByIdsAsync(IEnumerable<string> messageIds, CancellationToken cancellationToken = default)
        {
            var ids = messageIds.Select(Uri.EscapeDataString).ToArray();
            var content = await RequestAsync(HttpMethod.Get, $"messages?message_ids={string.Join(",", ids)}", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxMessageList>(content);
        }

        /// <summary>
        /// GET /messages/{messageId} — получить сообщение по ID (mid)
        /// </summary>
        public async Task<MaxMessage> GetMessageAsync(string messageId, CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Get, $"messages/{Uri.EscapeDataString(messageId)}", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxMessage>(content);
        }

        /// <summary>
        /// PUT /messages?message_id= — редактировать сообщение.
        /// attachments=null — вложения не меняются; пустой список — все вложения удаляются
        /// </summary>
        public async Task EditMessageAsync(string messageId, NewMessageBody body, CancellationToken cancellationToken = default)
        {
            await RequestAsync(HttpMethod.Put, $"messages?message_id={Uri.EscapeDataString(messageId)}", body, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// DELETE /messages?message_id= — удалить сообщение
        /// </summary>
        public async Task DeleteMessageAsync(string messageId, CancellationToken cancellationToken = default)
        {
            await RequestAsync(HttpMethod.Delete, $"messages?message_id={Uri.EscapeDataString(messageId)}", null, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// GET /videos/{videoToken} — детальная информация о видео-вложении: прямые URL по разрешениям
        /// </summary>
        public async Task<MaxVideoAttachmentDetails> GetVideoDetailsAsync(string videoToken, CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Get, $"videos/{Uri.EscapeDataString(videoToken)}", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxVideoAttachmentDetails>(content);
        }

        /// <summary>
        /// POST /answers?callback_id= — ответ на нажатие кнопки: уведомление и/или обновление сообщения
        /// </summary>
        public async Task AnswerCallbackAsync(string callbackId, NewMessageBody? body = null, string? notification = null, bool? disableLinkPreview = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(callbackId))
                throw new ArgumentException("callbackId is empty");

            var req = new AnswerCallbackRequest
            {
                CallbackId = callbackId,
                Notification = notification,
                Message = body
            };

            var qs = new List<string> { $"callback_id={Uri.EscapeDataString(callbackId)}" };
            if (disableLinkPreview != null) qs.Add($"disable_link_preview={Bool(disableLinkPreview.Value)}");

            await RequestAsync(HttpMethod.Post, $"answers{Query(qs)}", req, null, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region Comments

        /// <summary>
        /// POST /messages/{messageId}/comments — отправить комментарий к посту в канале (вложения запрещены)
        /// </summary>
        public async Task<MaxCommentMessage> SendCommentAsync(string messageId, NewCommentBody body, bool? disableLinkPreview = null, CancellationToken cancellationToken = default)
        {
            var qs = new List<string>();
            if (disableLinkPreview != null) qs.Add($"disable_link_preview={Bool(disableLinkPreview.Value)}");

            var content = await RequestAsync(HttpMethod.Post,
                $"messages/{Uri.EscapeDataString(messageId)}/comments{Query(qs)}", body, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxSendCommentResult>(content).Message!;
        }

        /// <summary>
        /// GET /messages/{messageId}/comments — комментарии к посту (последние первыми)
        /// </summary>
        public async Task<MaxCommentMessageList> GetCommentsAsync(string messageId, IEnumerable<string>? commentIds = null, long? before = null, long? after = null, int? count = null, CancellationToken cancellationToken = default)
        {
            var qs = new List<string>();
            if (commentIds?.Any() == true)
                qs.Add("comment_ids=" + string.Join(",", commentIds.Select(Uri.EscapeDataString)));
            else
            {
                if (before != null) qs.Add($"before={before.Value}");
                if (after != null) qs.Add($"after={after.Value}");
                if (count != null) qs.Add($"count={count.Value}");
            }

            var content = await RequestAsync(HttpMethod.Get,
                $"messages/{Uri.EscapeDataString(messageId)}/comments{Query(qs)}", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxCommentMessageList>(content);
        }

        /// <summary>
        /// GET /messages/{messageId}/comments/{commentId} — комментарий по ID
        /// </summary>
        public async Task<MaxCommentMessage> GetCommentAsync(string messageId, string commentId, CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Get,
                $"messages/{Uri.EscapeDataString(messageId)}/comments/{Uri.EscapeDataString(commentId)}", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxCommentMessage>(content);
        }

        /// <summary>
        /// PUT /messages/{messageId}/comments?comment_id= — отредактировать комментарий
        /// </summary>
        public async Task EditCommentAsync(string messageId, string commentId, NewCommentBody body, CancellationToken cancellationToken = default)
        {
            await RequestAsync(HttpMethod.Put,
                $"messages/{Uri.EscapeDataString(messageId)}/comments?comment_id={Uri.EscapeDataString(commentId)}",
                body, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// DELETE /messages/{messageId}/comments?comment_id= — удалить комментарий
        /// </summary>
        public async Task DeleteCommentAsync(string messageId, string commentId, CancellationToken cancellationToken = default)
        {
            await RequestAsync(HttpMethod.Delete,
                $"messages/{Uri.EscapeDataString(messageId)}/comments?comment_id={Uri.EscapeDataString(commentId)}",
                null, null, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region Subscriptions

        /// <summary>
        /// GET /subscriptions — список Webhook-подписок бота
        /// </summary>
        public async Task<List<MaxSubscription>> GetSubscriptionsAsync(CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Get, "subscriptions", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxSubscriptionsResponse>(content).Subscriptions.ToList();
        }

        /// <summary>
        /// POST /subscriptions — подписка на события через Webhook (сервер должен слушать порт 443).
        /// updateTypes — см. Enums.UpdateType; пустой/null — все события.
        /// secret проверяется в заголовке X-Max-Bot-Api-Secret входящих запросов
        /// </summary>
        public async Task<MaxOperationResult> SubscribeAsync(string webhookUrl, IEnumerable<string>? updateTypes = null, string? secret = null, CancellationToken cancellationToken = default)
        {
            var body = new MaxSubscriptionRequestData
            {
                Url = webhookUrl,
                Secret = secret,
                UpdateTypes = updateTypes?.ToList()
            };

            var content = await RequestAsync(HttpMethod.Post, "subscriptions", body, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxOperationResult>(content);
        }

        /// <summary>
        /// POST /subscriptions — подпись со списком типов событий
        /// </summary>
        public Task<MaxOperationResult> CreateSubscriptionAsync(string webhookUrl, IEnumerable<string> updateTypes, string? secret = null, CancellationToken cancellationToken = default)
            => SubscribeAsync(webhookUrl, updateTypes, secret, cancellationToken);

        /// <summary>
        /// DELETE /subscriptions?url= — отписаться от Webhook.
        /// После отписки становится доступен Long Polling (GET /updates)
        /// </summary>
        public async Task<MaxOperationResult> UnsubscribeAsync(string url, CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Delete, $"subscriptions?url={Uri.EscapeDataString(url)}", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxOperationResult>(content);
        }

        /// <summary>
        /// DELETE /subscriptions?url= — устаревшее имя для <see cref="UnsubscribeAsync"/>
        /// </summary>
        public Task DeleteSubscriptionAsync(string url, CancellationToken cancellationToken = default)
            => UnsubscribeAsync(url, cancellationToken);

        #endregion

        #region Updates (Long Polling)

        /// <summary>
        /// GET /updates — получение событий через Long Polling (недоступно при активной Webhook-подписке).
        /// marker — указатель на следующее событие; если не передан — вернутся все необработанные события.
        /// timeout — 0-90 секунд. types — см. Enums.UpdateType
        /// </summary>
        public async Task<MaxUpdateList> GetUpdatesAsync(int? limit = null, int? timeout = null, long? marker = null, IEnumerable<string>? types = null, CancellationToken cancellationToken = default)
        {
            var qs = new List<string>();
            if (limit != null) qs.Add($"limit={limit.Value}");
            if (timeout != null) qs.Add($"timeout={timeout.Value}");
            if (marker != null) qs.Add($"marker={marker.Value}");
            if (types?.Any() == true) qs.Add("types=" + string.Join(",", types));

            // long-poll ждёт на сервере до timeout секунд — добавляем запас к транспортному таймауту
            TimeSpan? requestTimeout = TimeSpan.FromSeconds((timeout ?? 30) + 15);

            var content = await RequestAsync(HttpMethod.Get, $"updates{Query(qs)}", null, requestTimeout, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxUpdateList>(content);
        }

        #endregion

        #region Upload

        /// <summary>
        /// POST /uploads?type= — получить URL для загрузки медиафайла
        /// </summary>
        public async Task<MaxUploadEndpoint> GetUploadEndpointAsync(string type, CancellationToken cancellationToken = default)
        {
            var content = await RequestAsync(HttpMethod.Post, $"uploads?type={type}", null, null, cancellationToken).ConfigureAwait(false);
            return Deserialize<MaxUploadEndpoint>(content);
        }

        /// <summary>
        /// Двухшаговая загрузка медиафайла:
        /// 1) POST /uploads?type={type} получает URL загрузки;
        /// 2) multipart-POST на этот URL (поле "data") возвращает token (video/audio/file)
        ///    или photos (image) — готовый payload для вложения
        /// </summary>
        /// <param name="data">Бинарные данные файла</param>
        /// <param name="fileName">Имя файла</param>
        /// <param name="type">image | video | audio | file (см. Enums.UploadType)</param>
        public async Task<MaxUploadResult> UploadAsync(byte[] data, string fileName, string type = UploadType.File, CancellationToken cancellationToken = default)
        {
            var endpoint = await GetUploadEndpointAsync(type, cancellationToken).ConfigureAwait(false);

            using var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(data);
            content.Add(fileContent, "data", fileName);

            using var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            linked.CancelAfter(_options.Timeout);

            var resp = await UploadHttp.PostAsync(endpoint.Url, content, linked.Token).ConfigureAwait(false);
            var respBody = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!resp.IsSuccessStatusCode)
                throw new MaxApiException($"MAX upload error {(int)resp.StatusCode}: {respBody}", (int)resp.StatusCode);

            return Deserialize<MaxUploadResult>(respBody);
        }

        #endregion

        #region Infrastructure

        private static string Bool(bool value) => value ? "true" : "false";

        private static string Query(List<string> parts) => parts.Count > 0 ? "?" + string.Join("&", parts) : "";

        private async Task<string> RequestAsync(HttpMethod method, string path, object? body, TimeSpan? timeoutOverride, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("Access token MAX не задан");

            using var request = new HttpRequestMessage(method, path);
            if (body != null)
                request.Content = new StringContent(JsonSerializer.Serialize(body, _json), Encoding.UTF8, "application/json");

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(timeoutOverride ?? _options.Timeout);

            HttpResponseMessage response;
            string content;
            try
            {
                response = await _http.SendAsync(request, cts.Token).ConfigureAwait(false);
                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                throw new TimeoutException($"MAX API request to '{path}' timed out after {(timeoutOverride ?? _options.Timeout).TotalSeconds:F0}s");
            }

            using (response)
            {
                if (!response.IsSuccessStatusCode)
                {
                    var error = TryParse<MaxApiError>(content);
                    var message = error?.Message ?? error?.Error ?? content;
                    throw new MaxApiException($"MAX API error {(int)response.StatusCode}: {message}", (int)response.StatusCode, error?.Code, content);
                }

                // Часть методов возвращает HTTP 200 с success=false — это тоже ошибка
                var result = TryParse<MaxOperationResult>(content);
                if (result?.Success == false)
                    throw new MaxApiException($"MAX API error: {result.Message} ({result.Code})", (int)response.StatusCode, result.Code, content);

                return content;
            }
        }

        private T Deserialize<T>(string content) =>
            JsonSerializer.Deserialize<T>(content, _json)!;

        private T? TryParse<T>(string? content) where T : class
        {
            if (string.IsNullOrWhiteSpace(content) || content[0] != '{')
                return null;
            try
            {
                return JsonSerializer.Deserialize<T>(content, _json);
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public void Dispose()
        {
            if (_ownsHttpClient)
                _http.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
