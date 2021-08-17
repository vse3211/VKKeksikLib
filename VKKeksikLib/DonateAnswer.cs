namespace VKKeksikLib.DonateAnswer
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Основной класс
    /// </summary>
    public partial class Da
    {
        /// <summary>
        /// Тип уведомления.
        /// Возможные значения:
        /// confirmation - подтверждение страницы при настройке адреса сервера в приложении;
        /// new_donate - новый донат;
        /// payment_status - изменение статуса заявки на выплату.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        /// Объект с информацией о донате.
        /// </summary>
        [JsonProperty("donate", NullValueHandling = NullValueHandling.Ignore)]
        public Donate Donate { get; set; }

        /// <summary>
        /// Объект с информацией о заявке на выплату.
        /// </summary>
        [JsonProperty("payment", NullValueHandling = NullValueHandling.Ignore)]
        public Payment Payment { get; set; }

        /// <summary>
        /// VK ID группы.
        /// </summary>
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }

        /// <summary>
        /// Хеш запроса. Нужен для проверки того, что уведомление получено от нашего сервера, а не от злоумышленника. Подробнее про проверку хеша читайте в https://keksik.io/api#callback-hash или воспользуйтесь существующим классом.
        /// </summary>
        [JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash { get; set; }
    }

    #region Donate in Da
    /// <summary>
    /// Объект с информацией о донате.
    /// </summary>
    public partial class Donate
    {
        /// <summary>
        /// ID донаты в системе.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        /// <summary>
        /// Временная метка по UNIX (в миллисекундах) даты и времени доната.
        /// </summary>
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public long Date { get; set; }

        /// <summary>
        /// Сумма доната в рублях.
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public int Amount { get; set; }

        /// <summary>
        /// Сумма поступившая на баланс приложения в копейках. Данное поле будет отсутствовать если донат был отправлен через VK Pay, либо импортирован из другого приложения.
        /// </summary>
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public int Total { get; set; }

        /// <summary>
        /// Анонимность доната.
        /// </summary>
        [JsonProperty("anonym", NullValueHandling = NullValueHandling.Ignore)]
        public bool Anonym { get; set; }

        /// <summary>
        /// Сообщение прикрепленное к донату.
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        /// <summary>
        /// Ответ администратора группы на донат.
        /// </summary>
        [JsonProperty("answer", NullValueHandling = NullValueHandling.Ignore)]
        public string Answer { get; set; }

        /// <summary>
        /// Если донат был отправлен через VK Pay, то значение данного поля будет равно true.
        /// </summary>
        [JsonProperty("vkpay", NullValueHandling = NullValueHandling.Ignore)]
        public bool Vkpay { get; set; }

        /// <summary>
        /// Статус доната
        /// Возможные значения:
        /// new - новый донат;
        /// public - опубликован;
        /// hidden - скрыт.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        /// <summary>
        /// VK ID пользователя совершившего донат. Если донаты отправлен анонимно, то значение данного поле будет 0.
        /// </summary>
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public int User { get; set; }

        /// <summary>
        /// Если не было выбрано вознаграждение, то данный объект будет пустым.
        /// </summary>
        [JsonProperty("reward", NullValueHandling = NullValueHandling.Ignore)]
        public Reward Reward { get; set; }
    }

    /// <summary>
    /// Если не было выбрано вознаграждение, то данный объект будет пустым.
    /// </summary>
    public partial class Reward
    {
        /// <summary>
        /// ID вознаграждения в системе.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        /// <summary>
        /// Название вознаграждения.
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public int Title { get; set; }

        /// <summary>
        /// Статус выдачи вознаграждения.
        /// Возможные значения:
        /// not_sended - не выдано;
        /// sended - выдано.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }
    }
    #endregion

    #region Payment in Da
    /// <summary>
    /// Объект с информацией о заявке на выплату.
    /// </summary>
    public partial class Payment
    {
        /// <summary>
        /// ID выплаты в системе.
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        /// <summary>
        /// Статус вывода. 
        /// created - заявка обрабаывается; 
        /// ready - заявка выполнена; 
        /// error - произошла ошибка.
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        /// <summary>
        /// Временная метка по unix (в миллисекундах) даты и времени обработки заявки.
        /// </summary>
        [JsonProperty("processed", NullValueHandling = NullValueHandling.Ignore)]
        public int Processed { get; set; }

        /// <summary>
        /// Платежная система. 
        /// bank - Банковская карта; 
        /// qiwi - Qiwi; 
        /// webmoney - WebMoney; 
        /// yandex_money - Яндекс.Деньги; 
        /// mobile - Счет мобильного телефона.
        /// </summary>
        [JsonProperty("system", NullValueHandling = NullValueHandling.Ignore)]
        public string System { get; set; }

        /// <summary>
        /// Счет в платежной системе на который заказана выплата.
        /// </summary>
        [JsonProperty("purse", NullValueHandling = NullValueHandling.Ignore)]
        public string Purse { get; set; }

        /// <summary>
        /// Сумма выплаты в рублях указанная в заявке.
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public int Amount { get; set; }

        /// <summary>
        /// VK ID пользователя создавшего заявку.
        /// </summary>
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public int User { get; set; }
    }
    #endregion

    public partial class Da
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static Da FromJson(string json) => JsonConvert.DeserializeObject<Da>(json, VKKeksikLib.Converters.Converter.Settings);
    }
    public partial class Donate
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static Dictionary<string, DonateAnswer.Donate> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, DonateAnswer.Donate>>(json, VKKeksikLib.Converters.Converter.Settings);
    }
    public partial class Payment
    {
        /// <summary>
        /// Конвертация строки массива в объект
        /// </summary>
        /// <param name="json">JSON массив в виде строки</param>
        /// <returns></returns>
        public static Dictionary<string, DonateAnswer.Payment> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, DonateAnswer.Payment>>(json, VKKeksikLib.Converters.Converter.Settings);
    }
}