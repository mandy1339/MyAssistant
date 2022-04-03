using MyAssistant.Controllers;
using MyAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyAssistant
{
    public partial class LoginWithMasterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLoggedIn"] != null)
            {
                Response.Redirect("TodoDefault.aspx", false);
            }
        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            User u = LoginController.GetUser(Txb_UserName.Text, Txb_Password.Text);
            if (u == null)
            {
                Lbl_Result.Text = "Could Not Login";
                return;
            }
            Session["isLoggedIn"] = true;
            Session["user"] = u;
            Response.Redirect("TodoDefault.aspx", false);
        }
    }
}
