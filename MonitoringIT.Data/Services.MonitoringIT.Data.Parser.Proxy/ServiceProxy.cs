using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Database.MonitoringIT.DAL.WithEF6;
using Lib.MonitoringIT.Data.ProxyParser;
using Services.MonitoringIT.Data.Integration.Github;

namespace Services.MonitoringIT.Data.Parser.Proxy
{
    public partial class ServiceProxy : ServiceBase
    {
        private const string cookie = "__cfduid=d2757d919c97f308de2ce0e1aea6ba9b51558338260; cf_clearance=c005fecb3505149d6eaff6254d2dcedcb94b6cb5-1558338268-86400-150; t=118714372; _ga=GA1.2.805940750.1558338270; _gid=GA1.2.721649801.1558338270; _ym_uid=1558338270305853745; _ym_d=1558338270; _ym_isad=1; _ym_wasSynced=%7B%22time%22%3A1558338270230%2C%22params%22%3A%7B%22eu%22%3A0%7D%2C%22bkParams%22%3A%7B%7D%7D; _fbp=fb.1.1558338270418.181706723; _ym_visorc_42065329=w; jv_enter_ts_EBSrukxUuA=1558338274153; jv_visits_count_EBSrukxUuA=1; jv_refer_EBSrukxUuA=https%3A%2F%2Fhidemyna.me%2Fen%2Fproxy-list%2F%3Ftype%3Ds%3Fstart%3D%7Bpage%7D; jv_utm_EBSrukxUuA=; _dc_gtm_UA-90263203-1=1; _gat_UA-90263203-1=1; jv_pages_count_EBSrukxUuA=3";
        public ServiceProxy()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            HidemeParser _hidemeParser = new HidemeParser();
            var proxies = _hidemeParser.GetProxy(cookie).Result;
            using (MonitoringEntities db=new MonitoringEntities())
            {
                db.Database.ExecuteSqlCommand("delete from proxy");
                db.Proxies.AddRange(proxies);
                db.SaveChanges();
            }
        }

        protected override void OnStop()
        {
        }

        public void TestAndStart()
        {
            OnStart(null);
            Console.ReadKey();
            OnStop();
        }
    }
}
