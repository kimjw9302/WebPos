using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebProject
{
    class StrForm
    {
        public static string Formating(string str)
        {
            string strReturn = string.Format("{0:#,##0}", int.Parse(str));

            return strReturn;
        }
        public static string DeFormating(string str)
        {
            string strReturn = Regex.Replace(str, @"\D", "");

            return strReturn;
        }
    }
}
