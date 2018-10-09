using System;
using System.Collections.Generic;

namespace MonitoringIT.DAL.Models
{
    public partial class LinkedinSkill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? EndorsedCount { get; set; }
        public int? LikedinProfileId { get; set; }

        public LinkedinProfile LikedinProfile { get; set; }
    }
}
