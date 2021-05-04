using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalkerCommon
{

    public class SqlHelper
    {
        public static string connStr { get; set; }


        /// <summary>
        /// 获取datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetTable(string sql)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;

                    var table = new DataTable();
                    var sqlAda = new SqlDataAdapter(cmd);
                    sqlAda.Fill(table);
                    return table;
                }
            }
        }
    }
}
