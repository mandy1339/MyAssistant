using MySql.Data.MySqlClient;
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
    public class DBUtilsMySQL
    {
        public static MySqlConnection GetConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyAssistantDB"].ConnectionString;
            //SqlConnection conn = new SqlConnection(connStr);
            //return conn;
            MySqlConnection conn = new MySqlConnection(connStr);
            return conn;
        }



        public static DataTable Get1RSFromSqlString(string sqlString, string passedInConnString = null)
        {
            MySqlConnection conn;
            if (passedInConnString != null)
                conn = new MySqlConnection(passedInConnString);
            else
                conn = GetConnection();
            DataTable rs = new DataTable();
            conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter(sqlString, conn);
            MySqlCommand cmd = new MySqlCommand(sqlString, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            //da.Fill(rs);
            rs.Load(reader);
            conn.Close();
            return rs;
        }




        public static bool DoesFieldContainData(string fieldName, string tableName, string schemaName = null)
        {
            MySqlConnection conn = GetConnection();
            DataTable rs = new DataTable();
            string sql = $"SELECT TOP 1 \"{fieldName}\" AS data FROM \"{tableName}\" WHERE \"{fieldName}\" IS NOT NULL";
            rs = Get1RSFromSqlString(sql);
            int rsCount = rs.Rows.Count;
            return (rsCount > 0 ? true : false);
        }



        public static void TestConnectionString(string connString)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            conn.Open();
            conn.Close();
        }




        public static void ExecuteStoredProcedure(MySqlCommand cmd)
        {
            MySqlConnection con = DBUtilsMySQL.GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }


    }
}

