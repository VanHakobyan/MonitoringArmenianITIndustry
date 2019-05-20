using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Database.MonitoringIT.DB.EfCore.Models
{
    public partial class Job
    {
        [NotMapped]
        public string Image { get; set; }
    }
}
