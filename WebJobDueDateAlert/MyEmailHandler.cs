using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using MyAssistant.Utils;
using System.Data;
using MyAssistant.Models;

namespace WebJobDueDateAlert
{
    enum enum_DayOfAlert
    {
        TODAY,
        TOMORROW
    }

    internal static class MyEmailHandler
    {
        // get properties from the app config to set up smtp
        static private readonly NetworkCredential EmailSmtpAccountCreds = new NetworkCredential(ConfigurationManager.AppSettings["SMTP_USER"], ConfigurationManager.AppSettings["SMTP_PW"]);
        static private readonly string EmailSmtpUser = ConfigurationManager.AppSettings["SMTP_USER"];
        static private readonly string EmailSmtpHost = ConfigurationManager.AppSettings["SMTP_HOST"];
        static private readonly int EmailSmtpPort = Int32.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
       
        // Send email and text if configured for the user
        // takes in a userID and the email to be sent
        internal static void SendAlertAboutItemToUserWithID(TodoItem item, int userID, enum_DayOfAlert dayOfAlert)
        {
            string userName = null;
            // Get Email of recipient
            string userEmail = null;
            // Get TextEmail of recipient
            string userTextEmail = null;

            string sqlGetUser = $"SELECT `UserName`, `PhoneNumber`, `Email` FROM `User` WHERE ID = {userID};";
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(sqlGetUser);
            if (RS.Rows[0]["UserName"] != DBNull.Value)
                userName = RS.Rows[0]["UserName"].ToString();
            if (RS.Rows[0]["PhoneNumber"] != DBNull.Value)
                userEmail = RS.Rows[0]["PhoneNumber"].ToString();
            if (RS.Rows[0]["Email"] != DBNull.Value)
                userTextEmail = RS.Rows[0]["Email"].ToString();

            // create email subject
            string emailSubject = $"TASK DUE ALERT :: P{item.Priority}";
            // create email text
            string emailBody = $"Dear {userName}\n The following task with priority P{item.Priority} " +
                $"is due {((dayOfAlert == enum_DayOfAlert.TODAY) ? "today" : "tomorrow")}: \n\n{item.Description}";

            // Send email
            if (userEmail != null)
            {
                var sender = new SmtpSender(() => new SmtpClient(EmailSmtpHost, EmailSmtpPort)
                {
                    Credentials = EmailSmtpAccountCreds,
                    EnableSsl = true,
                });
                Email.DefaultSender = sender;
                var email = Email
                    .From(EmailSmtpUser, "MyAssistant")
                    .To(userEmail)
                    .Subject(emailSubject)
                    .Body(emailBody)
                    .Send();
            }
            // Send Text
            if (userTextEmail != null)
            {
                var sender = new SmtpSender(() => new SmtpClient(EmailSmtpHost, EmailSmtpPort)
                {
                    Credentials = EmailSmtpAccountCreds,
                    EnableSsl = true,
                });
                Email.DefaultSender = sender;
                var email = Email
                    .From(EmailSmtpUser, "MyAssistant")
                    .To(userTextEmail, userName)
                    .Subject(emailSubject)
                    .Body(emailBody)
                    .Send();
            }
        }
    }
}
