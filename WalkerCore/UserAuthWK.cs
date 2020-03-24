using System;
using System.Collections.Generic;
using System.Text;
using WebCore.Data;

namespace WalkerCore
{
    public class UserAuthWK
    {

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="mo">授权用户信息</param>
        /// <returns></returns>
        public static string TokenMake(CoreUser mo)
        {
            var key = GlobalTo.GetValue("VerifyCode:Key");

            var token = Arithmetic.EnDES(new
            {
                mo = new
                {
                    mo.UserId,
                    mo.UserEmail,
                    mo.UserSign,
                },
                expired = DateTime.Now.AddDays(10).ToTimestamp()
            }.ToJson(), key);

            return token;
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static CoreUser TokenValid(string token)
        {
            CoreUser mo = null;

            try
            {
                var key = GlobalTo.GetValue("VerifyCode:Key");

                var jo = Arithmetic.DeDES(token, key).ToJObject();

                if (DateTime.Now.ToTimestamp() < long.Parse(jo["expired"].ToString()))
                {
                    mo = jo["mo"].ToString().ToEntity<CoreUser>();
                }
            }
            catch (Exception)
            {

            }

            return mo;
        }
    }
}
