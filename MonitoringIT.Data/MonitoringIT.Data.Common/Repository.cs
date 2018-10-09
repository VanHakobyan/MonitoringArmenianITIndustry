using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MonitoringIT.Data.Common
{
    public class Repository
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int CommitCount { get; set; }
        public int BranchCount { get; set; }
        public List<string> ContributorsUrl { get; set; }
        public string Readme { get; set; }
        public List<Language> Lenguages { get; set; }

        //[ForeignKey("Profile")]
        //public int ProfileId { get; set; }
        //public Profile Profile { get; set; }

    }
}
