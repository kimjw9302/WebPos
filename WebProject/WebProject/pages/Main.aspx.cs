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
    public partial class Main : System.Web.UI.Page
    {
        SqlConnection con = null;
        float row1_sum, row2_sum, row3_sum;
        DataTable dt1;
        DataRow dr1;

        private SqlDataAdapter adapter;
        private DataSet ds;
        DataTable dt;
        DataRowCollection dtRow;
        decimal man;
        decimal woman;
        private DataTable manTable;
        private DataTable womenTable;
        private DataRowCollection manRow;
        private DataRowCollection womenRow;
        private decimal[] m = new decimal[5] { 0, 0, 0, 0, 0 };
        private decimal[] w = new decimal[5] { 0, 0, 0, 0, 0 };

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblUserID.Text = Session["userName"].ToString() + " 님 환영합니다";
            if (Session["userGrade"].ToString() == "점장" || Session["userGrade"].ToString() == "사장")
            {

                this.modeTxt.Text = "관리자 모드로 홈페이지를 이용중입니다.";
                // LoadProducts();
                LoadCheckPro();
                LoadWeekChart();
                LoadTime();
                LoadChart();
                PersonCheck();
                LoadWrite();
                LoadOrderCnt();

            }
            else
            {
                Response.Redirect("~/BoardList.aspx");
            }
        }
        public void LoadOrderCnt()
        {
            con = DBController.Instance();
            try
            {
                con.Open();

                using (var cmd = new SqlCommand("CheckOrderCnt", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    DateTime current = DateTime.Now;
                    string year = current.Year.ToString();
                    string month = current.Month.ToString();
                    
                    cmd.Parameters.AddWithValue("sdate", DateTime.Now.Year);
                    if (DateTime.Now.Month.ToString().Length==1)
                    {
                        cmd.Parameters.AddWithValue("edate","0"+ DateTime.Now.Month);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("edate", DateTime.Now.Month);
                    }
                    
                    string a = cmd.ExecuteScalar().ToString();
                    //this.lblOrderCnt.Text = StrForm.Formating(a)+"원";
                }
                con.Close();
            }
            catch (Exception)
            {


            }
        }
        public void LoadWrite()
        {
            con = DBController.Instance();
            try
            {
                con.Open();

                using (var cmd = new SqlCommand("CheckBoardCnt", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("sdate", DateTime.Now.ToShortDateString() + " 00:00:00");
                    cmd.Parameters.AddWithValue("edate", DateTime.Now.ToShortDateString() + " 23:59:59");
                    string a = cmd.ExecuteScalar().ToString();
                    this.lblWriteCnt.Text = a;
                }
                con.Close();
            }
            catch (Exception)
            {


            }
        }

        private void LoadChart()
        {
            if (!IsPostBack)
            {
                makeTimeRevenue();
                makeAge();
            }
        }

        private void makeAge()
        {
            con = DBController.Instance();
            using (var cmd = new SqlCommand("MemberAgeSales", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startDate", DateTime.Now.ToShortDateString() + " 00:00:00");
                cmd.Parameters.AddWithValue("@endDate", DateTime.Now.ToShortDateString() + " 23:59:59");

                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);

                DataRowCollection rows = ds.Tables[0].Rows;
                man = 0;
                woman = 0;
                if (man == 0 && woman == 0)
                {
                    //this.msg.Text = "데이터가 없습니다.";
                }

                foreach (var item in rows)
                {
                    if (rows.Count > 1)
                    {
                        man = decimal.Parse(ds.Tables[0].Rows[0][1].ToString());
                        woman = decimal.Parse(ds.Tables[0].Rows[1][1].ToString());

                        string chart = "$(document).ready(function(){";
                        chart += "$('#ctGender').html(\"<div class='row' id='morris-donut-chart'></div>\");});";
                        chart += "$(function(){" +
                        "Morris.Donut({ " +
                         "element: 'morris-donut-chart', ";

                        chart += "data: [{ label: '남자',";
                        chart += "value : " + man.ToString() + "}, { ";
                        chart += "label : '여자',";
                        chart += "value : " + woman.ToString() + "}],";

                        chart += "colors:[  '#abd2ea', '#e2acb3'],";
                        chart += "resize : true})});";

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "ChartDiv1", chart, true);

                    }

                }

                con.Close();
            }
        }

        private void makeTimeRevenue()
        {
            Response.Write(DateTime.Now.ToShortDateString());
            con = DBController.Instance();
            using (var cmd = new SqlCommand("AgeAnalysis", con))
            {

                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startDate", DateTime.Now.ToShortDateString() + " 00:00:00");
                cmd.Parameters.AddWithValue("@endDate", DateTime.Now.ToShortDateString() + " 23:59:59");

                adapter = new SqlDataAdapter();
                ds = new DataSet();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                manTable = ds.Tables[0];
                womenTable = ds.Tables[1];

                manRow = manTable.Rows;
                womenRow = womenTable.Rows;

            }
            con.Close();

            foreach (DataRow item in manRow)
            {
                switch (item[0].ToString())
                {
                    case "10대":
                        m[0] = (decimal)item[1];
                        break;
                    case "20대":
                        m[1] = (decimal)item[1];
                        break;
                    case "30대":
                        m[2] = (decimal)item[1];
                        break;
                    case "40대":
                        m[3] = (decimal)item[1];
                        break;
                    default:
                        m[4] = (decimal)item[1];
                        break;
                }
            }
            foreach (DataRow item in womenRow)
            {
                switch (item[0].ToString())
                {
                    case "10대":
                        w[0] = (decimal)item[1];
                        break;
                    case "20대":
                        w[1] = (decimal)item[1];
                        break;
                    case "30대":
                        w[2] = (decimal)item[1];
                        break;
                    case "40대":
                        w[3] = (decimal)item[1];
                        break;
                    default:
                        w[4] = (decimal)item[1];
                        break;
                }
            }


            string age = "$(document).ready(function(){";
            age += "$('#ctAge').html(\"<div class='row' id='morris-bar-chart'></div>\");});";
            age += "$(function(){" +
            "Morris.Bar({ " +
             "element: 'morris-bar-chart', ";

            age += "data: [{ y: '10대', m:" + m[0].ToString() + ", w:" + w[0].ToString() + "}, { ";
            age += " y: '20대', m:" + m[1].ToString() + ", w:" + w[1].ToString() + "}, { ";
            age += " y: '30대', m:" + m[2].ToString() + ", w:" + w[2].ToString() + "}, { ";
            age += " y: '40대', m:" + m[3].ToString() + ", w:" + w[3].ToString() + "}, { ";
            age += "y: '50대 이상', m:" + m[4].ToString() + ", w:" + w[4].ToString() + "}], ";
            age += "xkey : 'y',parseTime: false, ";
            age += "ykeys : ['m', 'w'],";
            age += "ymax: 100,";
            age += "labels: [ '남자', '여자' ],";
            age += "barColors: ['#4286f4', '#edbbd3'],";
            age += "hidehover: 'auto',";
            age += "resize : true})});";

            ClientScript.RegisterClientScriptBlock(this.GetType(), "ChartDiv2", age, true);
        }

        private void LoadTime()
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

              
                cmd.Parameters.AddWithValue("@startDate", DateTime.Now.ToShortDateString() + " 00:00:00");
                cmd.Parameters.AddWithValue("@endDate", DateTime.Now.ToShortDateString() + " 23:59:59");

                
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

        public DateTime MakeWeek()
        {

            DateTime currnetDate = DateTime.Now;

            switch (currnetDate.DayOfWeek.ToString())
            {
                case "0"://일요일임!
                    return currnetDate;
                case "1"://월요일
                    currnetDate.AddDays(-1);
                    return currnetDate;
                case "2":
                    currnetDate.AddDays(-2);
                    return currnetDate;
                case "3":
                    currnetDate.AddDays(-3);
                    return currnetDate;
                case "4":
                    currnetDate.AddDays(-4);
                    return currnetDate;
                case "5":
                    currnetDate.AddDays(-5);
                    return currnetDate;
                case "6":
                    currnetDate.AddDays(-6);
                    return currnetDate;
            }

            return currnetDate;
        }
        public void LoadCheckPro()
        {           
            con = DBController.Instance();
            try
            {
                con.Open();
             
                using (var cmd = new SqlCommand("LoadCheckProducts",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();                    
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        this.lblProCnt.Text = "0";
                        con.Close();
                    }
                    else
                    {
                        this.lblProCnt.Text = ds.Tables[0].Rows.Count.ToString();
                        con.Close();
                        
                    }
                }
            }
            catch (Exception)
            {

            
            }
        }
        public void LoadWeekChart()
        {

            con = DBController.Instance();
            
            DateTime sdate = MakeWeek();
            DateTime edate = sdate.AddDays(7);
            TimeSpan subDate = edate - sdate;
            int days = int.Parse(subDate.TotalDays.ToString()) + 1;
            row1_sum = row2_sum = row3_sum = 0;
            DataSet ds = new DataSet();
            dt1 = new DataTable();
            con.Close();
            con.Open();
            DateTime temp = sdate;
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

            sdate = MakeWeek();
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

            sdate = MakeWeek();
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

            sdate = MakeWeek();
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

            DrawChart();

        }
        public string parseDate(string date)
        {
            DateTime currentdate = DateTime.Parse(date);
            return currentdate.Year + " " + currentdate.Month + " " + currentdate.Day;
        }
        public void DrawChart()
        {
            string chart = "$(document).ready(function(){";
            chart += "$('#morrisChart').html(\"<div class='row' id='morris-area-chart'></div>\");";
            chart += "$(function(){" +
            "Morris.Area({" +
            "element:'morris-area-chart',"
            ;
            chart += "data: [";
            int j = 1;
            for (int i = 1; i < dt1.Columns.Count - 1; i++)
            {
                chart += "{";
                chart += "year: '" + parseDate(dt1.Columns[i].ToString()) + "',";
                chart += "총매출현황: " + dt1.Rows[3].ItemArray[j].ToString() + ",";
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
        public void PersonCheck()
        {
            con = DBController.Instance();
            try
            {
                con.Open();

                using (var cmd = new SqlCommand("LoadPersonCheck", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("sdate",DateTime.Now.ToShortDateString()+" 00:00:00");
                    cmd.Parameters.AddWithValue("edate", DateTime.Now.ToShortDateString() + " 23:59:59");
                    string a = cmd.ExecuteScalar().ToString();
                    this.lblCusCnt.Text = a;
                }
                con.Close();
            }
            catch (Exception)
            {


            }

        }
        //public void LoadProducts()
        //{
        //    // 카운트 라벨 : lblProCnt
        //    // 재고 상품명 라벨 : lblProducts

        //    con = DBController.Instance();
        //    con.Open();
        //    using (var cmd = new SqlCommand("LoadCheckProducts", con))
        //    {
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //        using (var sdr = cmd.ExecuteReader())
        //        {
        //            if (sdr.HasRows)
        //            {

        //                while (sdr.Read())
        //                {


        //                    lblProducts.Text = sdr["상품명"].ToString() +" ,  재고수 : "+sdr["재고수"].ToString()+"\r\n";



        //                }
        //                lblProCnt.Text = sdr.FieldCount.ToString();

        //            }
        //            else
        //            {
        //                lblProCnt.Text = "0";
        //                lblProducts.Text = "부족한 상품이 없습니다.";
        //            }                   
        //        }
        //    }

        //    con.Close();

        //}
        protected void btn_Logout_Click(object sender, EventArgs e)
        {
            Session["userGrade"] = null;
            Session["userName"] = null;
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();


        }
    }
}