using System.Collections.Generic;
using System.Linq;
using Database.MonitoringIT.DB.EfCore.Models;
using DAL.MonitoringIT.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.MonitoringIT.Implementation
{
    public class GithubProfileDAL : BaseDAL, IGithubProfileDAL
    {
        public GithubProfileDAL(MonitoringContext dbContext) : base(dbContext)
        {
        }

        public List<GithubProfile> GetAll()
        {
            var profiles = _dbContext.GithubProfiles.ToList();
            foreach (var profile in profiles)
            {
                foreach (var profileRepository in profile.GithubRepositories)
                {
                    profileRepository.Readme = null;
                }
            }

            return profiles;
        }


        public GithubProfile GetById(int id)
        {
            var profile = _dbContext.GithubProfiles.Include(x => x.GithubRepositories).ThenInclude(x => x.GithubLanguages).FirstOrDefault(x => x.Id == id);
            if (profile is null) return null;

            foreach (var profileRepository in profile.GithubRepositories)
            {
                profileRepository.Readme = null;
            }

            return profile;
        }

        public List<GithubProfile> GetAllWithReadme()
        {
            return _dbContext.GithubProfiles.ToList();
        }

        public GithubProfile GetByIdWithReadme(int id)
        {
            return _dbContext.GithubProfiles.Include(x => x.GithubRepositories).ThenInclude(x => x.GithubLanguages).FirstOrDefault(x => x.Id == id);
        }

        public GithubProfile GetByUserName(string username)
        {
            return _dbContext.GithubProfiles.Include(x => x.GithubRepositories).ThenInclude(x => x.GithubLanguages).FirstOrDefault(x => x.UserName == username);
        }
    }
}
