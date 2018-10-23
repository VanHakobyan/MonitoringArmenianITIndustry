using System;
using System.Collections.Generic;

namespace Database.MonitoringIT.DB.EfCore.Models
{
    public partial class LinkedinSkill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? EndorsedCount { get; set; }
        public int LinkedinProfileId { get; set; }

        public LinkedinProfile LinkedinProfile { get; set; }
    }
}
