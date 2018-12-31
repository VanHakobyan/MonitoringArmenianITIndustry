using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Backend.MonitoringIT.Models
{
    public class CrossProfileSearchModel
    {
        public string Username { get; set; }
        public ContentType ContentType { get; set; }
    }

    public enum ContentType
    {
        Github = 1,
        Linkedin = 2
    }
}
