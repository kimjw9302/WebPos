﻿using System;
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
    public partial class AllTotalRevenue : System.Web.UI.Page
    {
        float row1_sum,row2_sum,row3_sum;
        DataTable dt1;
        DataRow dr1;
        SqlConnection con = null;

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
 
                DateTime temp = DateTime.Parse(DateSample.Text); ;
                dt1.Columns.Add("분류");
    
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
                        cmd.Parameters.AddWithValue("@sdate", temp.ToShortDateString());
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
               dr1["합계"] = row1_sum.ToString();
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
                dr1["합계"] = row2_sum;
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
                dr1["합계"] = row3_sum;
                dt1.Rows.Add(dr1);

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
                dr1["합계"] = row4_sum;
                dt1.Rows.Add(dr1);
                con.Close();
                ClientTable();
            }
            }
        public void ClientTable()
        {
           
            string table = "$(document).ready(function(){$('#tablediv').html(\"";
            table += "<div class='col-sm-12' style='overflow:scroll'>";
            table += "<table style='width:300%; max-widht:290%' class='table table-striped table-bordered table-hover' id='dataTables-example'>";
            table += "<thead><tr>";
            foreach (DataColumn column in dt1.Columns)
            {
                table+= "<th>"+column.ColumnName+"</th>";
            }
            table += "</tr></thead>";
            table += "<tbody>";
      
            foreach (DataRow row in dt1.Rows)
            {
                table += "<tr class='odd grade'X>";
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    table += "<td>" + row.ItemArray[i].ToString() + "</td>";
                }
                table += "</tr>";
            }

            table += "</tbody>";
            table += "</table>\");});";
            DrawChart();
            ClientScript.RegisterClientScriptBlock(this.GetType(),"tablediv", table,true);
         
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
        public string parseDate(string date)
        {
            DateTime currentdate = DateTime.Parse(date);
            return currentdate.Year + " " + currentdate.Month + " " + currentdate.Day;
        }
        public void DrawChart()
        {
            string chart = "$(document).ready(function(){";
             chart+= "$('#morrisChart').html(\"<div class='row' id='morris-area-chart'></div>\");";    
               chart+= "$(function(){" +
               "Morris.Area({" +
               "element:'morris-area-chart',"
               ;
            chart += "data: [";
            int j = 1;
            for (int i = 1; i < dt1.Columns.Count-1; i++)
            {
                chart += "{";
               chart +="year: '"+ parseDate(dt1.Columns[i].ToString())+"',";
                chart += "총매출현황: " +dt1.Rows[3].ItemArray[j].ToString() + ",";
                chart += "매출액: " + dt1.Rows[0].ItemArray[j].ToString() + ",";
                chart += "매출단가: " + dt1.Rows[2].ItemArray[j].ToString() + ",";
                chart += "}";
                j++;
                if (i != dt1.Columns.Count - 2)
                {
                    chart += ",";
                }
            }
            chart += "] ,xkey:'year',parseTime: false";
            chart += ",ykeys:['총매출현황', '매출액', '매출단가'],";
            chart += "labels: ['총매출현황', '매출액', '매출단가'],";
            chart += " pointSize: 2,";
            chart += " hideHover: 'auto', resize: true  });});});";
        
            ClientScript.RegisterClientScriptBlock(this.GetType(), "stablediv", chart, true);
        }
    }
}