using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WebCore.Data;

namespace WalkerCore
{
    public class UserAuthWK
    {
        private readonly HttpContext context;

        /// <summary>
        /// 构造，拿到当前上下文
        /// </summary>
        /// <param name="httpContext"></param>
        public UserAuthWK(HttpContext httpContext)
        {
            context = httpContext;
        }

        /// <summary>
        /// 获取授权用户
        /// </summary>
        /// <returns></returns>
        public CoreUser Get()
        {
            var user = context.User;

            if (user.Identity.IsAuthenticated)
            {
                return new CoreUser
                {
                    UserId = Convert.ToInt32(user.FindFirst(ClaimTypes.PrimarySid)?.Value),
                    UserEmail = user.FindFirst(ClaimTypes.Name)?.Value,
                    UserSign = user.FindFirst(ClaimTypes.Sid)?.Value,
                };
            }
            else
            {
                var token = context.Request.Query["token"].ToString();
                var mo = TokenValid(token);
                if (mo == null)
                {
                    mo = new CoreUser();
                }
                return mo;
            }
        }

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
