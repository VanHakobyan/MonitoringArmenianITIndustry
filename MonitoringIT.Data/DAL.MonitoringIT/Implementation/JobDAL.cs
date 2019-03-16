using System.Collections.Generic;
using System.Linq;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.MonitoringIT.Implementation
{
    public class JobDAL : BaseDAL<Job>, IJobDAL
    {
        public JobDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }
        public new IQueryable<Job> GetAllQuery()
        {
            return _dbContext.Job;
        }
        public List<Job> GetAllJob()
        {
            return GetAllQuery().Include(x => x.StaffSkill).ToList();
        }

        public List<Job> GetJobsByCategory(string category)
        {
            return GetAllQuery().Where(x=>x.Category==category).Include(x => x.StaffSkill).ToList();
        }
    }
}