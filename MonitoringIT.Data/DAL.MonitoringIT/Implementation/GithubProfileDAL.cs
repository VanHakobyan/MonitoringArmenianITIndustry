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
            var profiles = _dbContext.Profiles.ToList();
            foreach (var profile in profiles)
            {
                foreach (var profileRepository in profile.Repositories)
                {
                    profileRepository.Readme = null;
                }
            }

            return profiles;
        }


        public GithubProfile GetById(int id)
        {
            var profile = _dbContext.Profiles.Include(x => x.Repositories).ThenInclude(x => x.Languages).FirstOrDefault(x => x.Id == id);
            if (profile is null) return null;

            foreach (var profileRepository in profile.Repositories)
            {
                profileRepository.Readme = null;
            }

            return profile;
        }

        public List<GithubProfile> GetAllWithReadme()
        {
            return _dbContext.Profiles.ToList();
        }

        public GithubProfile GetByIdWithReadme(int id)
        {
            return _dbContext.Profiles.Include(x => x.Repositories).ThenInclude(x => x.Languages).FirstOrDefault(x => x.Id == id);
        }

        public GithubProfile GetByUserName(string username)
        {
            return _dbContext.Profiles.Include(x => x.Repositories).ThenInclude(x => x.Languages).FirstOrDefault(x => x.UserName == username);
        }
    }
}
