using System.Collections.Generic;
using System.Linq;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.MonitoringIT.Implementation
{
    public class GithubProfileDAL : BaseDAL, IGithubProfileDAL
    {
        public GithubProfileDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }

        public List<Profiles> GetAll()
        {
            return _dbContext.Profiles.ToList();
        }

        public Profiles GetById(int id)
        {
            return _dbContext.Profiles.Include(x=>x.Repositories).ThenInclude(x=>x.Languages).FirstOrDefault(x=>x.Id==id);
        }

        public Profiles GetByUserName(string username)
        {
            return _dbContext.Profiles.Include(x => x.Repositories).ThenInclude(x => x.Languages).FirstOrDefault(x => x.UserName == username);
        }
    }
}
