using MyAssistant.Utils;
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
            DataTable RS = DBUtils.Get1RSFromSqlString(
                "SELECT " +
                    "PKey, " +
                    "Description, " +
                    "CreatedDate, " +
                    "DueDate, " +
                    "Category, " +
                    "IsComplete, " +
                    "Priority " +
                "FROM " +
                    "dbo.TodoItem " +
                "WHERE " +
                    "IsComplete <> 1 OR DateCompleted >= CAST(GETDATE() AS DATE) " +
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
                    IsComplete = (bool)RS.Rows[i]["IsComplete"],
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
            SqlConnection con = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(procSQL, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Description", SqlDbType.VarChar).Value = description;
            if (dueDate != null)
                cmd.Parameters.AddWithValue("@DueDate", SqlDbType.Date).Value = dueDate;
            cmd.Parameters.AddWithValue("@Category", SqlDbType.Char).Value = category.ToString();
            cmd.Parameters.AddWithValue("@IsComplete", SqlDbType.Bit).Value = isComplete;
            cmd.Parameters.AddWithValue("@Priority", SqlDbType.TinyInt).Value = (Byte)priority;
            cmd.Parameters.Add("@PKey", SqlDbType.Int);
            cmd.Parameters["@PKey"].Direction = ParameterDirection.Output;

            DBUtils.ExecuteStoredProcedure(cmd);

            IDOut = (int)cmd.Parameters["@PKey"].Value;
            return IDOut;
        }


        public static void DeleteTodoItem(int id)
        {
            string sql = "spr_RemoveItem";
            SqlConnection con = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = id;
            DBUtils.ExecuteStoredProcedure(cmd);
        }


        public static void ToggleCheckBox(int id)
        {
            string sql = "spr_ToggleTodoItem";
            SqlConnection con = DBUtils.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = id;
            DBUtils.ExecuteStoredProcedure(cmd);
        }
    }
}