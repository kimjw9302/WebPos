using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PosBord
{
    public partial class Bord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReadData();
            }
            this.lblUserID.Text = Session["userName"].ToString() + " 님 환영합니다";
            if (Session["userGrade"].ToString() == "점장" || Session["userGrade"].ToString() == "사장")
            {

                this.modeTxt.Text = "관리자 모드로 홈페이지를 이용중입니다.";
            }
            else
            {
                Response.Redirect("~/BoardList.aspx");
            }

        }
        protected void btn_Logout_Click(object sender, EventArgs e)
        {
            Session["userGrade"] = null;
            Session["userName"] = null;
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();


        }

        private void ReadData()
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["gposStr"].ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("BoardList", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var da = new SqlDataAdapter();
                    da.SelectCommand = cmd;

                    var ds = new DataSet();
                    da.Fill(ds, "Board");
                    
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
        }
        protected string FuncStep(object step)
        {
            int intStep = Convert.ToInt32(step);
            string strResult = "";
            for (int i = 0; i < intStep; i++)
            {
                strResult += "&nbsp;&nbsp;"; // 들여쓰기
            }
            if (intStep > 0)
            {
                strResult += "<img src='images\\Reply.png' witdh=10px, height=10px>" + intStep;
            }
            return strResult;

        }
        protected string GetNew(object postdate)
        {
            DateTime wirteDate = Convert.ToDateTime(postdate);            
            TimeSpan ts = DateTime.Now - wirteDate;
            if (ts.TotalHours < 24)
            {
                return "<img src='images\\apple-icon-180x180.png' witdh 10px, height=10px>";
            }
            else
            {
                return "";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ReadData();
        }

        protected void btnWirte_Click(object sender, EventArgs e)
        {
            Response.Redirect("BoardWrite.aspx");
        }
    }
}