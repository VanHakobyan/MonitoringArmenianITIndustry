using System;
using System.Collections.Generic;

namespace Database.MonitoringIT.DB.EfCore.Models
{
    public partial class Proxies
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public string Type { get; set; }
    }
}
