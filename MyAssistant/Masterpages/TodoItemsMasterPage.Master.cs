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
                User user = null;
                if (Session["user"] != null) // If user is set, display the username in the page
                {
                    user = (Session["user"] as User);
                    Lbl_UserName.Text = $"User: {user.UserName}";
                }
                if (Session["isLoggedIn"] != null && user != null) // If use is logged and user is set, display the user's profile picture
                {
                    ImageProfile.ImageUrl = $"~/Images/{user.UserName.ToUpper()}.png";
                    ImageProfileTopRight.ImageUrl = $"~/Images/{user.UserName.ToUpper()}.png";
                }
            }
            
        }
    }
}