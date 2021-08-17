using System;
using System.Collections.Generic;

using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#region Defaults

namespace VKKeksikLib.Request.Default
{
    /// <summary>
    /// Основной класс. Стандартный класс запроса.
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}
namespace VKKeksikLib.Response.Default
{
    /// <summary>
    /// Основной класс. Стандартный класс ответа.
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// Успешность обработки запроса сервером.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Если запрос выполнен с ошибкой, то будет возвращен код ошибки. Используйте GetErrorCodeInfo(IntErrorCode) для получения информации об ошибке или посмотрите список ошибок: https://keksik.io/api#error-table
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public int Error { get; set; }

        /// <summary>
        /// Текстовое сообщение с результатом выполнения запроса.
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}

namespace VKKeksikLib.Request.IDS
{
    /// <summary>
    /// Основной класс. Стандартный класс запроса с указанием списка ID.
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// Можно передать массив системных ID
        /// </summary>
        [JsonProperty("ids", NullValueHandling = NullValueHandling.Ignore)]
        public int[] IDs { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}

#endregion

#region Donates

namespace VKKeksikLib.Donates.Get.Request
{
    /// <summary>
    /// Основной класс
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// Количество донатов в списке.
        /// Максимум 100. По умолчанию 20.
        /// </summary>
        [JsonProperty("len", NullValueHandling = NullValueHandling.Ignore)]
        public int Len { get; set; }

        /// <summary>
        /// Смещение по выборе донатов
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int Offset { get; set; }

        /// <summary>
        /// Временная метка по UNIX (в миллисекундах). Задает минимальную дату и время выбираемых донатов.
        /// </summary>
        [JsonProperty("start_date", NullValueHandling = NullValueHandling.Ignore)]
        public long StartDate { get; set; }

        /// <summary>
        /// Временная метка по UNIX (в миллисекундах). Задает максимальную дату и время выбираемых донатов.
        /// </summary>
        [JsonProperty("end_date", NullValueHandling = NullValueHandling.Ignore)]
        public long EndDate { get; set; }

        /// <summary>
        /// Метод сортировки. По умолчанию date. 
        /// Возможные значения: 
        /// date - сортировка по дате; 
        /// amount - сортировка по сумме.
        /// </summary>
        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public string Sort { get; set; }

        /// <summary>
        /// Направление сортировки. По умолчанию false. 
        /// Возможные значения: 
        /// false - сортировка по убыванию; 
        /// true - сортировка по возрастанию.
        /// </summary>
        [JsonProperty("reverse", NullValueHandling = NullValueHandling.Ignore)]
        public bool Reverse { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}
namespace VKKeksikLib.Donates.Get.Response
{
    /// <summary>
    /// Основной класс
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// Успешность обработки запроса сервером.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Если запрос выполнен с ошибкой, то будет возвращен код ошибки. Используйте GetErrorCodeInfo(IntErrorCode) для получения информации об ошибке или посмотрите список ошибок: https://keksik.io/api#error-table
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public int Error { get; set; }

        /// <summary>
        /// Текстовое сообщение с результатом выполнения запроса.
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        /// <summary>
        /// Список донатов.
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public List<VKKeksikLib.DonateAnswer.Donate> List { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}

namespace VKKeksikLib.Donates.ChangeStatus.Request
{
    /// <summary>
    /// Основной класс. Для обработки ответа используйте NULL.Response.JSON
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// ID доната в системе.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Статус доната. Возможные значения: public - опубликован; hidden - скрыт.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}
// Use VKKeksikLib.Response.Default

namespace VKKeksikLib.Donates.Answer.Request
{
    /// <summary>
    /// Основной класс. Для обработки ответа используйте NULL.Response.JSON
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// ID доната в системе.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Текст ответа. Для удаления ответа следует передать пустую строку.
        /// </summary>
        [JsonProperty("answer")]
        public string Answer { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}
// Use VKKeksikLib.Response.Default

namespace VKKeksikLib.Donates.ChangeRewardStatus.Request
{
    /// <summary>
    /// Основной класс. Для обработки ответа используйте NULL.Response.JSON
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// ID доната в системе.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Статус выдачи вознаграждения. not_sended - не вадно; sended - выдано.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}
// Use VKKeksikLib.Response.Default

#endregion

#region Campaigns

namespace VKKeksikLib.Campaigns
{
    /// <summary>
    /// Класс кампании для обработки JSON массива
    /// </summary>
    public class Campaign
    {
        /// <summary>
        /// Основной класс массива
        /// </summary>
        public partial class JSON
        {
            /// <summary>
            /// ID кампании в системе.
            /// </summary>
            [JsonProperty("id")]
            public int Id { get; set; }

