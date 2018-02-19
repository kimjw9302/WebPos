using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject
{
    public partial class timeRevenue : System.Web.UI.Page
    {
        private SqlDataAdapter adapter;
        private DataSet ds;
        DataTable dt;
        DataRowCollection dtRow;

        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!IsPostBack)
            {
                DateSample.Text = DateTime.Now.ToShortDateString();
                DateSample2.Text = DateTime.Now.ToShortDateString();
                makeTimeRevenue();
            } 
        }

        public void makeTimeRevenue()
        {
           
            DataTable table = new DataTable();
            table.Columns.Add("시간대");
            table.Columns.Add("건수");
            table.Columns.Add("총 금액");

            var con = DBController.Instance();
            using (var cmd = new SqlCommand("TimeRevenue", con))
            {

                int[] count = new int[24];
                decimal[] money = new decimal[24];

                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;

               
                cmd.Parameters.AddWithValue("@startDate", DateSample.Text + " 00:00:00");
                cmd.Parameters.AddWithValue("@endDate", DateSample2.Text + " 23:59:59");



                adapter = new SqlDataAdapter();
                ds = new DataSet();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                dt = ds.Tables[0];

                dtRow = dt.Rows;

                #region 시간대별로 건수, 금액 넣는 부분
                foreach (DataRow item in dtRow)
                {
                    string time = DateTime.Parse(item[0].ToString()).ToString("HH:mm:ss");
                    switch (time.Substring(0, 2))
                    {
                        case "00":
                            count[0] = int.Parse(item[1].ToString());
                            money[0] = decimal.Parse(item[2].ToString());
                            break;
                        case "01":
                            count[1] = int.Parse(item[1].ToString());
                            money[1] = decimal.Parse(item[2].ToString());
                            break;
                        case "02":
                            count[2] = int.Parse(item[1].ToString());
                            money[2] = decimal.Parse(item[2].ToString());
                            break;
                        case "03":
                            count[3] = int.Parse(item[1].ToString());
                            money[3] = decimal.Parse(item[2].ToString());
                            break;
                        case "04":
                            count[4] = int.Parse(item[1].ToString());
                            money[4] = decimal.Parse(item[2].ToString());
                            break;
                        case "05":
                            count[5] = int.Parse(item[1].ToString());
                            money[5] = decimal.Parse(item[2].ToString());
                            break;
                        case "06":
                            count[6] = int.Parse(item[1].ToString());
                            money[6] = decimal.Parse(item[2].ToString());
                            break;
                        case "07":
                            count[7] = int.Parse(item[1].ToString());
                            money[7] = decimal.Parse(item[2].ToString());
                            break;
                        case "08":
                            count[8] = int.Parse(item[1].ToString());
                            money[8] = decimal.Parse(item[2].ToString());
                            break;
                        case "09":
                            count[9] = int.Parse(item[1].ToString());
                            money[9] = decimal.Parse(item[2].ToString());
                            break;
                        case "10":
                            count[10] = int.Parse(item[1].ToString());
                            money[10] = decimal.Parse(item[2].ToString());
                            break;
                        case "11":
                            count[11] = int.Parse(item[1].ToString());
                            money[11] = decimal.Parse(item[2].ToString());
                            break;
                        case "12":
                            count[12] = int.Parse(item[1].ToString());
                            money[12] = decimal.Parse(item[2].ToString());
                            break;
                        case "13":
                            count[13] = int.Parse(item[1].ToString());
                            money[13] = decimal.Parse(item[2].ToString());
                            break;
                        case "14":
                            count[14] = int.Parse(item[1].ToString());
                            money[14] = decimal.Parse(item[2].ToString());
                            break;
                        case "15":
                            count[15] = int.Parse(item[1].ToString());
                            money[15] = decimal.Parse(item[2].ToString());
                            break;
                        case "16":
                            count[16] = int.Parse(item[1].ToString());
                            money[16] = decimal.Parse(item[2].ToString());
                            break;
                        case "17":
                            count[17] = int.Parse(item[1].ToString());
                            money[17] = decimal.Parse(item[2].ToString());
                            break;
                        case "18":
                            count[18] = int.Parse(item[1].ToString());
                            money[18] = decimal.Parse(item[2].ToString());
                            break;
                        case "19":
                            count[19] = int.Parse(item[1].ToString());
                            money[19] = decimal.Parse(item[2].ToString());
                            break;
                        case "20":
                            count[20] = int.Parse(item[1].ToString());
                            money[20] = decimal.Parse(item[2].ToString());
                            break;
                        case "21":
                            count[21] = int.Parse(item[1].ToString());
                            money[21] = decimal.Parse(item[2].ToString());
                            break;
                        case "22":
                            count[22] = int.Parse(item[1].ToString());
                            money[22] = decimal.Parse(item[2].ToString());
                            break;
                        case "23":
                            count[23] = int.Parse(item[1].ToString());
                            money[23] = decimal.Parse(item[2].ToString());
                            break;

                    }
                    #endregion
                    
                }
                for (int i = 0; i < 24; i++)
                {
                    var s = "PM";
                    if (i < 12)
                    {
                        s = "AM";
                    }
                    DataRow dr = table.NewRow();

                    dr["시간대"] = s + (i).ToString() + "~" + (i + 1);
                    if (count[i].ToString() == "0")
                    {
                        dr["건수"] = "-";
                    }
                    else
                    {
                        dr["건수"] = count[i].ToString() + "건";
                    }
                    

                    if (money[i].ToString() == "0")
                    {
                        dr["총 금액"] = "-";
                    }
                    else
                    {
                        dr["총 금액"] = DataFormat((int)money[i]);
                    }
                   

                    table.Rows.Add(dr);

                }
                timeGridview.DataSource = table;
                timeGridview.DataBind();
                
                con.Close();
            }

        }

        private string DataFormat(int Date)
        {
            return string.Format("{0:0,0}", Date) + "원";
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            makeTimeRevenue();
        }

       
    }
}