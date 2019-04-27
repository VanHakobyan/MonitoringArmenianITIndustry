using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;

namespace DAL.MonitoringIT.Implementation
{
    public class BaseDAL<T>:IBaseDAL<T> where T : class
    {
        protected MonitoringContext _dbContext;

        public BaseDAL(MonitoringContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetAllQuery()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAllQueryByPage(int count, int page)
        {
            throw new NotImplementedException();
        }
    }
}
