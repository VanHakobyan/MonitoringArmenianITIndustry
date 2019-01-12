using System;
using System.Collections.Generic;

namespace Database.MonitoringIT.DB.EfCore.Models
{
    public partial class GithubLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Percent { get; set; }
        public int RepositoryId { get; set; }

        public GithubRepository GithubRepository { get; set; }
    }
}
