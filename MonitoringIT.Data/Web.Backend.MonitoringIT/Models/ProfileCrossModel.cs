using Database.MonitoringIT.DB.EfCore.Models;

namespace Web.Backend.MonitoringIT.Models
{
    public class ProfileCrossModel
    {
        public GithubProfile GithubProfile { get; set; }
        public LinkedinProfile LinkedinProfile { get; set; }
    }
}
