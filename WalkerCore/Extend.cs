using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;


namespace WalkerCore
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class Extend
    {
        /// <summary>
        /// object 转 JSON 字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="DateTimeFormat">时间格式化</param>
        /// <returns></returns>
        public static string ToJson(this object obj, string DateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            Newtonsoft.Json.Converters.IsoDateTimeConverter dtFmt = new Newtonsoft.Json.Converters.IsoDateTimeConverter
            {
                DateTimeFormat = DateTimeFormat
            };
            return JsonConvert.SerializeObject(obj, dtFmt);
        }
        /// <summary>
        /// 解析 JSON字符串 为JObject对象
        /// </summary>
        /// <param name="json">JSON字符串</param>
        /// <returns>JObject对象</returns>
        public static JObject ToJObject(this string json)
        {
            return JObject.Parse(json);
        }
        /// <summary>
        /// 将Datetime转换成时间戳，10位：秒 或 13位：毫秒
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="isms">毫秒，默认false为秒，设为true，返回13位，毫秒</param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime datetime, bool isms = false)
        {
            var t = datetime.ToUniversalTime().Ticks - 621355968000000000;
            var tc = t / (isms ? 10000 : 10000000);
            return tc;
        }
    }
}
