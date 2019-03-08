using System;
using System.Collections.Generic;

namespace Database.MonitoringIT.DB.EfCore.Models
{
    public partial class Company
    {
        public Company()
        {
            Job = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Industry { get; set; }
        public int NumberOfEmployees { get; set; }
        public DateTime? DateOfFoundation { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string GooglePlus { get; set; }
        public string Twitter { get; set; }
        public string Phone { get; set; }
        public int? Views { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Job> Job { get; set; }
    }
}
