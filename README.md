# MAX.Messenger.API

Unofficial .NET client for MAX Messenger Bot API (https://platform-api2.max.ru).

⚠️ Not affiliated with MAX.

## Features

- Sending, editing, deleting and reading messages
- Inline keyboards (callback / link / open_app / request_geo_location / request_contact / message / clipboard)
- Attachments: image, video, audio, file, sticker, contact, location, share (link preview)
- Two-step file upload (`POST /uploads?type=...` → upload to returned URL)
- Webhook subscriptions (with `secret` → `X-Max-Bot-Api-Secret`) and Long Polling (`GET /updates`)
- Chats: info, edit (title/description/icon), actions, pin/unpin, members, admins
- Comments to channel posts (send / get / edit / delete)
- Bot info (`GET /me`) and bot commands (`PATCH /me/commands`)
- All update types, including `message_edited`, `comment_created`, `bot_admin_permissions_changed`, etc.
- Error handling with machine-readable `code` (`not.found`, `chat.denied`, ...) via `MaxApiException`

## Installation

```bash
dotnet add package MAX.Messenger.API
```

## Important notes (since 2026)

- The Bot API domain is **`https://platform-api2.max.ru`** — `platform-api.max.ru` is deprecated.
  Make sure the Russian root certificate (Минцифры) is trusted on your server.
- Token is passed **only** in the `Authorization: <access_token>` header — query parameter `access_token` is no longer supported.
- Some methods return HTTP 200 with `"success": false` — this library detects it and throws `MaxApiException` with `ErrorCode`.

## Sending a message

```csharp
using MAX.Messenger.API.Client;
using MAX.Messenger.API.Enums;
using MAX.Messenger.API.Requests;

var client = new MaxApiClient("YOUR_ACCESS_TOKEN");

var sent = await client.SendMessageAsync(new NewMessageBody
    {
        Text = "Hello!",
        Format = TextFormat.Markdown
    },
    chatId: 123); // or userId: 456

string mid = sent.Body.Mid; // can be used later for EditMessageAsync / DeleteMessageAsync
```

## Inline keyboard

```csharp
using MAX.Messenger.API.Keyboards;
using MAX.Messenger.API.Objects;

var keyboard = new MaxMessageAttachment
{
    Type = AttachmentType.InlineKeyboard,
    Payload = new InlineKeyboardAttachmentRequestPayload
    {
        Buttons = new()
        {
            new()
            {
                new InlineKeyboardButton { Type = "callback", Text = "Yes", Payload = "yes" },
                new InlineKeyboardButton { Type = "link", Text = "Docs", Url = "https://dev.max.ru" }
            }
        }
    }
};

await client.SendMessageAsync(new NewMessageBody { Text = "Choose:", Attachments = new[] { keyboard } }, chatId: 123);
```

## Receiving updates (Webhook)

```csharp
[HttpPost]
public IActionResult Callback([FromBody] MaxUpdate update)
{
    if (update.UpdateType == UpdateType.MessageCreated)
    {
        var text = update.Message?.Body?.Text;
    }

    if (update.UpdateType == UpdateType.MessageCallback)
    {
        var payload = update.Callback?.Payload;
    }

    return Ok();
}
```

If the subscription was created with a `secret`, validate the `X-Max-Bot-Api-Secret` header of each request.

## Receiving updates (Long Polling)

```csharp
long? marker = null;
while (true)
{
    var page = await client.GetUpdatesAsync(timeout: 30, marker: marker);
    foreach (var update in page.Updates) { /* handle */ }
    marker = page.Marker;
}
```

## Answering a callback

```csharp
await client.AnswerCallbackAsync(
    callbackId,
    notification: "Done"
);
```

## Subscribing to webhook events

```csharp
await client.SubscribeAsync(
    webhookUrl: "https://bot.example.com/max/webhook",
    updateTypes: new[] { UpdateType.MessageCreated, UpdateType.MessageCallback },
    secret: "my-long-secret");
```

## Upload and send media

```csharp
using MAX.Messenger.API.Enums;
using MAX.Messenger.API.Objects;

var uploaded = await client.UploadAsync(File.ReadAllBytes("cat.jpg"), "cat.jpg", UploadType.Image);

var attach = new MaxMessageAttachment
{
    Type = AttachmentType.Image,
    Payload = new MaxPhotoAttachmentRequestPayload { Photos = uploaded.Photos }
};

await client.SendMessageAsync(new NewMessageBody { Text = "Look!", Attachments = new[] { attach } }, userId: 456);
```

## Notes

- Inline keyboards are sent via attachments
- Callback payload is available in `update.Callback.Payload`
- `callback_id` is sent as a query parameter for `/answers` (not in the body)
- `GET /chats` is no longer supported by MAX (since June 2026) — use webhook updates to learn where your bot was added
- `SendMessageAsync` now returns the created `MaxMessage`

## Links

Official MAX API docs: https://dev.max.ru/docs-api
Official OpenAPI schema: https://github.com/max-messenger/api-schema