            /// <summary>
            /// Заголовок кампании.
            /// </summary>
            [JsonProperty("title")]
            public string Title { get; set; }

            /// <summary>
            /// Статус кампании. draft - черновик; active - активная кампания; archive - кампания архивирована.
            /// </summary>
            [JsonProperty("status")]
            public string Status { get; set; }

            /// <summary>
            /// Временная метка по unix (в миллисекундах) начала кампании.
            /// </summary>
            [JsonProperty("start")]
            public int Start { get; set; }

            /// <summary>
            /// Временная метка по unix (в миллисекундах) окончания кампании.
            /// </summary>
            [JsonProperty("end")]
            public int End { get; set; }

            /// <summary>
            /// Цель по сбору в рублях.
            /// </summary>
            [JsonProperty("point")]
            public int Point { get; set; }

            /// <summary>
            /// Собрано за пределами приложения в рублях.
            /// </summary>
            [JsonProperty("start_received")]
            public float StartReceived { get; set; }

            /// <summary>
            /// Кол-во спонсоров пожертвовавших за пределами приложения.
            /// </summary>
            [JsonProperty("start_backers")]
            public int StartBackers { get; set; }

            /// <summary>
            /// Собрано на данный момент в рублях.
            /// </summary>
            [JsonProperty("received")]
            public float Received { get; set; }

            /// <summary>
            /// Кол-во спонсоров.
            /// </summary>
            [JsonProperty("backers")]
            public int Backers { get; set; }
        }
        public partial class JSON
        {
            /// <summary>
            /// Конвертация строки массива в объект
            /// </summary>
            /// <param name="json">JSON массив в виде строки</param>
            /// <returns></returns>
            public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
        }
    }
    /// <summary>
    /// Класс вознаграждения для обработки JSON массива
    /// </summary>
    public class Reward
    {
        /// <summary>
        /// Основной класс массива
        /// </summary>
        public partial class JSON
        {
            /// <summary>
            /// ID вознаграждения в системе.
            /// </summary>
            [JsonProperty("id")]
            public int Id { get; set; }

            /// <summary>
            /// Название вознаграждения.
            /// </summary>
            [JsonProperty("title")]
            public string Title { get; set; }

            /// <summary>
            /// Описание вознаграждения.
            /// </summary>
            [JsonProperty("desc")]
            public string Desc { get; set; }

            /// <summary>
            /// Минимальный донат для получения текущего вознаграждения.
            /// </summary>
            [JsonProperty("min_donate")]
            public int MinDonate { get; set; }

            /// <summary>
            /// Ограничение кол-во вознаграждений. Если ограничений нет, данное поле не передается.
            /// </summary>
            [JsonProperty("limits")]
            public int Limits { get; set; }

            /// <summary>
            /// Статус вознаграждения. public - вознаграждение опубликовано; hidden - вознаграждение скрыто.
            /// </summary>
            [JsonProperty("status")]
            public string Status { get; set; }

            /// <summary>
            /// Кол-во спонсоров выбравших данное вознаграждение.
            /// </summary>
            [JsonProperty("backers")]
            public int Backers { get; set; }
        }
        public partial class JSON
        {
            /// <summary>
            /// Конвертация строки массива в объект
            /// </summary>
            /// <param name="json">JSON массив в виде строки</param>
            /// <returns></returns>
            public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
        }
    }
}

// Use VKKeksikLib.Request.IDS
namespace VKKeksikLib.Campaigns.Get.Response
{
    /// <summary>
    /// Основной класс.
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// Успешность обработки запроса сервером.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Если запрос выполнен с ошибкой, то будет возвращен код ошибки. Используйте GetErrorCodeInfo(IntErrorCode) для получения информации об ошибке или посмотрите список ошибок: https://keksik.io/api#error-table
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public int Error { get; set; }

