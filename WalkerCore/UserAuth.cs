using System;
using System.Collections.Generic;
using System.Text;


namespace WalkerCore
{
    //class UserAuth
    //{

    //    /// <summary>
    //    /// 生成Token
    //    /// </summary>
    //    /// <param name="mo">授权用户信息</param>
    //    /// <returns></returns>
    //    public static string TokenMake(CoreUser mo)
    //    {
    //        var key = GlobalTo.GetValue("VerifyCode:Key");

    //        var token = Core.CalcTo.EnDES(new
    //        {
    //            mo = new
    //            {
    //                mo.UserId,
    //                mo.UserName,
    //                mo.Nickname,
    //                mo.UserSign,
    //                mo.UserPhoto
    //            },
    //            expired = DateTime.Now.AddDays(10).ToTimestamp()
    //        }.ToJson(), key);

    //        return token;
    //    }

    //    /// <summary>
    //    /// 验证Token
    //    /// </summary>
    //    /// <param name="token"></param>
    //    /// <returns></returns>
    //    public static Domain.UserInfo TokenValid(string token)
    //    {
    //        Domain.UserInfo mo = null;

    //        try
    //        {
    //            var key = GlobalTo.GetValue("VerifyCode:Key");

    //            var jo = Core.CalcTo.DeDES(token, key).ToJObject();

    //            if (DateTime.Now.ToTimestamp() < long.Parse(jo["expired"].ToString()))
    //            {
    //                mo = jo["mo"].ToString().ToEntity<Domain.UserInfo>();
    //            }
    //        }
    //        catch (Exception)
    //        {

    //        }

    //        return mo;
    //    }
    //}
}
