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
        private const string cookie = "__cfduid=d9bcdc52e419046289c9e9682ec7f5dea1544683349; t=88145399; _ga=GA1.2.1449105534.1544683360; PAPVisitorId=7348119cf106a753ce0ccbe4e14rw0d9; _ym_uid=1544683360469056594; _ym_d=1544683360; cf_clearance=4d6592bb4c5ce2d26d87452a19f26f192d2d1196-1547909622-86400-150; _gid=GA1.2.1162939158.1547909623; _dc_gtm_UA-90263203-1=1; _gat_UA-90263203-1=1; _ym_isad=1; _fbp=fb.1.1547909623961.1368742913; _ym_wasSynced=%7B%22time%22%3A1547909624155%2C%22params%22%3A%7B%22eu%22%3A0%7D%2C%22bkParams%22%3A%7B%7D%7D; _ym_visorc_42065329=w; jv_enter_ts_EBSrukxUuA=1547909625845; jv_visits_count_EBSrukxUuA=3; jv_refer_EBSrukxUuA=https%3A%2F%2Fhidemyna.me%2Fen%2Fproxy-list%2F%3Ftype%3Ds%3Fstart%3D%7Bpage%7D; jv_utm_EBSrukxUuA=; jv_pages_count_EBSrukxUuA=1";
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
