using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringIT.Data.ProxyParser
{
    public class ProxyModel
    {
        public string Country { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public string Anonymity { get; set; }
        public string Type { get; set; }
    }
}
