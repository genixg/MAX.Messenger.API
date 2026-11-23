using System;

namespace MAX.Messenger.API.Client
{
    public class MaxApiOptions
    {
        /// <summary>
        /// Актуальный домен Bot API MAX (с 19.07.2026 platform-api2.max.ru вместо platform-api.max.ru).
        /// </summary>
        public string BaseUrl { get; set; } = "https://platform-api2.max.ru";
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);
    }
}
