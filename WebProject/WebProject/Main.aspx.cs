using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblUserID.Text = Context.User.Identity.Name + " 님 환영합니다";
            if (Session["userGrade"].ToString() == "점장" || Session["userGrade"].ToString() == "사장")
            {

            }
            else
            {
                Response.Redirect("~/Board.aspx");
            }
        }
    }
}