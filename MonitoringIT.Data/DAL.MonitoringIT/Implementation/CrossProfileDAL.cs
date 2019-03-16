using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;

namespace DAL.MonitoringIT.Implementation
{
    public class CrossProfileDAL : BaseDAL<GithubLinkedinCrossTable>, ICrossProfileDAL
    {
        public CrossProfileDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }

        public List<GithubLinkedinCrossTable> GetAllCrossProfiles()
        {
            return GetAllQuery().ToList();
        }

        public GithubLinkedinCrossTable GetCrossProfileById(int id)
        {
            return GetAllQuery().FirstOrDefault(x => x.Id == id);
        }

        public GithubLinkedinCrossTable GetCrossProfileByGithubProfileId(int id)
        {
            return GetAllQuery().FirstOrDefault(x => x.GithubUserId == id);
        }

        public GithubLinkedinCrossTable GetCrossProfileByLinkedinProfileId(int id)
        {
            return GetAllQuery().FirstOrDefault(x => x.LinkedinUserId == id);
        }

        public new IQueryable<GithubLinkedinCrossTable> GetAllQuery()
        {
            return _dbContext.GithubLinkedinCrossTable;
        }
    }
}
