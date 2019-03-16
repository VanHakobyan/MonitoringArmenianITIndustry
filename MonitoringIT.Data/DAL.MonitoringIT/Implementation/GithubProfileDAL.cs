﻿using System.Collections.Generic;
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
    }
}
