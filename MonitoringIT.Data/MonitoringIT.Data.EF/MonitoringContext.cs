using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitoringIT.Data.Common;

namespace MonitoringIT.Data.EF
{
    public class MonitoringContext : DbContext
    {
        public MonitoringContext() : base("Monitoring")
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Proxy> Proxies { get; set; }
    }
}

