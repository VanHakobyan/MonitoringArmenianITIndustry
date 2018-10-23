using System.Collections.Generic;
using System.Linq;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;

namespace DAL.MonitoringIT.Implementation
{
    public class LInkedinProfileDAL : BaseDAL, ILinkedinProfileDAL
    {
        public LInkedinProfileDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }

        public List<LinkedinProfile> GetAll()
        {
            return _dbContext.LinkedinProfile.ToList();
        }
    }
}
