using System;
using System.Collections.Generic;

namespace Database.MonitoringIT.DB.EfCore.Models
{
    public partial class LinkedinProfile
    {
        public LinkedinProfile()
        {
            LinkedinEducation = new HashSet<LinkedinEducation>();
            LinkedinExperience = new HashSet<LinkedinExperience>();
            LinkedinInterest = new HashSet<LinkedinInterest>();
            LinkedinLanguage = new HashSet<LinkedinLanguage>();
            LinkedinSkill = new HashSet<LinkedinSkill>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public string Location { get; set; }
        public string Education { get; set; }
        public string Company { get; set; }
        public int? ConnectionCount { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        public DateTime? Connected { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? LastUpdate { get; set; }

        public virtual ICollection<LinkedinEducation> LinkedinEducation { get; set; }
        public virtual ICollection<LinkedinExperience> LinkedinExperience { get; set; }
        public virtual ICollection<LinkedinInterest> LinkedinInterest { get; set; }
        public virtual ICollection<LinkedinLanguage> LinkedinLanguage { get; set; }
        public virtual ICollection<LinkedinSkill> LinkedinSkill { get; set; }
    }
}
