using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAX.Messenger.API.Objects
{
    /// <summary>
    /// Вложение сообщения (Attachment). При ответе сервера дополнительные поля
    /// (width, height, filename и т.д.) зависят от типа вложения.
    /// </summary>
    public class MaxMessageAttachment
    {
        /// <summary>
        /// image, video, audio, file, sticker, contact, location, share, inline_keyboard.
        /// См. <see cref="Enums.AttachmentType"/>
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Payload вложения в зависимости от Type:
        /// - image: MaxPhotoAttachmentPayload
        /// - video/audio: MaxMediaAttachmentPayload
        /// - file: MaxFileAttachmentPayload
        /// - sticker: MaxStickerAttachmentPayload
        /// - contact: MaxContactAttachmentPayload
        /// - share: MaxShareAttachmentPayload
        /// - inline_keyboard: InlineKeyboardAttachmentRequestPayload
        /// При десериализации содержит JsonElement — используйте <see cref="GetPayload{T}"/>
        /// </summary>
        [JsonPropertyName("payload")]
        public object? Payload { get; set; }

        /// <summary>Широта (location)</summary>
        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        /// <summary>Долгота (location)</summary>
        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

        /// <summary>Ширина (video, sticker)</summary>
        [JsonPropertyName("width")]
        public int? Width { get; set; }

        /// <summary>Высота (video, sticker)</summary>
        [JsonPropertyName("height")]
        public int? Height { get; set; }

        /// <summary>Длительность в секундах (video)</summary>
        [JsonPropertyName("duration")]
        public int? Duration { get; set; }

        /// <summary>Имя загруженного файла (file)</summary>
        [JsonPropertyName("filename")]
        public string? Filename { get; set; }

        /// <summary>Размер файла в байтах (file)</summary>
        [JsonPropertyName("size")]
        public long? Size { get; set; }

        /// <summary>Распознанный текст аудио (audio)</summary>
        [JsonPropertyName("transcription")]
        public string? Transcription { get; set; }

        /// <summary>Заголовок превью ссылки (share)</summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>Описание превью ссылки (share)</summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>URL картинки превью (share)</summary>
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        /// <summary>Миниатюра (video)</summary>
        [JsonPropertyName("thumbnail")]
        public MaxVideoThumbnail? Thumbnail { get; set; }

        /// <summary>
        /// Десериализует Payload в типизированный объект.
        /// Пример: attachment.GetPayload&lt;MaxPhotoAttachmentPayload&gt;()
        /// </summary>
        public T? GetPayload<T>() where T : class
        {
            if (Payload is T typed)
                return typed;

            if (Payload is JsonElement element)
                return element.Deserialize<T>(PayloadJsonOptions);

            return Payload as T;
        }

        /// <summary>URL медиа из payload (image/video/audio/file/sticker/share)</summary>
        public string? GetUrl() => GetPayload<MaxAttachmentPayloadBase>()?.Url;

        /// <summary>Токен из payload (для повторного использования вложения)</summary>
        public string? GetToken() => (GetPayload<MaxAttachmentPayloadBase>() as MaxMediaTokenPayload)?.Token;

        private static readonly JsonSerializerOptions PayloadJsonOptions = new()
        {
            PropertyNameCaseInsensitive = false
        };
    }

    /// <summary>
    /// Базовый payload вложения с медиа URL
    /// </summary>
    public class MaxAttachmentPayloadBase
    {
        /// <summary>
        /// URL медиа. Для видео прямые ссылки получайте через GET /videos/{videoToken}
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    /// <summary>
    /// Payload с токеном повторного использования (video, audio, file, share)
    /// </summary>
    public class MaxMediaTokenPayload : MaxAttachmentPayloadBase
    {
        /// <summary>
        /// Токен для повторного использования того же вложения в других сообщениях
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }

    /// <summary>
    /// Payload вложения-изображения (type=image)
    /// </summary>
    public class MaxPhotoAttachmentPayload : MaxAttachmentPayloadBase
    {
        /// <summary>Уникальный ID изображения</summary>
        [JsonPropertyName("photo_id")]
        public long PhotoId { get; set; }

        /// <summary>Токен изображения</summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }

    /// <summary>
    /// Payload вложения video/audio (url + token)
    /// </summary>
    public class MaxMediaAttachmentPayload : MaxMediaTokenPayload
    {
    }

    /// <summary>
    /// Payload вложения file (url + token)
    /// </summary>
    public class MaxFileAttachmentPayload : MaxMediaTokenPayload
    {
    }

    /// <summary>
    /// Payload вложения sticker
    /// </summary>
    public class MaxStickerAttachmentPayload : MaxAttachmentPayloadBase
    {
        /// <summary>Идентификатор стикера</summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }
    }

    /// <summary>
    /// Payload вложения contact
    /// </summary>
    public class MaxContactAttachmentPayload
    {
        /// <summary>Информация о контакте в формате VCF</summary>
        [JsonPropertyName("vcf_info")]
        public string? VcfInfo { get; set; }

        /// <summary>Хэш VCF-информации</summary>
        [JsonPropertyName("hash")]
        public string? Hash { get; set; }

        /// <summary>Пользователь MAX, если контакт зарегистрирован</summary>
        [JsonPropertyName("max_info")]
        public MaxUser? MaxInfo { get; set; }
    }

    /// <summary>
    /// Payload превью ссылки (type=share). url и token взаимоисключающие
    /// </summary>
    public class MaxShareAttachmentPayload
    {
        /// <summary>URL, к которому генерируется медиа-превью</summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>Токен вложения</summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }

    /// <summary>
    /// Payload для исходящего вложения-изображения (PhotoAttachmentRequest).
    /// Поля url / token / photos взаимоисключающие
    /// </summary>
    public class MaxPhotoAttachmentRequestPayload
    {
        /// <summary>Внешний URL картинки, который нужно прикрепить</summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>Токен уже загруженного вложения</summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>
        /// Токены, полученные после загрузки изображений через /uploads?type=image
        /// </summary>
        [JsonPropertyName("photos")]
        public Dictionary<string, MaxPhotoToken>? Photos { get; set; }
    }

    /// <summary>
    /// Токен загруженного изображения
    /// </summary>
    public class MaxPhotoToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = null!;
    }

    /// <summary>
    /// Payload для исходящих вложений video/audio/file — то, что вернул сервер после загрузки
    /// </summary>
    public class MaxUploadedInfo
    {
        /// <summary>Уникальный токен загруженного медиа</summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = null!;
    }

    /// <summary>
    /// Payload для исходящего вложения sticker (единственное вложение в сообщении)
    /// </summary>
    public class MaxStickerAttachmentRequestPayload
    {
        /// <summary>Код стикера</summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = null!;
    }

    /// <summary>
    /// Payload для исходящего вложения contact (единственное вложение в сообщении)
    /// </summary>
    public class MaxContactAttachmentRequestPayload
    {
        /// <summary>Имя контакта</summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>ID пользователя MAX, если контакт зарегистрирован</summary>
        [JsonPropertyName("contact_id")]
        public long? ContactId { get; set; }

        /// <summary>Полная информация о контакте в формате VCF</summary>
        [JsonPropertyName("vcf_info")]
        public string? VcfInfo { get; set; }

        /// <summary>Телефон контакта в формате VCF</summary>
        [JsonPropertyName("vcf_phone")]
        public string? VcfPhone { get; set; }
    }

    /// <summary>
    /// Миниатюра видео
    /// </summary>
    public class MaxVideoThumbnail
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;
    }

    /// <summary>
    /// Устаревший обобщённый payload вложения. Сохранён для обратной совместимости,
    /// рекомендуется MaxPhotoAttachmentRequestPayload / MaxUploadedInfo
    /// </summary>
    public class MaxAttachmentPayload
    {
        [JsonPropertyName("photos")]
        public IEnumerable<string>? Photos { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
