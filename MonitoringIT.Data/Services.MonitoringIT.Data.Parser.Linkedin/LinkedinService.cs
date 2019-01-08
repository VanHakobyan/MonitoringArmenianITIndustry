using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Services.MonitoringIT.Data.Parser.Linkedin
{
    public partial class LinkedinService : ServiceBase
    {
        public LinkedinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Task.Run(() => { LinkedinLogic.Start(); });
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
