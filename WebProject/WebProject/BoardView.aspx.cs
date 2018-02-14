using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PosBord
{
    public partial class BoardView : System.Web.UI.Page
    {
        HttpCookieCollection Cookies = new HttpCookieCollection();
        int count;
        string Num;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Request["Num"]))
            {
                Response.Redirect("BoardList.aspx");
            }
            else
            {

                if (!IsPostBack)
                {
                    SaveCookie(Request["Num"]);
                    ReadBoard();
                    ReadRely();
                    
                }              

            }
        }
        private void ReadBoard()
        {            
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["gposStr"].ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("BoardView", con))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Num", Request["Num"]);

                    var sr = cmd.ExecuteReader();
                    while (sr.Read())
                    {
                        lblNickName.Text = sr["NickName"].ToString();
                        lblTitle.Text = sr["Title"].ToString();                        
                        lblDate.Text = sr["WriteDate"].ToString();
                        lblCount.Text = sr["Count"].ToString();
                        lblLike.Text = sr["Recommand"].ToString();
                        if (sr.IsDBNull(10))
                        {
                            lblContent.Text = sr["Content"].ToString();
                        }
                        else
                        {
                            lblContent.Text = "<img src='upload/" + sr["Img"].ToString() + "'><br/>";
                            lblContent.Text += sr["Content"].ToString();
                        }
                        ViewState["Ref"] = sr["Ref"].ToString();
                        Session["Step"] = sr["Step"].ToString();
                        ViewState["RefOrder"] = sr["RefOrder"].ToString();
                    }
                }
                con.Close();
            }
        }
        private void ReadRely()
        {
            
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["gposStr"].ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("ReplyView",con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ref", Request["num"]);

                    var da = new SqlDataAdapter();
                    da.SelectCommand = cmd;

                    var ds = new DataSet();
                    da.Fill(ds);                    
                    
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
        }
        protected void btnReply_Click(object sender, EventArgs e)
        {
            string strUrl = String.Format("BoardWrite.aspx?Num={0}&Ref={1}&Step={2}&RefOrder={3}", Request["Num"], ViewState["Ref"], Session["Step"], ViewState["RefOrder"]);
            Response.Redirect(strUrl);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("BoardList.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnWriter_Click(object sender, EventArgs e)
        {
            
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["gposStr"].ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("ReplyWrite",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nickname","김덕준");
                    cmd.Parameters.AddWithValue("@Content",tboxReply.Text);
                    cmd.Parameters.AddWithValue("@Ref",Request["Num"]);
                    cmd.Parameters.AddWithValue("@RefOrder", RefOrder());                   
                    var sr = cmd.ExecuteScalar();
                    con.Close();
                    Response.Redirect("BoardView.aspx?Num="+Request["Num"]);
                }
            }
        }
        private string RefOrder()
        {
            string RefOrder ="";
            
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["gposStr"].ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("ReplyCheck", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                    
                    cmd.Parameters.AddWithValue("@Ref", Request["Num"]);                    
                    
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        RefOrder= dr["RefOrder"].ToString();
                    }                   
                    
                    con.Close();
                    return RefOrder;
                }
            }
        }
        [WebMethod]
        public void SaveCookie(string Num)
        {
            Response.Write(Session.Count);
            if (Session.IsNewSession)
                {
                
                    Session.Add("글번호", Request["Num"]);
                    Response.Write("방금 생김"+ Session.SessionID.ToString());
                
                }
                else
                {                    
                    Response.Write("아까 생김"+ Request["Num"]); 
                } 

        }       
        

        protected void btnLike_Click(object sender, EventArgs e)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["gposStr"].ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("BoardLike", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Num", Request["Num"]);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("BoardView.aspx?Num=" + Request["Num"]);
                }                
            }
        }
        protected void ReplyReply_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnReplyRWrite_Click(object sender, EventArgs e)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["gposStr"].ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("ReplyReplyWrite", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    
                }
            }
            Response.Write(GridView1.SelectedIndex.ToString());
            
        }
        [WebMethod]
        public static string ReplyR(string ReplyNum)
        {
            
            return ReplyNum;
        }
    }
}