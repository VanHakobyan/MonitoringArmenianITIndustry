using System;
using System.Collections.Generic;

namespace MonitoringIT.DAL.Models
{
    public partial class Languages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Procent { get; set; }
        public int? RepositoryId { get; set; }

        public Repositories Repository { get; set; }
    }
}
