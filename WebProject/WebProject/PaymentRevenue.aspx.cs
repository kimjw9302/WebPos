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
    public partial class PaymentRevenue : System.Web.UI.Page
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
            con = DBController.Instance();
            DataTable sTable = new DataTable();
            sTable.Columns.Add("바코드");
            sTable.Columns.Add("상품명");
            sTable.Columns.Add("단가");
            sTable.Columns.Add("원가");
            sTable.Columns.Add("실재고", typeof(int));
            sTable.Columns.Add("현재고", typeof(int));
            sTable.Columns.Add("대분류");
            sTable.Columns.Add("소분류");
            con.Open();
            using (var cmd = new SqlCommand("ProductAllView", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var sdr = cmd.ExecuteReader())
                {

                    while (sdr.Read())
                    {
                        DataRow newRow = sTable.NewRow();
                        newRow["바코드"] = sdr["barcode"].ToString();
                        newRow["상품명"] = sdr["productName"].ToString();
                        newRow["단가"] = sdr["unitPrice"].ToString();
                        newRow["원가"] = sdr["costPrice"].ToString();
                        newRow["대분류"] = sdr["cate1"].ToString();
                        newRow["소분류"] = sdr["categoryName"].ToString();
                        sTable.Rows.Add(newRow);
                    }
                }
            }
            con.Close();

            //foreach (DataRow row in sTable.Rows)
            //{
            //    con.Open();
            //    string barcode = row["바코드"].ToString();
            //    using (var cmd = new SqlCommand("productRevenue", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@barcode", barcode);
            //        cmd.Parameters.AddWithValue("@productName", );
            //        cmd.Parameters.AddWithValue("@unitPrice", );
            //        var sdr = cmd.ExecuteReader();
            //        while (sdr.Read())
            //        {
            //            row["실재고"] = sdr[0].ToString();
            //            row["현재고"] = sdr[1].ToString();
            //        }
            //    }
            //    con.Close();
            //}

            stockGridview.DataSource = sTable;
            stockGridview.DataBind();
   
        }

        protected void stockGridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            stockGridview.PageIndex = e.NewPageIndex;
            makeGridView();
        }
    }
}