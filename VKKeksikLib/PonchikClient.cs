using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace VKKeksikLib
{
    //Данная библиотека создана мной для использования API донат - сервиса "Пончик" в C# приложениях
    //С помощью написанной мной библиотеки вы сможете подключить API донат - сервиса к своему приложению и использовать его без особых усилий
    //Возможно библиотека будет обновлятся, но это зависит только от вашей поддержки
    //Ваш Londonist (StealthKiller#8719, https://vk.com/londonist)

    /// <summary>
    /// Основной класс библиотеки
    /// </summary>
    public class PonchikClient
    {
        #region Main Values
        /// <summary>
        /// Секретный ключ
        /// </summary>
        private static string _SecretKey;
        /// <summary>
        /// Ключ подтверждения
        /// </summary>
        private static string _ConfirmKey;
        /// <summary>
        /// ID вашей группы
        /// </summary>
        private static int _GroupID;
        /// <summary>
        /// Ваш токен API в приложении
        /// </summary>
        private static string _APIToken;
        /// <summary>
        /// Версия API. Задается библиотекой
        /// </summary>
        private static int _APIVersion;
        #endregion

        #region CallBack API
        /// <summary>
        /// Класс для подключения CallBack API. Рекомендуется использовать только в приложениях, способных принимать и отправлять http и/или https запросы
        /// </summary>
        public class CallBack
        {

            /// <summary>
            /// Обработчик событий для CallBack API
            /// Обрабатывает 3 типа запросов и событие:
            /// confirmation - Возвращает тип и строку для ответа серверу
            /// new_donate - Возвращает тип, строку для ответа серверу и массив VKKeksikLib.DonateAnswer.Donate в виде объекта для дальнейшего использования
            /// payment_status - Возвращает тип, строку для ответа серверу и массив VKKeksikLib.DonateAnswer.Payment в виде объекта для дальнейшего использования
            /// error - Возвращает тип, строку для ответа серверу и причину ошибки в виде String
            /// 
            /// Подробнее о типах запросов можно узнать на GitHub проекта и keksik.io/api#callback
            /// </summary>
            /// <param name="type">Тип ответа/запроса</param>
            /// <param name="answer">Строка для ответа серверу</param>
            /// <param name="obj">Объект, приклепленный к событию</param>
            public delegate void Handler(string type, string answer, object obj = null);
            /// <summary>
            /// Выполняется при входящем запросе подтверждения
            /// </summary>
            public event Handler OnNewConfirmation;
            /// <summary>
            /// Вызывается при входящем донате
            /// </summary>
            public event Handler OnNewDonate;
            /// <summary>
            /// Вызывается при входящем выводе средств
            /// </summary>
            public event Handler OnNewPaymentStatus;
            /// <summary>
            /// Вызывается при возникновении внутренней ошибки в работе CallBack API
            /// </summary>
            public event Handler OnError;

            /// <summary>
            /// Инициализация CallBack API
            /// </summary>
            /// <param name="GroupID">VKID группы</param>
            /// <param name="APIToken">Токен приложения</param>
            /// <param name="SecretKey">Секретный ключ из настроек приложения</param>
            /// <param name="ConfirmKey">Код подтверждения (Пример: a1b2c3)</param>
            public CallBack(int GroupID, string APIToken, string SecretKey, string ConfirmKey)
            {
                _SecretKey = SecretKey;
                _ConfirmKey = ConfirmKey;
                _GroupID = GroupID;
                _APIToken = APIToken;
                // For API UPDATED
                _APIVersion = 1;
            }

            /// <summary>
            /// Принимает и обрабатывает входящий массив от CallBack API
            /// </summary>
            /// <param name="json">Массив для обработки в виде строки</param>
            public void Input(string json)
            {
                VKKeksikLib.DonateAnswer.Da DA = VKKeksikLib.DonateAnswer.Da.FromJson(json);
                if (CheckRequest(json, DA, _SecretKey))
                {
                    //DA API version "1"
                    if (DA.Type == "confirmation")
                    {
                        try
                        {
                            OnNewConfirmation?.Invoke("confirmation", VKKeksikLib.Converters.Serialize.ToJson(new ConfirmationJSON { Code = _ConfirmKey }));
                        }
                        catch (Exception ex)
                        {
                            OnError?.Invoke("error", VKKeksikLib.Converters.Serialize.ToJson(new ConfirmJSON { Status = "error" }), $"Ошибка обработки confirmation: {ex.Message}");
                        }
                    }
                    else if (DA.Type == "new_donate")
                    {
                        try
                        {
                            OnNewDonate?.Invoke("new_donate", VKKeksikLib.Converters.Serialize.ToJson(new ConfirmJSON { Status = "ok" }), DA.Donate);
                        }
                        catch (Exception ex)
                        {
                            OnError?.Invoke("error", VKKeksikLib.Converters.Serialize.ToJson(new ConfirmJSON { Status = "error" }), $"Ошибка обработки new_donate: {ex.Message}");
                        }
                    }
                    else if (DA.Type == "payment_status")
                    {
                        try
                        {
                            OnNewPaymentStatus?.Invoke("payment_status", VKKeksikLib.Converters.Serialize.ToJson(new ConfirmJSON { Status = "ok" }), DA.Payment);
                        }
                        catch (Exception ex)
                        {
                            OnError?.Invoke("error", VKKeksikLib.Converters.Serialize.ToJson(new ConfirmJSON { Status = "error" }), $"Ошибка обработки payment_status: {ex.Message}");
                        }
                    }
                }
                else
                {
                    OnError?.Invoke("error", VKKeksikLib.Converters.Serialize.ToJson(new ConfirmJSON { Status = "error" }), "Запрос не прошел проверку");
                }
            }

            /// <summary>
            /// Конвертация boolean для проверки на подлинность
            /// </summary>
            /// <param name="bl">Переменная boolean для конвертации в string</param>
            /// <returns></returns>
            private string BoolToStringFC(bool bl)
            {
                if (bl) return "1";
                else return "";
            }

            /// <summary>
            /// Проверка запроса на подлинность
            /// </summary>
            /// <param name="json">Исходный массив</param>
            /// <param name="DA">Конвертированный массив</param>
            /// <param name="ke">Секретный ключ</param>
            /// <returns></returns>
            private bool CheckRequest(string json, VKKeksikLib.DonateAnswer.Da DA, string ke)
            {
                var jObj = (JObject)JsonConvert.DeserializeObject(json);
                Dictionary<string, object> r1 = ToDictionary(jObj);
                Dictionary<string, string> r2 = new Dictionary<string, string>();
                Convert(r1);

                void Convert(Dictionary<string, object> @object, string prevKey = "")
                {
                    if (!String.IsNullOrWhiteSpace(prevKey))
                    {
                        prevKey += "/";
                    }
                    foreach (KeyValuePair<string, object> r in @object)
                    {
                        string key = r.Key;
                        dynamic value = r.Value;
                        

                        if (value.GetType() == typeof(Dictionary<string, object>))
                        {
                            Convert(value, key);
                        }
                        else
                        {
                            if (value.GetType() == typeof(bool) || value.GetType() == typeof(bool?) || value.GetType() == typeof(Boolean))
                            {
                                r2.Add(prevKey + $"{key}", BoolToStringFC(System.Convert.ToBoolean(value)));
                            }
                            else if (key != "hash") r2.Add(prevKey + $"{key}", value.ToString());
                        }
                    }
                }

                Dictionary<string, object> ToDictionary(JObject @object)
                {
                    var result = @object.ToObject<Dictionary<string, object>>();

                    var JObjectKeys = (from r in result
                                       let key = r.Key
                                       let value = r.Value
                                       where value.GetType() == typeof(JObject)
                                       select key).ToList();

                    var JArrayKeys = (from r in result
                                      let key = r.Key
                                      let value = r.Value
                                      where value.GetType() == typeof(JArray)
                                      select key).ToList();

                    JArrayKeys.ForEach(key => result[key] = ((JArray)result[key]).Values().Select(x => ((JValue)x).Value).ToArray());
                    JObjectKeys.ForEach(key => result[key] = ToDictionary(result[key] as JObject));

                    return result;
                }

                List<string> keys = new List<string>();
                foreach (string k in r2.Keys) keys.Add(k);
                IEnumerable<string> auto = keys.OrderByDescending(s => s);
                string[] s1 = new string[auto.Count()];
                int i = auto.Count() - 1;
                foreach (string st in auto)
                {
                    s1[i] = st;
                    i--;
                }
                string s2 = null;
                foreach (string st in s1)
                {
                    if (String.IsNullOrWhiteSpace(s2)) s2 = r2[st];
                    else s2 += $",{r2[st]}";
                }
                s2 += $",{ke}";
                string hash = VKKeksikLib.Converters.CustomConverters.ComputeSha256Hash(s2);
                if (DA.Hash == hash) return true;
                else
                    return false;
            }
        }
        #endregion

        #region Main API
        /// <summary>
        /// API для работы с донатами
        /// </summary>
        public class Donate
        {
            /// <summary>
            /// Набор функций для работы с донатами
            /// </summary>
            /// <param name="GroupID">ID вашей группы</param>
            /// <param name="SecretKey">Ваш секретный ключ в приложении</param>
            /// <param name="ConfirmKey">Ваш код подтверждения в приложении</param>
            public Donate(int GroupID, string SecretKey, string ConfirmKey)
            {
                _SecretKey = SecretKey;
                _ConfirmKey = ConfirmKey;
                _GroupID = GroupID;
                // For API UPDATED
                _APIVersion = 1;
            }
            /// <summary>
            /// Получение списка донатов
            /// </summary>
            /// <param name="len">Количество донатов в списке. Максимум 100. По умолчанию 20.</param>
            /// <param name="offset">Смещение по выборе донатов</param>
            /// <param name="start_date">Временная метка по UNIX (в миллисекундах). Задает минимальную дату и время выбираемых донатов.</param>
            /// <param name="end_date">Временная метка по UNIX (в миллисекундах). Задает максимальную дату и время выбираемых донатов.</param>
            /// <param name="sort">Метод сортировки. По умолчанию date. Возможные значения: date - сортировка по дате; amount - сортировка по сумме.</param>
            /// <param name="reverse">Направление сортировки. По умолчанию false. Возможные значения: false - сортировка по убыванию; true - сортировка по возрастанию.</param>
            /// <returns></returns>
            public VKKeksikLib.Donates.Get.Response.JSON Get(int len = 20, int offset = 0, int start_date = 0, int end_date = 0, string sort = "date", bool reverse = false)
            {
                VKKeksikLib.Donates.Get.Request.JSON JSON = new VKKeksikLib.Donates.Get.Request.JSON { Group = _GroupID, Token = _APIToken, Version = _APIVersion };
                JSON.Len = len;
                JSON.Offset = offset;
                JSON.StartDate = start_date;
                if (end_date != 0) JSON.EndDate = end_date;
                else JSON.EndDate = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
                JSON.Sort = sort;
                JSON.Reverse = reverse;

                string ResponseCache = SendPostJSON("https://api.keksik.io/donates/get", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.Donates.Get.Response.JSON.FromJson(ResponseCache);
            }

            /// <summary>
            /// Изменить статус доната
            /// </summary>
            /// <param name="id">ID доната в системе.</param>
            /// <param name="status">Статус доната. Возможные значения: public - опубликован; hidden - скрыт.</param>
            /// <returns></returns>
            public VKKeksikLib.Response.Default.JSON ChangeStatus(int id, string status)
            {
                VKKeksikLib.Donates.ChangeStatus.Request.JSON JSON = new Donates.ChangeStatus.Request.JSON { Group = _GroupID, Token = _APIToken, Version = _APIVersion };
                JSON.ID = id;
                JSON.Status = status;

                string ResponseCache = SendPostJSON("https://api.keksik.io/donates/change-status", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.Response.Default.JSON.FromJson(ResponseCache);
            }

            /// <summary>
            /// Добавить/изменить ответ сообщества на донат
            /// </summary>
            /// <param name="id">ID доната в системе.</param>
            /// <param name="answer">Текст ответа. Для удаления ответа следует передать пустую строку.</param>
            /// <returns></returns>
            public VKKeksikLib.Response.Default.JSON Answer(int id, string answer)
            {
                VKKeksikLib.Donates.Answer.Request.JSON JSON = new Donates.Answer.Request.JSON { Group = _GroupID, Token = _APIToken, Version = _APIVersion };
                JSON.ID = id;
                JSON.Answer = answer;

                string ResponseCache = SendPostJSON("https://api.keksik.io/donates/answer", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.Response.Default.JSON.FromJson(ResponseCache);
            }

            /// <summary>
            /// Изменить выдачи вознаграждения.
            /// </summary>
            /// <param name="id">ID доната в системе.</param>
            /// <param name="status">Статус выдачи вознаграждения. not_sended - не вадно; sended - выдано.</param>
            /// <returns></returns>
            public VKKeksikLib.Response.Default.JSON ChangeRewardStatus(int id, string status)
            {
                VKKeksikLib.Donates.ChangeRewardStatus.Request.JSON JSON = new Donates.ChangeRewardStatus.Request.JSON { Group = _GroupID, Token = _APIToken, Version = _APIVersion };
                JSON.ID = id;
                JSON.Status = status;

                string ResponseCache = SendPostJSON("https://api.keksik.io/donates/change-reward-status", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.Response.Default.JSON.FromJson(ResponseCache);
            }
        }
        /// <summary>
        /// API для работы с краудфандинговыми кампаниями
        /// </summary>
        public class Campaign
        {
            /// <summary>
            /// Набор функций для работы с краудфандинговыми кампаниями
            /// </summary>
            /// <param name="GroupID">ID вашей группы</param>
            /// <param name="SecretKey">Ваш секретный ключ в приложении</param>
            /// <param name="ConfirmKey">Ваш код подтверждения в приложении</param>
            public Campaign(int GroupID, string SecretKey, string ConfirmKey)
            {
                _SecretKey = SecretKey;
                _ConfirmKey = ConfirmKey;
                _GroupID = GroupID;
                // For API UPDATED
                _APIVersion = 1;
            }

            /// <summary>
            /// Получить список краудфандинговых кампаний (последние 20 кампаний).
            /// </summary>
            /// <param name="IDs">Можно передать массив системных ID кампаний для выборки конкрентных кампаний. Если данный параметр не передан, то вернутся 20 последних кампаний.</param>
            /// <returns></returns>
            public VKKeksikLib.Campaigns.Get.Response.JSON Get(int[] IDs = null)
            {
                VKKeksikLib.Request.IDS.JSON JSON = new Request.IDS.JSON { Group = _GroupID, Token = _SecretKey, Version = _APIVersion , IDs = IDs };

                string ResponseCache = SendPostJSON("https://api.keksik.io/campaigns/get", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.Campaigns.Get.Response.JSON.FromJson(ResponseCache);
            }

            /// <summary>
            /// Получить активную краудфандинговую кампанию.
            /// </summary>
            /// <returns></returns>
            public VKKeksikLib.Campaigns.GetActive.Response.JSON GetActive()
            {
                VKKeksikLib.Request.Default.JSON JSON = new Request.Default.JSON { Group = _GroupID, Token = _SecretKey, Version = _APIVersion };

                string ResponseCache = SendPostJSON("https://api.keksik.io/campaigns/get-active", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.Campaigns.GetActive.Response.JSON.FromJson(ResponseCache);
            }

            /// <summary>
            /// Получить список вознаграждений краудфандинговой кампании.
            /// </summary>
            /// <param name="campaign">ID кампании в системе.</param>
            /// <returns></returns>
            public VKKeksikLib.Campaigns.GetRewards.Response.JSON GetRewards(int campaign)
            {
                VKKeksikLib.Campaigns.GetRewards.Request.JSON JSON = new Campaigns.GetRewards.Request.JSON { Group = _GroupID, Token = _SecretKey, Version = _APIVersion, Campaign = campaign };

                string ResponseCache = SendPostJSON("https://api.keksik.io/campaigns/get-active", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.Campaigns.GetRewards.Response.JSON.FromJson(ResponseCache);
            }

            /// <summary>
            /// Обновить информацию о краудфандинговой кампании.
            /// </summary>
            /// <param name="ID">ID кампании в системе.</param>
            /// <param name="title">Заголовок кампании.</param>
            /// <param name="status">Статус кампании. draft - черновик; active - активная кампания; archive - кампания архивирована.</param>
            /// <param name="end">Временная метка по unix (в миллисекундах) окончания кампании.</param>
            /// <param name="point">Цель по сбору в рублях.</param>
            /// <param name="StartReceived">Собрано за пределами приложения в рублях.</param>
            /// <param name="StartBackers">Кол-во спонсоров пожертвовавших за пределами приложения.</param>
            /// <returns></returns>
            public VKKeksikLib.Response.Default.JSON Change(int ID, string title = null, string status = null, long end = 0, int point = 1000, int StartReceived = 0, int StartBackers = 0)
            {
                VKKeksikLib.Campaigns.Change.Request.JSON JSON = new Campaigns.Change.Request.JSON { Group = _GroupID, Token = _SecretKey, Version = _APIVersion, ID = ID, Title = title, Status = status, End = end, Point = point, StartReceived = StartReceived, StartBackers = StartBackers };

                string ResponseCache = SendPostJSON("https://api.keksik.io/campaigns/change", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.Response.Default.JSON.FromJson(ResponseCache);
            }

            /// <summary>
            /// Обновить информацию о вознаграждении краудфандинговой кампании.
            /// </summary>
            /// <param name="ID">ID вознаграждения в системе.</param>
            /// <param name="Title">Название вознаграждения.</param>
            /// <param name="Desc">Описание вознаграждения.</param>
            /// <param name="MinDonate">Минимальный донат для получения текущего вознаграждения.</param>
            /// <param name="Limits">Ограничение кол-во вознаграждений. Если ограничений нет, данное поле должно быть равно 0.</param>
            /// <param name="Status">Статус вознаграждения. public - вознаграждение опубликовано; hidden - вознаграждение скрыто.</param>
            /// <returns></returns>
            public VKKeksikLib.Response.Default.JSON ChangeReward(int ID, string Title = null, string Desc = null, int MinDonate = 100, int Limits = 0, string Status = "hidden")
            {
                VKKeksikLib.Campaigns.ChangeReward.Request.JSON JSON = new Campaigns.ChangeReward.Request.JSON { Group = _GroupID, Token = _SecretKey, Version = _APIVersion, ID = ID, Title = Title, Desc = Desc, MinDonate = MinDonate, Limits = Limits, Status = Status };

                string ResponseCache = SendPostJSON("https://api.keksik.io/campaigns/change-reward", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.Response.Default.JSON.FromJson(ResponseCache);
            }
        }
        /// <summary>
        /// API для работы с выплатами
        /// </summary>
        public class Payment
        {
            /// <summary>
            /// Набор функций для работы с выплатами
            /// </summary>
            /// <param name="GroupID">ID вашей группы</param>
            /// <param name="SecretKey">Ваш секретный ключ в приложении</param>
            /// <param name="ConfirmKey">Ваш код подтверждения в приложении</param>
            public Payment(int GroupID, string SecretKey, string ConfirmKey)
            {
                _SecretKey = SecretKey;
                _ConfirmKey = ConfirmKey;
                _GroupID = GroupID;
                // For API UPDATED
                _APIVersion = 1;
            }

            /// <summary>
            /// Получить список заявок на выплату (последние 20 заявок).
            /// </summary>
            /// <param name="IDs">Можно передать массив системных ID заявок на выплату для выборки конкрентных заявок на выплату. Если данный параметр не передан, то вернутся 20 последних заявок на выплату.</param>
            /// <returns></returns>
            public VKKeksikLib.Payments.Get.Response.JSON Get(int[] IDs = null)
            {
                VKKeksikLib.Request.IDS.JSON JSON = new Request.IDS.JSON { Group = _GroupID, Token = _SecretKey, Version = _APIVersion, IDs = IDs };

                string ResponseCache = SendPostJSON("https://api.keksik.io/payments/get", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.Payments.Get.Response.JSON.FromJson(ResponseCache);
            }

            /// <summary>
            /// Создать заявку на выплату.
            /// </summary>
            /// <param name="System">Платежная система. bank - Банковская карта; qiwi - Qiwi; webmoney - WebMoney; yandex_money - Яндекс.Деньги; mobile - Счет мобильного телефона.</param>
            /// <param name="Purse">Счет в платежной системе на который будет произведена выплата.</param>
            /// <param name="Ammount">Сумма выплаты в рублях.</param>
            /// <returns></returns>
            public VKKeksikLib.Payments.Create.Response.JSON Create(string System, string Purse, float Ammount)
            {
                VKKeksikLib.Payments.Create.Request.JSON JSON = new Payments.Create.Request.JSON { Group = _GroupID, Token = _SecretKey, Version = _APIVersion, System = System, Purse = Purse, Amount = Ammount };

                string ResponseCache = SendPostJSON("https://api.keksik.io/payments/create", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.Payments.Create.Response.JSON.FromJson(ResponseCache);
            }
        }
        /// <summary>
        /// API для работы с балансом
        /// </summary>
        public class Balance
        {
            /// <summary>
            /// Набор функций для работы с балансом
            /// </summary>
            /// <param name="GroupID">ID вашей группы</param>
            /// <param name="SecretKey">Ваш секретный ключ в приложении</param>
            /// <param name="ConfirmKey">Ваш код подтверждения в приложении</param>
            public Balance(int GroupID, string SecretKey, string ConfirmKey)
            {
                _SecretKey = SecretKey;
                _ConfirmKey = ConfirmKey;
                _GroupID = GroupID;
                // For API UPDATED
                _APIVersion = 1;
            }

            /// <summary>
            /// Получить баланс группы в приложении.
            /// </summary>
            /// <returns></returns>
            public VKKeksikLib.BalanceJSON.Get.Response.JSON Get()
            {
                VKKeksikLib.Request.Default.JSON JSON = new Request.Default.JSON { Group = _GroupID, Token = _SecretKey, Version = _APIVersion };

                string ResponseCache = SendPostJSON("https://api.keksik.io/balance", VKKeksikLib.Converters.Serialize.ToJson(JSON));
                return VKKeksikLib.BalanceJSON.Get.Response.JSON.FromJson(ResponseCache);
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Отправляет JSON массив через POST запрос на указанный адрес
        /// </summary>
        /// <param name="uri">Ссылка на сервер</param>
        /// <param name="json">Массив в виде строки</param>
        /// <returns></returns>
        public static string SendPostJSON(string uri, string json)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }
        /// <summary>
        /// Возвращает описание ошибки по ее коду
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetErrorCodeInfo(int code)
        {
            switch(code)
            {
                case 1000:
                    return "Неизвестный метод.";

                case 1001:
                    return "Не переданы обязательные параметры.";

                case 1002:
                    return "Переданы некорректные значения для некоторых параметров.";

                case 1004:
                    return "Ошибка авторизации. Проверьте правильность параметров \'group\' и \'token\'.";

                case 1005:
                    return "Версия API устарела.";

                case 1006:
                    return "API сервис временно не доступен.";

                case 2000:
                    return "Превышен лимит обращений к API.";

                case 3000:
                    return "В данный момент нет активной кампании.";

                case 3001:
                    return "Кампания с таким ID не найдена.";

                case 3002:
                    return "Заявка на вывод с таким ID не найдена.";

                case 3003:
                    return "Донат с таким ID не найден.";

                case 3004:
                    return "Запрашиваемая сумма выплаты превышает остаток средств на балансе.";

                case 3005:
                    return "Запрашиваемая сумма выплаты ниже минимальной суммы выплаты для данной платежной системы.";

                case 3006:
                    return "Ошибка списания средств. Повторите запрос.";

                case 3007:
                    return "Создание выпдат через API отключено в настройках приложения.";

                case 3008:
                    return "Время окончания указано неправильно. Кампания не может оканчиваться менее чем через три часа от текущего момента.";

                default:
                    return $"Ошибка {code} отсутствует в базе библиотеки";
            }
        }

        #endregion
    }

    #region CallBack response JSON
    /// <summary>
    /// Масив, возвращаемый для CallBack API
    /// </summary>
    public class ConfirmationJSON
    {
        /// <summary>
        /// Код подтверждения (если нужно)
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }
    }
    /// <summary>
    /// Массив для обычного подтверждения запроса
    /// </summary>
    public class ConfirmJSON
    {
        /// <summary>
        /// Состояние обработки запроса
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
    #endregion
}
