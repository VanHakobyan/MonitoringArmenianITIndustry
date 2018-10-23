using System;
using System.Collections.Generic;

namespace Database.MonitoringIT.DB.EfCore.Models
{
    public partial class LinkedinExperience
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public int LinkedinProfileId { get; set; }
        public string Location { get; set; }

        public LinkedinProfile LinkedinProfile { get; set; }
    }
}
