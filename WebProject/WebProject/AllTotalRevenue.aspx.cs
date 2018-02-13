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
    public partial class AllTotalRevenue : System.Web.UI.Page
    {
        float row1_sum,row2_sum,row3_sum;
        DataTable dt1 = null;
        DataRow dr1 = null;
        SqlConnection con = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblUserID.Text = Context.User.Identity.Name + " 님 환영합니다";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            makeGridView();
        }
        public void makeGridView()
        {
            if (!CheckDate())
            {

            }
            else
            {
                con = DBController.Instance();
                DateTime sdate = DateTime.Parse(DateChange(DateSample.Text));
                DateTime edate = DateTime.Parse(DateChange(DateSample2.Text));
                TimeSpan subDate = edate - sdate;
                int days = int.Parse(subDate.TotalDays.ToString()) + 1;
                row1_sum = row2_sum = row3_sum = 0;
                DataSet ds = new DataSet();
                dt1 = new DataTable();
             
                con.Open();
 
                DateTime temp = sdate;
                dt1.Columns.Add("분류");
                Response.Write(sdate.ToShortDateString());
                for (int i = 0; i < days; i++)
                {
                    if (sdate.Month.ToString().Length == 1)
                    {
                        if (sdate.Date.ToString().Length == 1)
                            dt1.Columns.Add(sdate.Year.ToString() + "년" + "0" + sdate.Month.ToString() + "월" + "0" + sdate.Day.ToString() + "일");
                        else
                            dt1.Columns.Add(sdate.Year.ToString() + "년" + "0" + sdate.Month.ToString() + "월" + sdate.Day.ToString() + "일");
                    }
                    else
                    {
                        if (sdate.Date.ToString().Length == 1)
                            dt1.Columns.Add(sdate.Year.ToString() + "년" + sdate.Month.ToString() + "월" + "0" + sdate.Day.ToString() + "일");
                        else
                            dt1.Columns.Add(sdate.Year.ToString() + "년" + sdate.Month.ToString() + "월" + sdate.Day.ToString() + "일");
                    }
                    sdate = sdate.AddDays(1);
                }

                dt1.Columns.Add("합계");
                dr1 = dt1.NewRow();
                //매출액 
                dr1[0] = "매출액";

                for (int i = 1; i < days + 1; i++)
                {
                    using (var cmd = new SqlCommand("TotalRevenue", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sdate", temp);
                        var sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            if (sdr["sum"] == DBNull.Value)
                            {
                                dr1[i] = 0.00;
                            }
                            else
                            {
                                dr1[i] = sdr["sum"].ToString();
                                row1_sum += float.Parse(sdr["sum"].ToString());
                            }


                        }
                        sdr.Close();

                    }

                    temp = temp.AddDays(1);
                }

                dr1[days + 1] = row1_sum;
                dt1.Rows.Add(dr1);

                //할인금액
                dr1 = dt1.NewRow();
                dr1[0] = "할인금";

                sdate = DateTime.Parse(DateSample.Text);
                for (int i = 1; i < days + 1; i++)
                {
                    using (var cmd = new SqlCommand("NoteRevenue", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sdate", sdate);
                        var sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            if (sdr["sum"] == DBNull.Value)
                            {
                                dr1[i] = 0.00;
                            }
                            else
                            {
                                dr1[i] = sdr["sum"].ToString();
                                row2_sum += float.Parse(sdr["sum"].ToString());
                            }

                        }
                        sdr.Close();

                    }
                    sdate = sdate.AddDays(1);

                }
                dr1[days + 1] = row2_sum;
                dt1.Rows.Add(dr1);
                //매출단가
                dr1 = dt1.NewRow();
                dr1[0] = "매출단가";

                sdate = DateTime.Parse(DateSample.Text);
                for (int i = 1; i < days + 1; i++)
                {
                    using (var cmd = new SqlCommand("ProMoneyRevenue", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sdate", sdate);
                        var sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            if (sdr["sum"] == DBNull.Value)
                            {
                                dr1[i] = 0.00;
                            }
                            else
                            {
                                dr1[i] = sdr["sum"].ToString();
                                row3_sum += float.Parse(sdr["sum"].ToString());
                            }


                        }
                        sdr.Close();

                    }
                    sdate = sdate.AddDays(1);

                }
                dr1[days + 1] = row3_sum;
                dt1.Rows.Add(dr1);

                //for (int i = 0; i < 3; i++)
                //{
                //    dt.Rows.Add("");
                //}

                //합계
                List<float> intarr = new List<float>();
                dr1 = dt1.NewRow();
                dr1[0] = "매출총합계";

                sdate = DateTime.Parse(DateSample.Text);
                intarr.Add(1);
                int count = 1;
                foreach (DataRow row in dt1.Rows)
                {

                    for (int i = 1; i < row.ItemArray.Length - 1; i++)
                    {
                        //MessageBox.Show("!! : "+row.ItemArray[i].ToString());
                        if (count == 1)
                        {
                            intarr.Add(float.Parse(row.ItemArray[i].ToString()));
                        }
                        else
                        {
                            intarr[i] += float.Parse(row.ItemArray[i].ToString());
                        }
                    }
                    count++;
                }
                float row4_sum = 0;
                for (int i = 1; i < intarr.Count; i++)
                {
                    dr1[i] = intarr[i];
                    row4_sum += intarr[i];
                }
                dr1[days + 1] = row4_sum;
                dt1.Rows.Add(dr1);
                con.Close();
                ClientTable();
            }
            }
        public void ClientTable()
        {
            string table = "$('.panel-body').html('";
            table += "<div class='col-sm-12'>";
            table += "<table width='100%' class='table table-striped table - bordered table - hover' id='dataTables - example'>";
            table += "<thead><tr>";
            foreach (DataColumn column in dt1.Columns)
            {
                table+= "<th>"+column.ColumnName+"</th>";
            }
            table += "</ tr></thead>";
            table += "<tbody>";
      
            foreach (DataRow row in dt1.Rows)
            {
                table += "<tr>";
                for (int i = 0; i < row.ItemArray.Length-1; i++)
                {
                    table += "<td>" + row.ItemArray[i].ToString() + "</td>";
                }
                table += "/<tr>";
            }

            table += "</tbody>";
            table += "</table>');";

            ClientScript.RegisterClientScriptBlock(this.GetType(),".body", table,true);
        }
        public bool CheckDate()
        {

            if ((DateSample.Text == "") || (DateSample2.Text == ""))
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
        public string DateChange(string date)
        {
            string first = date.Substring(0, 5);
            string end = date.Substring(6, 4);
            return end + "/" + first;
        }
    }
}