        /// <summary>
        /// Текстовое сообщение с результатом выполнения запроса.
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        /// <summary>
        /// Список кампаний.
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public List<VKKeksikLib.Campaigns.Campaign.JSON> List { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}

// Use VKKeksikLib.Request.Default
namespace VKKeksikLib.Campaigns.GetActive.Response
{
    /// <summary>
    /// Основной класс
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// Успешность обработки запроса сервером. Пустой ответ со всеми стандартными полями. Может быть использован для получения ответа в запросах без ожидания ответа с данными.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Если запрос выполнен с ошибкой, то будет возвращен код ошибки. Используйте GetErrorCodeInfo(IntErrorCode) для получения информации об ошибке или посмотрите список ошибок: https://keksik.io/api#error-table
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public int Error { get; set; }

        /// <summary>
        /// Текстовое сообщение с результатом выполнения запроса.
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        /// <summary>
        /// Объект кампании.
        /// </summary>
        [JsonProperty("campaign", NullValueHandling = NullValueHandling.Ignore)]
        public VKKeksikLib.Campaigns.Campaign.JSON Campaign { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}

namespace VKKeksikLib.Campaigns.GetRewards.Request
{
    /// <summary>
    /// Основной класс. Используется для "пустых" запросов с указанием авторризационных данных. Рекомендуется только для тестирования!
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// ID кампании в системе.
        /// </summary>
        [JsonProperty("campaign")]
        public int Campaign { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}
namespace VKKeksikLib.Campaigns.GetRewards.Response
{
    /// <summary>
    /// Основной класс
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// Успешность обработки запроса сервером. Пустой ответ со всеми стандартными полями. Может быть использован для получения ответа в запросах без ожидания ответа с данными.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Если запрос выполнен с ошибкой, то будет возвращен код ошибки. Используйте GetErrorCodeInfo(IntErrorCode) для получения информации об ошибке или посмотрите список ошибок: https://keksik.io/api#error-table
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public int Error { get; set; }

