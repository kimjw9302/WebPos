using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PosBord
{
    public partial class BoardWrite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["gposStr"].ConnectionString))
            {
                
                con.Open();
                string fn = "NULL";
                if (ImgFile.HasFile)
                {
                    fn = ImgFile.FileName;
                    ImgFile.SaveAs(Server.MapPath(".") + @"\Upload\" + fn);
                }
                if (Request["Num"] == null)
                {
                    using (var cmd = new SqlCommand("BoardWrite", con))
                    {
                        
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Title", tboxTitle.Text);
                        cmd.Parameters.AddWithValue("@Content", tboxTitle.Text);
                        cmd.Parameters.AddWithValue("@NickName", "김덕준");
                        cmd.Parameters.AddWithValue("@Ref", Ref());
                        cmd.Parameters.AddWithValue("@Img", fn);
                        cmd.ExecuteScalar();

                    } 
                }
                else
                {
                    using (var cmd = new SqlCommand("BoardReplyWrite", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Title", tboxTitle.Text);
                        cmd.Parameters.AddWithValue("@Content", tboxTitle.Text);
                        cmd.Parameters.AddWithValue("@NickName", "김덕준");
                        cmd.Parameters.AddWithValue("@Ref", Request["Ref"]);
                        cmd.Parameters.AddWithValue("@Step", Request["Step"]);
                        cmd.Parameters.AddWithValue("@RefOrder", Request["RefOrder"]);
                        cmd.Parameters.AddWithValue("@Img", fn);
                        cmd.ExecuteScalar();
                    }    
                }
                
                Response.Write("<script>alert('글 저장 성공!'); location.href='BoardList.aspx'</script>");
                con.Close();

            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            if (Request["Num"] == null)
            {
                Response.Redirect("BoardList.aspx");
            }
            else
            {
                Response.Redirect("BoardView.aspx?Num=" + Request["Num"]);
            }
            
        }
        private string Ref()
        {
            string strRef = string.Empty;            
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["gposStr"].ConnectionString))
            {
                using (var cmd = new SqlCommand("MaxRef", con))
                {
                    con.Open();
                    var sr = cmd.ExecuteReader();
                    if (sr.Read())
                    {
                        strRef = sr.IsDBNull(0) ? "1" : Convert.ToString(Convert.ToInt32(sr[0]) + 1);
                    }
                    con.Close();
                    return strRef;
                }
            }
        }
        //private void string  
    }
}