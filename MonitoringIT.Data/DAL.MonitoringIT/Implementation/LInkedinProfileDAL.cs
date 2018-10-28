using System.Collections.Generic;
using System.Linq;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;

namespace DAL.MonitoringIT.Implementation
{
    public class LinkedinProfileDAL : BaseDAL, ILinkedinProfileDAL
    {
        public LinkedinProfileDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }

        public List<LinkedinProfile> GetAll()
        {
            return _dbContext.LinkedinProfile.ToList();
        }
    }
}
