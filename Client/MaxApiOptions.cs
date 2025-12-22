using System;

namespace MAX.Messenger.API.Client
{
    public class MaxApiOptions
    {
        public string BaseUrl { get; set; } = "https://platform-api.max.ru";
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);
    }
}
