using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class ProductsRevenue : System.Web.UI.Page
    {
        SqlConnection con;
        DataTable pTable = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblUserID.Text = Session["userName"].ToString() + " 님 환영합니다";
            if (!IsPostBack)
            {
                this.modeTxt.Text = "관리자 모드로 홈페이지를 이용중입니다.";

            }
            else
            {


            }
        }
        protected void btn_Logout_Click(object sender, EventArgs e)
        {
            Session["userGrade"] = null;
            Session["userName"] = null;
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();


        }

        private void makeGridView()
        {
            if (!CheckDate())
            {

            }
            else { 
            con = DBController.Instance();
            pTable = new DataTable();
            pTable.Columns.Add("순위");
            pTable.Columns.Add("바코드");
            pTable.Columns.Add("상품명");
            pTable.Columns.Add("판매된개수", typeof(int));
            pTable.Columns.Add("거래처");
            con.Open();
            using (var cmd = new SqlCommand("SelectAllBarcode", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var sdr = cmd.ExecuteReader())
                {

                    while (sdr.Read())
                    {
                        DataRow newRow = pTable.NewRow();
                        newRow["바코드"] = sdr["barcode"].ToString();
                        newRow["상품명"] = sdr["productName"].ToString();
                        newRow["거래처"] = sdr["placeName"].ToString();
                        pTable.Rows.Add(newRow);
                    }
                }
            }         
            con.Close();
            foreach (DataRow row in pTable.Rows)
            {
                con.Open();
                string barcode = row["바코드"].ToString();
                using (var cmd = new SqlCommand("productRevenue", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@barcode", barcode);
                    cmd.Parameters.AddWithValue("@sdate", DateTime.Parse(DateChange(DateSample.Text) + " 00:00:00"));
                    cmd.Parameters.AddWithValue("@edate", DateTime.Parse(DateChange(DateSample2.Text) + " 23:59:59"));
                    var sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        row["판매된개수"] = sdr[0].ToString();
                    }
                }
                con.Close();
            }
            pTable = pTable.Select("", "판매된개수 DESC").CopyToDataTable();
            int count = 0;
            foreach (DataRow row in pTable.Rows)
            {
                row["순위"] = count + 1;
                count++;
            }
                productGrid.DataSource = pTable;
                productGrid.DataBind();

                //ClientTable();
            }
        }
        //public void ClientTable()
        //{
        //    string table = "$(document).ready(function(){";
       
        //    table += "$('#tablediv').html(\"";
        //  //  table += "< table style = 'width: 100 % ' class='table table-striped table - bordered table - hover' id='dataTables - example'>";

        //    table += "<table style='width:100%' class='table table-striped table-bordered table-hover' id='dataTables-example'>";
        //    table += "<thead><tr>";
        //    foreach (DataColumn column in pTable.Columns)
        //    {
        //        table += "<th>" + column.ColumnName + "</th>";
        //    }
        //    table += "</tr></thead>";
        //    table += "<tbody>";

        //    foreach (DataRow row in pTable.Rows)
        //    {
        //        table += "<tr class='odd grade'X>";
        //        for (int i = 0; i < row.ItemArray.Length; i++)
        //        {
        //            table += "<td>" + row.ItemArray[i].ToString() + "</td>";
        //        }
        //        table += "</tr>";
        //    }

        //    table += "</tbody></table>";
        //    table += "\");});";
        //    table = table.Replace("\r\n", "");
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "tablediv3", table, true);

        //    if (ClientScript.IsClientScriptBlockRegistered(this.GetType(), "tablediv3"))
        //    {

        //        string script = "$(document).ready(function(){";
        //        script += "$.getScript('../vendor/datatables/js/jquery.dataTables.min.js',function() { alert('이벤트 처리를 합시다1');});";

        //        script += "$.getScript('../vendor/datatables-responsive/dataTables.responsive.js',function() { alert('이벤트 처리를 합시다3');});";
        //        //script += "<script src='../vendor/datatables /js /jquery.dataTables.min.js'></script>";
        //        //script += "<script src='../vendor/datatables-plugins/dataTables.bootstrap.min.js'></script>";
        //        //script += "<script src='../vendor/datatables-responsive/dataTables.responsive.js'></script>";
        //        script += "});";
        //        string script2 = "$(document).ready(function(){";
        //        script2 += "$.getScript('../vendor/datatables-plugins/dataTables.bootstrap.min.js',function() { alert('이벤트 처리를 합시다2');});";
        //        script2 += "});";
        //        string script3 = " $(document).ready(function() {$('#dataTables-example').DataTable({  responsive: true   }); });";
        //        ClientScript.RegisterClientScriptBlock(this.GetType(), "tablediv4", script, true);
        //        //ClientScript.RegisterStartupScript(this.GetType(), "tablediv5", script2, true);
        //        ClientScript.RegisterStartupScript(this.GetType(), "tablediv6", script3, true);
        //    }
        //    else
        //    {
        //    }

        //}
        public string DateChange(string date)
        {
            string first = date.Substring(0, 5);
            string end = date.Substring(6,4);
            return end+"/"+first;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<style>#h1txt" +
                "{" +
                "display:none;" +
                "}</style>");
            makeGridView();
        }

        public bool CheckDate()
        {

            if ((DateSample.Text=="") || (DateSample2.Text==""))
            {
                DateSample.Text = "";
                DateSample2.Text = "";
                Response.Write("<script>alert('날짜를 선택해주세요.')</script>");
                return false;
            }

            TimeSpan ts = DateTime.Parse(DateChange(DateSample.Text)) - DateTime.Parse(DateChange(DateSample2.Text));
            if (ts.TotalSeconds < 0)
            {
                return true;
            }
            else
            {
                DateSample.Text = "";
                DateSample2.Text = "";
                Response.Write("<script>alert('날짜를 제대로 선택해주세요.')</script>");
                return false;
            }
        }

        protected void productGrid_PageIndexChanged(object sender, EventArgs e)
        {
         
        }

        protected void productGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newPageNumber = e.NewPageIndex + 1;
        }
    }
}