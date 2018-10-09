using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringIT.Data.Common
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Procent { get; set; }

        //[ForeignKey("Repository")]
        //public int RepositoryId { get; set; }
        //public Repository Repository { get; set; }
    }
}
