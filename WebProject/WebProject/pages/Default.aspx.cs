using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection con =null;
        /// <summary>
        /// 페이지 로드 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            //사용자 인증 여부를 가져옴
            if (Context.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SetAuthCookie(Request.Params["userId"], false);
            }
            else
            {
                
            }
        }
        /// <summary>
        /// 로그인 버튼 클릭시
        /// 1. Text에 있는 정보를 가져와 DB에연결.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userId = Request.Params["userId"]; //유저 아이디 저장 
            string storePw = Request.Params["userPw"];

            con = DBController.Instance();


            using (var cmd = new SqlCommand("FirstLoginSelect", con))
            {  
                con.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", int.Parse(userId));
                cmd.Parameters.AddWithValue("@StorePW", int.Parse(storePw));

                var type = cmd.ExecuteScalar();
                //type이 0 이면 없음;
                if (type.ToString() == "0")
                {
                    Response.Write("<script>alert('정보가 맞지 않습니다');</script>");
                }
                //type이 0 이아니면 로그인
                else
                {
                    using (var cmd2 = new SqlCommand("SelectGrade", con))
                    {
            
                        cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@EmployeeID", userId);
                        var sdr = cmd2.ExecuteReader();
                        while (sdr.Read())
                        {
                            Session["userName"] = sdr["이름"].ToString();
                            Session["userGrade"] = sdr["등급"].ToString();
                        }
                    }
                     FormsAuthentication.RedirectFromLoginPage(userId, true);
                    con.Close();

                }
            }
            con.Close();
        }
    }
}