using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;

namespace DAL.MonitoringIT.Implementation
{
    class GithubProfileDAL:BaseDAL,IGithubProfileDAL
    {
        public GithubProfileDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }

        public List<Profiles> GetAll()
        {
          return _dbContext.Profiles.ToList();
        }
    }
}
