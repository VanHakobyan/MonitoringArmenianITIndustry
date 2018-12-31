using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;

namespace DAL.MonitoringIT.Implementation
{
    public class CrossProfileDAL : BaseDAL, ICrossProfileDAL
    {
        public CrossProfileDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }

        public List<GithubLinkedinCrossTable> GetAllCrossProfiles()
        {
            return _dbContext.GithubLinkedinCrossTable.ToList();
        }

        public GithubLinkedinCrossTable GetCrossProfileById(int id)
        {
            return _dbContext.GithubLinkedinCrossTable.FirstOrDefault(x => x.Id == id);
        }

        public GithubLinkedinCrossTable GetCrossProfileByGithubProfileId(int id)
        {
            return _dbContext.GithubLinkedinCrossTable.FirstOrDefault(x => x.GithubUserId == id);
        }

        public GithubLinkedinCrossTable GetCrossProfileByLinkedinProfileId(int id)
        {
            return _dbContext.GithubLinkedinCrossTable.FirstOrDefault(x => x.LinkedinUserId == id);
        }
    }
}
