using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebProject.pages
{
    public partial class genderChart : System.Web.UI.Page
    {
        private SqlConnection con;
        DataSet ds;
        SqlDataAdapter adapter;
        decimal man;
        decimal woman;
        private DataTable manTable;
        private DataTable womenTable;
        private DataRowCollection manRow;
        private DataRowCollection womenRow;
        private decimal[] m = new decimal[5] { 0,0,0,0,0};
        private decimal[] w = new decimal[5] { 0,0,0,0,0};

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateSample.Text = DateTime.Now.ToShortDateString();
                DateSample2.Text = DateTime.Now.ToShortDateString();
                makeChart();
                makeAgeChart();
            }
        }

        private void makeAgeChart()
        {
            con = DBController.Instance();
            using (var cmd = new SqlCommand("AgeAnalysis", con))
            {
                
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startDate", DateSample.Text + " 00:00:00");
                cmd.Parameters.AddWithValue("@endDate", DateSample2.Text + " 00:00:00");

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
            age += "labels: [ '남자', '여자' ],";
            age += "hidehover: 'auto',";
            age += "resize : true})});";

            ClientScript.RegisterClientScriptBlock(this.GetType(), "ChartDiv1", age  , true);
            
        }

        private void makeChart()
        {

            con = DBController.Instance();
            using (var cmd = new SqlCommand("MemberAgeSales", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startDate", DateSample.Text + " 00:00:00");
                cmd.Parameters.AddWithValue("@endDate", DateSample2.Text + " 00:00:00");

                ds = new DataSet();
                adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);

                DataRowCollection rows = ds.Tables[0].Rows;
                man = 0;
                woman = 0;
                if (man == 0 && woman == 0)
                {
                    this.msg.Text = "데이터가 없습니다.";
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

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "ChartDiv", chart, true);
                       
                    }
                   
                }

                con.Close();
            }


        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            makeChart();
            makeAgeChart();
        }
    }
}