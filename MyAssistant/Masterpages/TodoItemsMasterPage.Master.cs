using MyAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyAssistant.Masterpages
{
    public partial class TodoItemsMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //#if !DEBUG
            if (HttpContext.Current.Request.Url.AbsolutePath.ToUpper().Contains("LOGIN") != true) // if this is not the login page
            {
                if (Session["isLoggedIn"] == null) // if you are not logged in
                    Response.Redirect("LoginWithMasterPage.aspx"); // redirect to login page
            }            
            //#endif
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {

            }
            if (Session["user"] != null)
                Lbl_UserName.Text = $"User: {(Session["user"] as User).UserName}";
        }
    }
}