using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using WebCore.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
namespace WalkerCore
{
    public class UserAuthAid
    {
        private readonly HttpContext context;

        /// <summary>
        /// 构造，拿到当前上下文
        /// </summary>
        /// <param name="httpContext"></param>
        public UserAuthAid(HttpContext httpContext)
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
                    UserName = user.FindFirst(ClaimTypes.Name)?.Value,
                    UserEmail = user.FindFirst(ClaimTypes.Email)?.Value,
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
        /// 写入授权
        /// </summary>
        /// <param name="context">当前上下文</param>
        /// <param name="user">用户信息</param>
        /// <param name="isremember">是否记住账号</param>
        public static void SetAuth(HttpContext context, CoreUser user, bool isremember = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.PrimarySid, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName??""),
                new Claim(ClaimTypes.Email, user.UserEmail),
                //new Claim(ClaimTypes.GivenName, user.Nickname ?? ""),
                new Claim(ClaimTypes.Sid, user.UserSign),
                new Claim(ClaimTypes.UserData, user.UserPhoto ?? ""),
            };

            var cp = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            var authProperties = new AuthenticationProperties();
            if (isremember)
            {
                //记住我
                authProperties.IsPersistent = true;
                authProperties.ExpiresUtc = DateTimeOffset.Now.AddDays(10);
            }
            //写入授权
            context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cp, authProperties).Wait();
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
