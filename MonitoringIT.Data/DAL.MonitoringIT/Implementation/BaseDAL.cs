using System;
using System.Collections.Generic;
using System.Text;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;

namespace DAL.MonitoringIT.Implementation
{
    public class BaseDAL:IBaseDAL
    {
        protected MonitoringContext _dbContext;

        public BaseDAL(MonitoringContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
