using Database.MonitoringIT.DB.EfCore.Models;

namespace Web.Backend.MonitoringIT.Models
{
    public class ProfileCrossModel
    {
        public Profiles GithubProfile { get; set; }
        public LinkedinProfile LinkedinProfile { get; set; }
    }
}
