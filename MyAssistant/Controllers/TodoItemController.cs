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
    public static class TodoItemController
    {
        /// <summary>
        /// GET A SINGLE TODO ITEM WITH GIVEN ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns null if not found. else returns the TodoItem</returns>
        public static TodoItem GetTodoItemByID(int id)
        {
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(
                "SELECT " +
                    "PKey, " +
                    "Description, " +
                    "CreatedDate, " +
                    "DueDate, " +
                    "Category, " +
                    "IsComplete, " +
                    "Priority, " +
                    "UserID, " +
                    "GroupID " +
                "FROM " +
                    "TodoItem " +
                $"WHERE PKey = {id}");
            if (RS.Rows.Count == 0)
                return null;
            else
                return new TodoItem() {
                    PKey = (int)RS.Rows[0]["PKey"],
                    Description = RS.Rows[0]["Description"].ToString(),
                    CreatedDate = (DateTime)RS.Rows[0]["CreatedDate"],
                    DueDate = (DateTime?)(RS.Rows[0]["DueDate"] == DBNull.Value ? null : RS.Rows[0]["DueDate"]),
                    Category = RS.Rows[0]["Category"].ToString()[0],
                    IsComplete = Convert.ToBoolean((ulong)RS.Rows[0]["IsComplete"]),
                    Priority = (Byte)RS.Rows[0]["Priority"],
                    UserID = (int?)(RS.Rows[0]["UserID"] == DBNull.Value ? null : RS.Rows[0]["UserID"]),
                    GroupID = (int?)(RS.Rows[0]["GroupID"] == DBNull.Value ? null : RS.Rows[0]["GroupID"])
                };
        }



        /// <summary>
        /// GET ALL ITEMS
        /// </summary>
        /// <returns>IEnumerable Of All Items</returns>
        public static IEnumerable<TodoItem> GetTodoItems(int userID)
        {
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(
                "SELECT " +
                    "PKey, " +
                    "Description, " +
                    "CreatedDate, " +
                    "DueDate, " +
                    "Category, " +
                    "IsComplete, " +
                    "Priority, " +
                    "GroupID " +
                "FROM " +
                    "TodoItem " +
                "WHERE " +
                    "(IsComplete <> 1 OR DateCompleted >= CURRENT_DATE() ) " +
                    $"AND `UserID` = {userID.ToString()} " +
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
                    GroupID = (int?)(RS.Rows[i]["GroupID"] == DBNull.Value ? null : RS.Rows[i]["GroupID"])
                };
                result.Add(newItem);
            }
            return result as IEnumerable<TodoItem>;
        }




        /// <summary>
        /// GET ONLY PERSONAL ITEMS
        /// </summary>
        /// <returns>IEnumerable Of All Items</returns>
        public static IEnumerable<TodoItem> GetPersonalTodoItems(int userID)
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
                     $"AND `UserID` = {userID.ToString()} " +
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
        public static IEnumerable<TodoItem> GetSchoolTodoItems(int userID)
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
                     $"AND `UserID` = {userID.ToString()} " +
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
        public static IEnumerable<TodoItem> GetWorkTodoItems(int userID)
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
                     $"AND `UserID` = {userID.ToString()} " +
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
        /// GET ALL ITEMS
        /// </summary>
        /// <returns>IEnumerable Of All Items</returns>
        public static IEnumerable<TodoItem> GetGroupItems(int userID)
        {
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(
                "SELECT " +
                    "i.PKey, " +
                    "i.Description, " +
                    "i.CreatedDate, " +
                    "i.DueDate, " +
                    "i.Category, " +
                    "i.IsComplete, " +
                    "i.Priority, " +
                    "i.GroupID " +
                "FROM " +
                    "`Group` g JOIN " +
                    $"`UserGroupXREF` x ON g.ID = x.GroupID AND x.`UserID` = {userID.ToString()} JOIN " +
                    "`TodoItem` i ON x.GroupID = i.GroupID " +
                "WHERE " +
                    "((IsComplete <> 1 OR DateCompleted >= CURRENT_DATE() ) " +
                    "AND i.`UserID` IS NULL ) " +
                    "OR (IsComplete = 1 AND DateCompleted >= CURRENT_DATE()) " +
                "ORDER BY " +
                    "i.IsComplete ASC, " +
                    "i.Priority ASC, " +
                    "i.PKey DESC ");

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
                    GroupID = (int)RS.Rows[i]["GroupID"]
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
        /// <param name="userID -> Pass Null If Creating Item For Group"></param>
        /// <param name="groupID -> Pass Null If Creating Item For User"></param>
        /// <returns>ID Of Newly Inserted Item</returns>
        public static int AddTodoItem(string description, DateTime? dueDate, char? category, bool isComplete, int priority, int? userID, int? groupID)
        {
            string procSQL = "spr_AddItem";
            int IDOut = 0;
            MySqlCommand cmd = new MySqlCommand(procSQL);
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
            if (userID.HasValue)
                cmd.Parameters.AddWithValue("UserID", MySqlDbType.Int32).Value = userID.Value;
            else
                cmd.Parameters.AddWithValue("UserID", MySqlDbType.Int32).Value = DBNull.Value;
            if (groupID.HasValue)
                cmd.Parameters.AddWithValue("GroupID", MySqlDbType.Int32).Value = groupID.Value;
            else
                cmd.Parameters.AddWithValue("GroupID", MySqlDbType.Int32).Value = DBNull.Value;

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
            MySqlCommand cmd = new MySqlCommand(sql);
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
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ID", SqlDbType.Int).Value = id;
            DBUtilsMySQL.ExecuteStoredProcedure(cmd);
        }




        /// <summary>
        /// Update the todo item and set the userid value = to the passed in userID
        /// </summary>
        /// <param name=""></param>
        public static void TakeOwnerShipOfTodoItem(int id, int userID)
        {
            string sql = "spr_TakeOwnershipOfItem";
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ID", SqlDbType.Int).Value = id;
            cmd.Parameters.AddWithValue("UserID", SqlDbType.Int).Value = userID;
            DBUtilsMySQL.ExecuteStoredProcedure(cmd);
        }



        /// <summary>
        /// Update the todo item and set the userid value to null
        /// </summary>
        /// <param name=""></param>
        public static void RemoveOwnerShipOfTodoItem(int id)
        {
            string sql = "spr_RemoveOwnershipOfItem";
            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ID", SqlDbType.Int).Value = id;
            DBUtilsMySQL.ExecuteStoredProcedure(cmd);
        }



    }
}