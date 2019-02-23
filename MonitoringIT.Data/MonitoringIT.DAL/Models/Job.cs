using System;
using System.Collections.Generic;

namespace Database.MonitoringIT.DB.EfCore.Models
{
    public partial class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public DateTime? Deadline { get; set; }
        public string EmploymentTerm { get; set; }
        public string TimeType { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Responsibilities { get; set; }
        public string RequiredQualifications { get; set; }
        public string ProfessionalSkills { get; set; }
        public string AdditionalInformation { get; set; }
        public string SoftSkills { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
