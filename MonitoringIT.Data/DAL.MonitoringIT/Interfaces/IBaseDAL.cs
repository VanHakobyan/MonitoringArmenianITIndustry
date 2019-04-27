using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.MonitoringIT.Interfaces
{
    public interface IBaseDAL<T> where T : class
    {
        IQueryable<T> GetAllQuery();
        IQueryable<T> GetAllQueryByPage(int count,int page);
    }
}
