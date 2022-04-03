using MyAssistant.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MyAssistant.Models
{
    public static class TodoItems
    {

        /// <summary>
        /// GET ALL ITEMS
        /// </summary>
        /// <returns>IEnumerable Of All Items</returns>
        public static IEnumerable<TodoItem> GetTodoItems()
        {
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(
                "SELECT " +
                    "PKey, " +
                    "Description, " +
                    "CreatedDate, " +
                    "DueDate, " +
                    "Category, " +
                    "IsComplete, " +
                    "Priority " +
                "FROM " +
                    "TodoItem " +
                "WHERE " +
                    "IsComplete <> 1 OR DateCompleted >= CURRENT_DATE() " +
                "ORDER BY " +
                    "IsComplete ASC, " +
                    "Priority ASC, " +
                    "PKey DESC ");

            List<TodoItem> result = new List<TodoItem>();
            for (int i = 0; i < RS.Rows.Count; i++)
            {
                int rowPKey = (int)RS.Rows[i]["PKey"];
                TodoItem newItem = new TodoItem()
                {
                    PKey = rowPKey,
                    Description = RS.Rows[i]["Description"].ToString(),
                    CreatedDate = (DateTime)RS.Rows[i]["CreatedDate"],
                    DueDate = (DateTime?) (RS.Rows[i]["DueDate"] == DBNull.Value ? null : RS.Rows[i]["DueDate"]),
                    Category = RS.Rows[i]["Category"].ToString()[0],
                    IsComplete = Convert.ToBoolean((ulong)RS.Rows[i]["IsComplete"]),
                    Priority = (Byte)RS.Rows[i]["Priority"],
                };
                result.Add(newItem);
            }
            return result as IEnumerable<TodoItem>;
        }




        /// <summary>
        /// GET ONLY PERSONAL ITEMS
        /// </summary>
        /// <returns>IEnumerable Of All Items</returns>
        public static IEnumerable<TodoItem> GetPersonalTodoItems()
        {
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(
                "SELECT " +
                    "PKey, " +
                    "Description, " +
                    "CreatedDate, " +
                    "DueDate, " +
                    "Category, " +
                    "IsComplete, " +
                    "Priority " +
                "FROM " +
                    "TodoItem " +
                "WHERE " +
                    "(IsComplete <> 1 OR DateCompleted >= CURRENT_DATE()) AND Category = 'P' " +
                "ORDER BY " +
                    "IsComplete ASC, " +
                    "Priority ASC, " +
                    "PKey DESC ");

            List<TodoItem> result = new List<TodoItem>();
            for (int i = 0; i < RS.Rows.Count; i++)
            {
                int rowPKey = (int)RS.Rows[i]["PKey"];
                TodoItem newItem = new TodoItem()
                {
                    PKey = rowPKey,
                    Description = RS.Rows[i]["Description"].ToString(),
                    CreatedDate = (DateTime)RS.Rows[i]["CreatedDate"],
                    DueDate = (DateTime?)(RS.Rows[i]["DueDate"] == DBNull.Value ? null : RS.Rows[i]["DueDate"]),
                    Category = RS.Rows[i]["Category"].ToString()[0],
                    IsComplete = Convert.ToBoolean((ulong)RS.Rows[i]["IsComplete"]),
                    Priority = (Byte)RS.Rows[i]["Priority"],
                };
                result.Add(newItem);
            }
            return result as IEnumerable<TodoItem>;
        }






        /// <summary>
        /// GET ONLY SCHOOL ITEMS
        /// </summary>
        /// <returns>IEnumerable Of All Items</returns>
        public static IEnumerable<TodoItem> GetSchoolTodoItems()
        {
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(
                "SELECT " +
                    "PKey, " +
                    "Description, " +
                    "CreatedDate, " +
                    "DueDate, " +
                    "Category, " +
                    "IsComplete, " +
                    "Priority " +
                "FROM " +
                    "TodoItem " +
                "WHERE " +
                    "(IsComplete <> 1 OR DateCompleted >= CURRENT_DATE()) AND Category = 'S' " +
                "ORDER BY " +
                    "IsComplete ASC, " +
                    "Priority ASC, " +
                    "PKey DESC ");

            List<TodoItem> result = new List<TodoItem>();
            for (int i = 0; i < RS.Rows.Count; i++)
            {
                int rowPKey = (int)RS.Rows[i]["PKey"];
                TodoItem newItem = new TodoItem()
                {
                    PKey = rowPKey,
                    Description = RS.Rows[i]["Description"].ToString(),
                    CreatedDate = (DateTime)RS.Rows[i]["CreatedDate"],
                    DueDate = (DateTime?)(RS.Rows[i]["DueDate"] == DBNull.Value ? null : RS.Rows[i]["DueDate"]),
                    Category = RS.Rows[i]["Category"].ToString()[0],
                    IsComplete = Convert.ToBoolean((ulong)RS.Rows[i]["IsComplete"]),
                    Priority = (Byte)RS.Rows[i]["Priority"],
                };
                result.Add(newItem);
            }
            return result as IEnumerable<TodoItem>;
        }





        /// <summary>
        /// GET ONLY WORK ITEMS
        /// </summary>
        /// <returns>IEnumerable Of All Items</returns>
        public static IEnumerable<TodoItem> GetWorkTodoItems()
        {
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(
                "SELECT " +
                    "PKey, " +
                    "Description, " +
                    "CreatedDate, " +
                    "DueDate, " +
                    "Category, " +
                    "IsComplete, " +
                    "Priority " +
                "FROM " +
                    "TodoItem " +
                "WHERE " +
                    "(IsComplete <> 1 OR DateCompleted >= CURRENT_DATE()) AND Category = 'W' " +
                "ORDER BY " +
                    "IsComplete ASC, " +
                    "Priority ASC, " +
                    "PKey DESC ");

            List<TodoItem> result = new List<TodoItem>();
            for (int i = 0; i < RS.Rows.Count; i++)
            {
                int rowPKey = (int)RS.Rows[i]["PKey"];
                TodoItem newItem = new TodoItem()
                {
                    PKey = rowPKey,
                    Description = RS.Rows[i]["Description"].ToString(),
                    CreatedDate = (DateTime)RS.Rows[i]["CreatedDate"],
                    DueDate = (DateTime?)(RS.Rows[i]["DueDate"] == DBNull.Value ? null : RS.Rows[i]["DueDate"]),
                    Category = RS.Rows[i]["Category"].ToString()[0],
                    IsComplete = Convert.ToBoolean((ulong)RS.Rows[i]["IsComplete"]),
                    Priority = (Byte)RS.Rows[i]["Priority"],
                };
                result.Add(newItem);
            }
            return result as IEnumerable<TodoItem>;
        }






        /// <summary>
        /// ADD NEW ITEM
        /// </summary>
        /// <param name="description"></param>
        /// <param name="dueDate"></param>
        /// <param name="category"></param>
        /// <param name="isComplete"></param>
        /// <returns>ID Of Newly Inserted Item</returns>
        public static int AddTodoItem(string description, DateTime? dueDate, char? category, bool isComplete, int priority)
        {
            string procSQL = "spr_AddItem";
            int IDOut = 0;
            MySqlConnection con = DBUtilsMySQL.GetConnection();
            MySqlCommand cmd = new MySqlCommand(procSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Description", MySqlDbType.VarChar).Value = description;
            cmd.Parameters["Description"].MySqlDbType = MySqlDbType.VarChar;
            if (dueDate != null)
                cmd.Parameters.AddWithValue("DueDate", MySqlDbType.Date).Value = dueDate;
            else
                cmd.Parameters.AddWithValue("DueDate", MySqlDbType.Date).Value = DBNull.Value;
            cmd.Parameters["DueDate"].MySqlDbType = MySqlDbType.Date;
            cmd.Parameters.AddWithValue("Category", MySqlDbType.VarChar).Value = category.ToString();
            cmd.Parameters["Category"].MySqlDbType = MySqlDbType.VarChar;
            cmd.Parameters.AddWithValue("IsComplete", MySqlDbType.Bit).Value = isComplete == true ? 1 : 0;
            cmd.Parameters["IsComplete"].MySqlDbType = MySqlDbType.Bit;
            cmd.Parameters.AddWithValue("Priority", MySqlDbType.UInt16).Value = (Byte)priority;
            cmd.Parameters.Add("PKey", MySqlDbType.Int32);     
            cmd.Parameters["PKey"].Direction = ParameterDirection.Output;

            DBUtilsMySQL.ExecuteStoredProcedure(cmd);

            IDOut = (int)cmd.Parameters["PKey"].Value;
            return IDOut;
        }

        /// <summary>
        /// DELETE ONE ITEM
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteTodoItem(int id)
        {
            string sql = "spr_RemoveItem";
            MySqlConnection con = DBUtilsMySQL.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ID", MySqlDbType.Int32).Value = id;
            DBUtilsMySQL.ExecuteStoredProcedure(cmd);
        }




        /// <summary>
        /// TOGGLE CHECKBOX
        /// Checking box sets the DateDone value and the IsDone value
        /// Unchecking box clears DateDone and IsDone
        /// </summary>
        /// <param name="id"></param>
        public static void ToggleCheckBox(int id)
        {
            string sql = "spr_ToggleTodoItem";
            MySqlConnection con = DBUtilsMySQL.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ID", SqlDbType.Int).Value = id;
            DBUtilsMySQL.ExecuteStoredProcedure(cmd);
        }
    }
}