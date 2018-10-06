using System;
using System.Collections.Generic;

namespace MonitoringIT.DAL.Models
{
    public partial class LinkedinProfile
    {
        public LinkedinProfile()
        {
            LinkedinEducation = new HashSet<LinkedinEducation>();
            LinkedinExperience = new HashSet<LinkedinExperience>();
            LinkedinInterest = new HashSet<LinkedinInterest>();
            LinkedinLanguage = new HashSet<LinkedinLanguage>();
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
        public byte[] Phone { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        public DateTime? Connected { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<LinkedinEducation> LinkedinEducation { get; set; }
        public ICollection<LinkedinExperience> LinkedinExperience { get; set; }
        public ICollection<LinkedinInterest> LinkedinInterest { get; set; }
        public ICollection<LinkedinLanguage> LinkedinLanguage { get; set; }
    }
}
