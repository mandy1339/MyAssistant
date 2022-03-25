using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssistant.Utils
{
    public class DBUtils
    {
        public static SqlConnection GetConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyAssistantDB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            return conn;
        }



        public static DataTable Get1RSFromSqlString(string sqlString)
        {
            SqlConnection conn = GetConnection();
            DataTable rs = new DataTable();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlString, conn);
            da.Fill(rs);
            conn.Close();
            return rs;
        }




        public static bool DoesFieldContainData(string fieldName, string tableName, string schemaName)
        {
            SqlConnection conn = GetConnection();
            DataTable rs = new DataTable();
            string sql = $"SELECT TOP 1 \"{fieldName}\" AS data FROM \"{schemaName}\".\"{tableName}\" WHERE \"{fieldName}\" IS NOT NULL";

            rs = Get1RSFromSqlString(sql);
            int rsCount = rs.Rows.Count;
            return (rsCount > 0 ? true : false);
        }



        public static void TestConnectionString(string connString)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            conn.Close();
        }




        public static void ExecuteStoredProcedure(SqlCommand cmd)
        {
            SqlConnection con = DBUtils.GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;
            
            con.Open();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        
    }
}

