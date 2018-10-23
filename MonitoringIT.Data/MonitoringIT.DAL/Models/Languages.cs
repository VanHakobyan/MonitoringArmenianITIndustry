using System;
using System.Collections.Generic;

namespace Database.MonitoringIT.DB.EfCore.Models
{
    public partial class Languages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Percent { get; set; }
        public int RepositoryId { get; set; }

        public Repositories Repository { get; set; }
    }
}
