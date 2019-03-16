using System.Collections.Generic;
using System.Linq;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.MonitoringIT.Implementation
{
    public class LinkedinProfileDAL : BaseDAL<LinkedinProfileDAL>, ILinkedinProfileDAL
    {
        public LinkedinProfileDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }

        public List<LinkedinProfile> GetAll()
        {
            return GetAllQuery().ToList();
        }

        public LinkedinProfile GetById(int id)
        {
            return GetAllQuery().Include(x=>x.LinkedinEducation).Include(x=>x.LinkedinExperience).Include(x=>x.LinkedinInterest).Include(x=>x.LinkedinLanguage).Include(x=>x.LinkedinSkill).FirstOrDefault(x => x.Id == id);
        }

        public LinkedinProfile GetByUserName(string username)
        {
            return GetAllQuery().Include(x => x.LinkedinEducation).Include(x => x.LinkedinExperience).Include(x => x.LinkedinInterest).Include(x => x.LinkedinLanguage).Include(x => x.LinkedinSkill).FirstOrDefault(x => x.Username == username);
        }
        public new IQueryable<LinkedinProfile> GetAllQuery()
        {
            return _dbContext.LinkedinProfile;
        }
    }
}
