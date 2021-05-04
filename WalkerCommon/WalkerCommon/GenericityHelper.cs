using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkerCommon
{
    public class GenericityHelper
    {

        /// <summary>
        /// 将datable转换为list
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnName">指定列</param>
        /// <returns></returns>
        public static List<string> TableToList(DataTable table, string columnName) 
        {
            var list = new List<string>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(row[columnName].ToString());
            }

            return list;
        }
    }
}
