using Database.MonitoringIT.DAL.WithEF6;
using System.Linq;

namespace Detection.MonitoringIT.Data.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var textDetection = new TextDetection();
            using (var db = new MonitoringEntities())
            {
                var githubLinkedinCrossTables = db.GithubLinkedinCrossTables.ToList();
                var gList = githubLinkedinCrossTables.Select(a => a.GithubUserId);
                var lList = githubLinkedinCrossTables.Select(a => a.LinkedinUserId);

                var githubProfiles = db.GithubProfiles.Where(x => !gList.Contains(x.Id)).ToList();
                var linkedinProfiles = db.LinkedinProfiles.Where(x => !lList.Contains(x.Id)).ToList();
                foreach (var githubProfile in githubProfiles)
                {
                    if (string.IsNullOrEmpty(githubProfile.Name) || string.IsNullOrEmpty(githubProfile.UserName)) continue;
                    foreach (var linkedinProfile in linkedinProfiles)
                    {
                        if (string.IsNullOrEmpty(linkedinProfile.FullName) || string.IsNullOrEmpty(linkedinProfile.Username)) continue;

                        var similarityByName = textDetection.GetSimilarity(githubProfile.Name, linkedinProfile.FullName, AlgorithmTypes.OverlapCoefficient);
                        var similarityByUserName = textDetection.GetSimilarity(githubProfile.UserName, linkedinProfile.Username, AlgorithmTypes.OverlapCoefficient);
                        if (similarityByName > 0.85 && similarityByUserName > 0.85)
                        {
                            if (db.GithubLinkedinCrossTables.FirstOrDefault(x => x.GithubUserId == githubProfile.Id) == null)
                            {
                                db.GithubLinkedinCrossTables.Add(new GithubLinkedinCrossTable() { GithubUserId = githubProfile.Id, LinkedinUserId = linkedinProfile.Id });
                                db.SaveChanges();
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
