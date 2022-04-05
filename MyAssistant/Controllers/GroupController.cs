using MyAssistant.Models;
using MyAssistant.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyAssistant.Controllers
{
    public static class GroupController
    {
        /// <summary>
        /// // perform SQL Statement to get group names and group ids for given user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static IEnumerable<Group> GetGroupsForUser(int userID)
        {
            List<Group> groups = new List<Group>();
            // perform SQL Statement to get group names and group ids for this user
            string sql = "SELECT g.`ID`, g.`GroupName` " +
                            "FROM " +
                                "`Group` g JOIN " +
                                "`UserGroupXREF` x ON g.ID = x.`GroupID` " +
                            $"WHERE x.`UserID` = {userID.ToString()} ";
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(sql);
            foreach (DataRow row in RS.Rows)
                groups.Add(new Group() { ID = (int)row["ID"], GroupName = row["GroupName"].ToString() });
            return groups;
        }
    }
}