        /// <summary>
        /// Текстовое сообщение с результатом выполнения запроса.
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        /// <summary>
        /// Массив объектов вознаграждений.
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public List<VKKeksikLib.Campaigns.Reward> List { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}

namespace VKKeksikLib.Campaigns.Change.Request
{
    /// <summary>
    /// Основной класс. Используется для "пустых" запросов с указанием авторризационных данных. Рекомендуется только для тестирования!
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// ID кампании в системе.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Заголовок кампании.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// Статус кампании. draft - черновик; active - активная кампания; archive - кампания архивирована.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        /// <summary>
        /// Временная метка по unix (в миллисекундах) окончания кампании.
        /// </summary>
        [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public long End { get; set; }

        /// <summary>
        /// Цель по сбору в рублях.
        /// </summary>
        [JsonProperty("point", NullValueHandling = NullValueHandling.Ignore)]
        public int Point { get; set; }

        /// <summary>
        /// Собрано за пределами приложения в рублях.
        /// </summary>
        [JsonProperty("start_received", NullValueHandling = NullValueHandling.Ignore)]
        public float StartReceived { get; set; }

        /// <summary>
        /// Кол-во спонсоров пожертвовавших за пределами приложения.
        /// </summary>
        [JsonProperty("start_backers", NullValueHandling = NullValueHandling.Ignore)]
        public int StartBackers { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}
// Use VKKeksikLib.Response.Default

namespace VKKeksikLib.Campaigns.ChangeReward.Request
{
    /// <summary>
    /// Основной класс. Используется для "пустых" запросов с указанием авторризационных данных. Рекомендуется только для тестирования!
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// ID вознаграждения в системе.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Название вознаграждения.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// Описание вознаграждения.
        /// </summary>
        [JsonProperty("desc", NullValueHandling = NullValueHandling.Ignore)]
        public string Desc { get; set; }

        /// <summary>
        /// Минимальный донат для получения текущего вознаграждения.
        /// </summary>
        [JsonProperty("min_donate", NullValueHandling = NullValueHandling.Ignore)]
        public int MinDonate { get; set; }

        /// <summary>
        /// Ограничение кол-во вознаграждений. Если ограничений нет, данное поле не передается.
        /// </summary>
        [JsonProperty("limits", NullValueHandling = NullValueHandling.Ignore)]
        public int Limits { get; set; }

        /// <summary>
        /// Статус вознаграждения. public - вознаграждение опубликовано; hidden - вознаграждение скрыто.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}
// Use VKKeksikLib.Response.Default

#endregion

#region Payments

// Use VKKeksikLib.Request.IDS
namespace VKKeksikLib.Payments.Get.Response
{
    /// <summary>
    /// Основной класс
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// Успешность обработки запроса сервером.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Если запрос выполнен с ошибкой, то будет возвращен код ошибки. Используйте GetErrorCodeInfo(IntErrorCode) для получения информации об ошибке или посмотрите список ошибок: https://keksik.io/api#error-table
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public int Error { get; set; }

        /// <summary>
        /// Текстовое сообщение с результатом выполнения запроса.
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        /// <summary>
        /// Массив объектов заявок на выплату.
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public List<Payment> List { get; set; }
    }

    /// <summary>
    /// Заявка на выплату
    /// </summary>
    public partial class Payment
    {
        /// <summary>
        /// ID выплаты в системе.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Статус вывода. created - заявка обрабаывается; ready - заявка выполнена; error - произошла ошибка.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Временная метка по unix (в миллисекундах) даты и времени обработки заявки.
        /// </summary>
        [JsonProperty("processed")]
        public long Processed { get; set; }

        /// <summary>
        /// Платежная система. bank - Банковская карта; qiwi - Qiwi; webmoney - WebMoney; yandex_money - Яндекс.Деньги; mobile - Счет мобильного телефона.
        /// </summary>
        [JsonProperty("system")]
        public string System { get; set; }

        /// <summary>
        /// Счет в платежной системе на который заказана выплата.
        /// </summary>
        [JsonProperty("purse")]
        public string Purse { get; set; }

        /// <summary>
        /// Сумма выплаты в рублях указанная в заявке.
        /// </summary>
        [JsonProperty("amount")]
        public float Amount { get; set; }

        /// <summary>
        /// VK ID пользователя создавшего заявку.
        /// </summary>
        [JsonProperty("user")]
        public int User { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}

namespace VKKeksikLib.Payments.Create.Request
{
    /// <summary>
    /// Основной класс.
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group")]
        public int Group { get; set; }

        /// <summary>
        /// Секретный токен.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Номер версии api.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }

        /// <summary>
        /// Платежная система. bank - Банковская карта; qiwi - Qiwi; webmoney - WebMoney; yandex_money - Яндекс.Деньги; mobile - Счет мобильного телефона.
        /// </summary>
        [JsonProperty("system")]
        public string System { get; set; }

        /// <summary>
        /// Счет в платежной системе на который будет произведена выплата.
        /// </summary>
        [JsonProperty("purse")]
        public string Purse { get; set; }

        /// <summary>
        /// Сумма выплаты в рублях.
        /// </summary>
        [JsonProperty("amount")]
        public float Amount { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}
namespace VKKeksikLib.Payments.Create.Response
{
    /// <summary>
    /// Основной класс
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// Успешность обработки запроса сервером.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Если запрос выполнен с ошибкой, то будет возвращен код ошибки. Используйте GetErrorCodeInfo(IntErrorCode) для получения информации об ошибке или посмотрите список ошибок: https://keksik.io/api#error-table
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public int Error { get; set; }

        /// <summary>
        /// Текстовое сообщение с результатом выполнения запроса.
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        /// <summary>
        /// ID выплаты в системе.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}

#endregion

#region GetBalance

// Default request JSON
namespace VKKeksikLib.BalanceJSON.Get.Response
{
    /// <summary>
    /// Основной класс
    /// </summary>
    public partial class JSON
    {
        /// <summary>
        /// Успешность обработки запроса сервером.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Если запрос выполнен с ошибкой, то будет возвращен код ошибки. Используйте GetErrorCodeInfo(IntErrorCode) для получения информации об ошибке или посмотрите список ошибок: https://keksik.io/api#error-table
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public int Error { get; set; }

        /// <summary>
        /// Текстовое сообщение с результатом выполнения запроса.
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        /// <summary>
        /// Баланс группы в копейках.
        /// </summary>
        [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]
        public float Balance { get; set; }
    }

    public partial class JSON
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static JSON FromJson(string json) => JsonConvert.DeserializeObject<JSON>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}

#endregion