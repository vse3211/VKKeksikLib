namespace VKKeksikLib.Converters
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Функции сериализации массивов
    /// </summary>
    public static class Serialize
    {
        /// <summary>
        /// Конвертация в JSON
        /// </summary>
        /// <param name="self">Объект массива</param>
        /// <returns></returns>
        public static string ToJson(object self) => JsonConvert.SerializeObject(self, VKKeksikLib.Converters.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    /// <summary>
    /// Конвертеры
    /// </summary>
    public static class CustomConverters
    {
        /// <summary>
        /// SHA256 Converter
        /// </summary>
        /// <param name="rawData">Enter string</param>
        /// <returns></returns>
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
