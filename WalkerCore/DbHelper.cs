using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WalkerCore
{
    public class DbHelper
    {

        public string connStr { get; set; }


        public object ExecuteScalar(string sql, SqlParameter[] sqlParameters = null)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (sqlParameters != null)
                    {
                        cmd.Parameters.AddRange(sqlParameters);
                    }
                    var obj = cmd.ExecuteScalar();

                    return obj;
                }
            }
        }
        /// <summary>
        /// 获取datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetTable(string sql, SqlParameter[] sqlParameters = null)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (sqlParameters != null)
                    {
                        cmd.Parameters.AddRange(sqlParameters);
                    }
                    var table = new DataTable();
                    var sqlAda = new SqlDataAdapter(cmd);
                    sqlAda.Fill(table);
                    return table;
                }
            }
        }
        public List<Dictionary<string, string>> GetRowList(string sql,out List<string> columnNames , SqlParameter[] sqlParameters = null)
        {
            var rowList = new List<Dictionary<string,string>>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (sqlParameters != null)
                    {
                        cmd.Parameters.AddRange(sqlParameters);
                    }
                    var dr = cmd.ExecuteReader();
                    var fieldCount = dr.FieldCount;

                    var colSchemas = dr.GetColumnSchema();
                    columnNames = new List<string>();

                    foreach (var col in colSchemas)
                    {
                        columnNames.Add(col.ColumnName);
                    }
                    while (dr.Read())
                    {
                        var listTemp = new Dictionary<string,string>();
                        for (var i = 0; i < fieldCount; i++)
                        {
                            listTemp.Add(columnNames[i], dr[i].ToString());
                        }
                        rowList.Add(listTemp);
                    }
                    dr.Close();
                }
                return rowList;
            }
        }
    }
}
