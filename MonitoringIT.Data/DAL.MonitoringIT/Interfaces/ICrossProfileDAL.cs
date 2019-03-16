using System.Collections.Generic;
using Database.MonitoringIT.DB.EfCore.Models;

namespace DAL.MonitoringIT.Interfaces
{
    public interface ICrossProfileDAL:IBaseDAL<GithubLinkedinCrossTable> 
    {
        List<GithubLinkedinCrossTable> GetAllCrossProfiles();
        GithubLinkedinCrossTable GetCrossProfileById(int id);
        GithubLinkedinCrossTable GetCrossProfileByGithubProfileId(int id);
        GithubLinkedinCrossTable GetCrossProfileByLinkedinProfileId(int id);
    }
}