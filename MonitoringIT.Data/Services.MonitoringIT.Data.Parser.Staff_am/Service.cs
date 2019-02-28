using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Lib.MonitoringIT.Data.Staff_am.Scrapper;

namespace Services.MonitoringIT.Data.Parser.Staff_am
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StaffScrapper scrapper = new StaffScrapper();
            scrapper.StartSScrapping();
            //scrapper.InitDriver();
            //scrapper.GetCompanyLinks();
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
