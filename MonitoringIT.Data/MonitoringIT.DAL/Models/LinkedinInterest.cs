using System;
using System.Collections.Generic;

namespace MonitoringIT.DAL.Models
{
    public partial class LinkedinInterest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FollowersCount { get; set; }
        public int LinkedinProfileId { get; set; }

        public LinkedinProfile LinkedinProfile { get; set; }
    }
}
