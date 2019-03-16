using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Database.MonitoringIT.DB.EfCore.Models;

namespace DAL.MonitoringIT.Interfaces
{
    public interface ICompanyDAL : IBaseDAL<Company> 
    {
        List<Company> GetAllCompany();
        List<Company> GetCompaniesByIndustry(string industry);
        List<Company> GetAllCompanyWithJob();
    }
}