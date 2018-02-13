using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class ProductsRevenue : System.Web.UI.Page
    {
        SqlConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblUserID.Text = Context.User.Identity.Name + " 님 환영합니다";
            if (!IsPostBack)
            {
               

            }
            else
            {


            }
        }

        private void makeGridView()
        {
            if (!CheckDate())
            {

            }
            else { 
            con = DBController.Instance();
            DataTable pTable = new DataTable();
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
            Response.Write("<script>alert('"+ DateChange(DateSample.Text)+"')</script>");
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

            productsGridview.DataSource = pTable;
            productsGridview.DataBind();
            }
        }
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

        protected void productsGridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productsGridview.PageIndex = e.NewPageIndex;
            makeGridView();
        }
    }
}