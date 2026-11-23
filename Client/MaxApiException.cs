using System;

namespace MAX.Messenger.API.Client
{
    /// <summary>
    /// Исключение MAX API: содержит HTTP-код и, если сервер его вернул,
    /// машиночитаемый код ошибки (поле code в формате "some.error")
    /// </summary>
    public class MaxApiException : Exception
    {
        /// <summary>HTTP-код ответа (200, если ошибка сигнализирована полем success=false в теле)</summary>
        public int StatusCode { get; }

        /// <summary>
        /// Код ошибки из ответа сервера: not.found, chat.denied, proto.payload и т.п.
        /// Может быть null для старых ответов и 5xx
        /// </summary>
        public string? ErrorCode { get; }

        /// <summary>Сырое тело ответа</summary>
        public string? ResponseBody { get; }

        public MaxApiException(string message, int statusCode, string? errorCode = null, string? responseBody = null)
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            ResponseBody = responseBody;
        }
    }
}
