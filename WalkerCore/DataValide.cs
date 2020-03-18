using System;

namespace WalkerCore
{
    public class DataValide
    {

        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <returns></returns>
        public static bool EmailValidate(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch 
            {
                return false;
            }
        }
        /// <summary>
        /// 随机生成手机号码
        /// </summary>
        /// <returns></returns>
        public static string GetRandomPhoneNum()
        {
            String[] Top3 = {"133", "149", "153", "173", "177",
                "180", "181", "189", "199", "130", "131", "132",
                "145", "155", "156", "166", "171", "175", "176", "185", "186", "166", "134", "135",
                "136", "137", "138", "139", "147", "150", "151", "152", "157", "158", "159", "172",
                "178", "182", "183", "184", "187", "188", "198", "170", "171"};
            //随机出真实号段   使用数组的length属性，获得数组长度，
            //通过Math.random（）*数组长度获得数组下标，从而随机出前三位的号段
            var rng = new Random();
            String firstNum = Top3[rng.Next(Top3.Length)];
            //随机出剩下的8位数
            String lastNum = "";
            for (int i = 0; i < 8; i++)
            {
                //每次循环都从0~9挑选一个随机数
                lastNum += rng.Next(10);
            }
            //最终将号段和尾数连接起来
            return firstNum + lastNum;
        }

        /// <summary>
        /// 获取随机日期-指定开始结束日期
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static DateTime GetRandomDate(DateTime startDate, DateTime endDate)
        {
            Random rng = new Random();
            if (startDate == null)
            {
                startDate = new DateTime(1995, 1, 1);
            }
            if (endDate == null)
                endDate = DateTime.Today;
            int range = (endDate - startDate).Days;
            return startDate.AddDays(rng.Next(range));
        }
    }
}
