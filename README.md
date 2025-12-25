# MAX.Messenger.API

Unofficial .NET client for MAX Messenger API.

⚠️ Not affiliated with MAX.

## Features

- Messages
- Keyboards
- Attachments
- Webhook subscriptions
- File upload

## Installation

```bash
dotnet add package MAX.Messenger.API
```


## Usage of sending message

```csharp
var client = new MaxApiClient("YOUR_ACCESS_TOKEN");
await client.SendMessageAsync(chatId, new NewMessageBody
    {
        Text = "Hello!",
        Format = TextFormat.Markdown
    });
```


## Usage of recieving messages (Webhook)

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


## Usage of Answering callback

```csharp
await client.AnswerCallbackAsync(
    callbackId,
    notification: "Done"
);
```

## Notes

- Inline keyboards are sent via attachments
- Callback payload is available in update.Callback.Payload
- callback_id must be sent as query parameter for /answers


## Links

Official MAX API: https://dev.max.ru/docs-api