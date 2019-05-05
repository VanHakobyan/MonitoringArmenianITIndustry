using System;
using System.Collections.Generic;
using System.Linq;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace DAL.MonitoringIT.Implementation
{
    public class CompanyDAL : BaseDAL<Company>, ICompanyDAL
    {
        public CompanyDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }

        public new IQueryable<Company> GetAllQuery()
        {
            return _dbContext.Company;
        }
        public new IQueryable<Company> GetAllQueryByPage(int count, int page)
        {
            return GetAllQuery().Skip((page - 1) * count).Take(count);
        }

        public new IQueryable<Company> GetFavoritesQuery(int count)
        {
            var random = new Random();
            var num = random.Next(1, 20);
            return GetAllQuery().Where(x => !string.IsNullOrEmpty(x.Image) && x.Views > 50000 && x.Industry== "Information technologies").Skip(num).Take(count);
        }
        public List<Company> GetAllCompany()
        {
            return GetAllQuery().ToList();
        }

        public List<Company> GetCompanyByPage(int count, int page)
        {
            return GetAllQueryByPage(count, page).ToList();
        }

        public List<Company> GetFavoriteCompanies(int count)
        {
            return GetFavoritesQuery(count).ToList();
        }

        public List<Company> GetCompaniesByIndustry(string industry)
        {
            return GetAllQuery().Where(x => x.Industry == industry).ToList();
        }

        public List<Company> GetAllCompanyWithJob()
        {
            return GetAllQuery().Where(x => x.Job != null && x.Job.Count > 0).Include(x => x.Job).ToList();
        }
    }
}