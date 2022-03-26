using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAssistant.Models;
using MyAssistant.Utils;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace MyAssistant.Controllers
{
    public class LoginController
    {
        public static User GetUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName)|| string.IsNullOrEmpty(password))
                return null;

            SHA256 sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder();
            foreach (byte b in hashedBytes)
                sb.Append(b.ToString("x2"));
            string hashedPassword = sb.ToString();
            sha256.Clear();

            string sql = $"SELECT ID, UserName, Password, PhoneNumber, Email FROM User WHERE UserName = '{userName}' AND Password = '{hashedPassword}'";
            DataTable RS = DBUtilsMySQL.Get1RSFromSqlString(sql);
            if (RS.Rows.Count == 0)
                return null;
            User user = new User()
            {
                ID = (int)RS.Rows[0]["ID"],
                UserName = RS.Rows[0]["UserName"].ToString(),
                Password = RS.Rows[0]["Password"].ToString(),
                PhoneNumber = RS.Rows[0]["PhoneNumber"].ToString(),
                Email = RS.Rows[0]["Email"].ToString()
            };
            return user;
        }
    }
}