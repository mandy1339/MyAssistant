using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;
using MyAssistant.Utils;
using System.Data;
using MyAssistant.Models;
using System.Configuration;

namespace WebJobDueDateAlert
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    internal class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage

        /// <summary>
        /// This web job is to be invoked via web hook (url)
        /// If this job is invoked between 9 am and 10 am, it will send alerts for items due today
        /// If this job is invoked between 11 am and 1 pm, it will send alerts for items due next day
        /// </summary>
        static void Main()
        {
            /// BEGIN MICROSOFT BOILER PLATE 
            //var config = new JobHostConfiguration();

            //if (config.IsDevelopment)
            //{
            //    config.UseDevelopmentSettings();
            //}

            //var host = new JobHost(config);
            //// The following code will invoke a function called ManualTrigger and 
            //// pass in data (value in this case) to the function
            //host.Call(typeof(Functions).GetMethod("ManualTrigger"), new { value = 20 });
            /// END MICROSOFT BOILER PLATE 

            // Get the current time
            DateTime now = DateTime.Now;

            // if it's within 1 hour of 9 am execute logic for same day alert
           // if (now.Hour > 8 && now.Hour < 10)
                SendAlertsForItemsDueSameDay();

            // if it's within 1 hour of 12:00 pm execute logic for next day alert
           // else if (now.Hour > 8 && now.Hour < 10)
                SendAlertsForItemsDueNextDay();
        }


        static void SendAlertsForItemsDueSameDay()
        {
            // Get list of key value pairs (k=> item, v=> list of users linked to the item)
            string sql =
                "SELECT  Description, DueDate, Priority, GROUP_CONCAT(UserID SEPARATOR ' ') AS IDs " +
                "FROM  " +
                "( " +
                "	SELECT i.Description, i.DueDate, i.Priority, COALESCE(i.UserID, x.UserID) AS UserID " +
                "	FROM " +
                "		TodoItem i 			LEFT JOIN  " +
                "		`UserGroupXREF` x 	ON i.GroupID = x.GroupID AND i.UserID IS NULL " +
                "	WHERE  " +
                "		i.IsComplete <> 1 AND i.DueDate IS NOT NULL AND i.DueDate = CURRENT_DATE() " +
                ") AS MyTable   " +
                "GROUP BY Description, DueDate, Priority ";
            string dbConnStr = ConfigurationManager.ConnectionStrings["MyAssistantDB"].ConnectionString; // cannot access web.config of the web app, so access our app.config
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(sql, dbConnStr);
            IterateThroughResultSetAndSendEmailToUsers(RS, enum_DayOfAlert.TODAY);
        }

        static void SendAlertsForItemsDueNextDay()
        {
            // Get list of key value pairs (k=> item, v=> list of users linked to the item)
            string sql =
                "SELECT  Description, DueDate, Priority, GROUP_CONCAT(UserID SEPARATOR ' ') AS IDs " +
                "FROM  " +
                "( " +
                "	SELECT i.Description, i.DueDate, i.Priority, COALESCE(i.UserID, x.UserID) AS UserID " +
                "	FROM " +
                "		TodoItem i 			LEFT JOIN  " +
                "		`UserGroupXREF` x 	ON i.GroupID = x.GroupID AND i.UserID IS NULL " +
                "	WHERE  " +
                "		i.IsComplete <> 1 AND i.DueDate IS NOT NULL AND i.DueDate = DATE_ADD(CURRENT_DATE(), INTERVAL 1 DAY) " +
                ") AS MyTable   " +
                "GROUP BY Description, DueDate, Priority ";
            string dbConnStr = ConfigurationManager.ConnectionStrings["MyAssistantDB"].ConnectionString; // cannot access web.config of the web app, so access our app.config
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(sql, dbConnStr);
            IterateThroughResultSetAndSendEmailToUsers(RS, enum_DayOfAlert.TOMORROW);
        }

        private static void IterateThroughResultSetAndSendEmailToUsers(DataTable RS, enum_DayOfAlert dayOfAlert)
        {
            List<(TodoItem, List<int>)> itemList = new List<(TodoItem, List<int>)>();
            for (int i = 0; i < RS.Rows.Count; i++)
            {
                TodoItem todoItem = new TodoItem()
                {
                    Description = RS.Rows[i]["Description"].ToString(),
                    DueDate = (DateTime?)RS.Rows[i]["DueDate"],
                    Priority = (Byte)RS.Rows[i]["Priority"],
                };
                List<int> idList = new List<int>();
                foreach (string idStr in RS.Rows[i]["IDs"].ToString().Split(' '))
                    idList.Add(int.Parse(idStr));
                itemList.Add((todoItem, idList));
            }
            foreach ((TodoItem, List<int>) item in itemList)
            {
                // call the SendAlert function for each user
                foreach (int userID in item.Item2)
                    MyEmailHandler.SendAlertAboutItemToUserWithID(item.Item1, userID, dayOfAlert);
            }
        }
    }
}
