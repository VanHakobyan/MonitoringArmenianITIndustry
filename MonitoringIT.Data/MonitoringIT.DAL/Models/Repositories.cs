using System;
using System.Collections.Generic;

namespace MonitoringIT.DAL.Models
{
    public partial class Repositories
    {
        public Repositories()
        {
            Languages = new HashSet<Languages>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int CommitCount { get; set; }
        public int BranchCount { get; set; }
        public string Readme { get; set; }
        public int ProfileId { get; set; }
        public int? StarsCount { get; set; }
        public int? ContributorsCount { get; set; }
        public int? ForksCount { get; set; }

        public Profiles Profile { get; set; }
        public ICollection<Languages> Languages { get; set; }
    }
}
