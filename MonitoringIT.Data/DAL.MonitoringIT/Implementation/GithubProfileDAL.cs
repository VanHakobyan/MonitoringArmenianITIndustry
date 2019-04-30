using System;
using System.Collections.Generic;
using System.Linq;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.MonitoringIT.Implementation
{
    public class GithubProfileDAL : BaseDAL<GithubProfile>, IGithubProfileDAL
    {
        public GithubProfileDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }

        public List<GithubProfile> GetByPage(int count, int page)
        {
            var profiles = GetAllQueryByPage(count,page).ToList();
            foreach (var profile in profiles)
            {
                foreach (var profileRepository in profile.GithubRepository)
                {
                    profileRepository.Readme = null;
                }
            }

            return profiles;
        }

        public new IQueryable<GithubProfile> GetFavoritesQuery(int count)
        {
            var random = new Random();
            var num = random.Next(1, 50);
            return GetAllQuery().Where(x=>!string.IsNullOrEmpty(x.ImageUrl) && !string.IsNullOrEmpty(x.Bio) &&!string.IsNullOrEmpty(x.Company)).Skip(num).Take(count);
        }

        public List<GithubProfile> GetFavorites(int count)
        {
            return GetFavoritesQuery(count).ToList();
        }

        public List<GithubProfile> GetAll()
        {
            var profiles = GetAllQuery().ToList();
            foreach (var profile in profiles)
            {
                foreach (var profileRepository in profile.GithubRepository)
                {
                    profileRepository.Readme = null;
                }
            }

            return profiles;
        }

        public GithubProfile GetById(int id)
        {
            var profile = GetAllQuery().Include(x => x.GithubRepository).ThenInclude(x => x.GithubLanguage).FirstOrDefault(x => x.Id == id);
            if (profile is null) return null;

            foreach (var profileRepository in profile.GithubRepository)
            {
                profileRepository.Readme = null;
            }

            return profile;
        }

        public List<GithubProfile> GetAllWithReadme()
        {
            return GetAllQuery().ToList();
        }

        public GithubProfile GetByIdWithReadme(int id)
        {
            return GetAllQuery().Include(x => x.GithubRepository).ThenInclude(x => x.GithubLanguage).FirstOrDefault(x => x.Id == id);
        }

        public GithubProfile GetByUserName(string username)
        {
            return GetAllQuery().Include(x => x.GithubRepository).ThenInclude(x => x.GithubLanguage).FirstOrDefault(x => x.UserName == username);
        }
        public new IQueryable<GithubProfile> GetAllQuery()
        {
            return _dbContext.GithubProfile;
        }
        public new IQueryable<GithubProfile> GetAllQueryByPage(int count, int page)
        {
            return GetAllQuery().Skip((page - 1) * count).Take(count);
        }
    }
}
