using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebProject
{
    public class DBController
    {
        static private SqlConnection con = null;
        private DBController()
        {

        }
        static public SqlConnection Instance()
        {
            if (con == null)
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["gposStr"].ConnectionString);
            }
            return con;
        }

    }
}