using System;
using System.Collections.Generic;

namespace Database.MonitoringIT.DB.EfCore.Models
{
    public partial class GithubRepository
    {
        public GithubRepository()
        {
            Languages = new HashSet<GithubLanguage>();
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

        public GithubProfile GithubProfile { get; set; }
        public ICollection<GithubLanguage> Languages { get; set; }
    }
}
