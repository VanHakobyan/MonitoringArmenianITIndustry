using System.Collections.Generic;
using System.Linq;
using Database.MonitoringIT.DB.EfCore.Models;

namespace DAL.MonitoringIT.Interfaces
{
    public interface IJobDAL:IBaseDAL<Job> 
    {
        List<Job> GetAllJob();
        List<Job> GetJobsByCategory(string category);
        List<Job> GetJobsByPage(int count,int page);
    }
}