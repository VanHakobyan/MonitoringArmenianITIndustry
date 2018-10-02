using System;
using System.Collections.Generic;

namespace MonitoringIT.Data.Common
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Url { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string BlogOrWebsite { get; set; }
        public int StarsCount { get; set; }
        public List<string> FolowersUrl { get; set; }
        public List<string> FolowingUrl { get; set; }
        private List<Repository> Repositories { get; set; }
    }
}
