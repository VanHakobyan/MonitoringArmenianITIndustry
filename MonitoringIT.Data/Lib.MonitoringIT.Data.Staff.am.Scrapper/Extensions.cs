using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MonitoringIT.Data.Staff.am.Scrapper
{
    public static class Extensions
    {
        public static string HtmlDecode(this string s)
        {
            return System.Web.HttpUtility.HtmlDecode(s);
        }
    }
}
