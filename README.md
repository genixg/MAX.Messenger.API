# MAX.Messenger.API

Unofficial .NET client for MAX Messenger API.
⚠️ Not affiliated with MAX.

## Features
- Messages
- Keyboards
- Attachments
- Webhook subscriptions
- File upload

## Usage
var client = new MaxApiClient("YOUR_ACCESS_TOKEN");
await client.SendMessageAsync(chatId, "Hello!");


## Installation
```bash
dotnet add package MAX.Messenger.API
