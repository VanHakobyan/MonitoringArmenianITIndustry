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

        public List<Profiles> GetAll()
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


        public Profiles GetById(int id)
        {
            var profile = _dbContext.Profiles.Include(x => x.Repositories).ThenInclude(x => x.Languages).FirstOrDefault(x => x.Id == id);
            if (profile is null) return null;

            foreach (var profileRepository in profile.Repositories)
            {
                profileRepository.Readme = null;
            }

            return profile;
        }

        public List<Profiles> GetAllWithReadme()
        {
            return _dbContext.Profiles.ToList();
        }

        public Profiles GetByIdWithReadme(int id)
        {
            return _dbContext.Profiles.Include(x => x.Repositories).ThenInclude(x => x.Languages).FirstOrDefault(x => x.Id == id);
        }

        public Profiles GetByUserName(string username)
        {
            return _dbContext.Profiles.Include(x => x.Repositories).ThenInclude(x => x.Languages).FirstOrDefault(x => x.UserName == username);
        }
    }
}
