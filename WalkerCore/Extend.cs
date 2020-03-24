using System;
using System.Collections.Generic;
using System.Text;

namespace WalkerCore
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class Extend
    {

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
