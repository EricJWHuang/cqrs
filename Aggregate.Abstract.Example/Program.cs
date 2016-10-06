using System;
using Aggregate.Abstract.Example.Aggregates;
using Newtonsoft.Json;

namespace Aggregate.Abstract.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var githubRepoId = Guid.NewGuid();
            var githubRepo = new GithubRepo(githubRepoId, "eric.huang", "testrepo", DateTime.UtcNow);
            githubRepo.Commit("eric.huang", "init commit", DateTime.UtcNow);
            githubRepo.Commit("eric.huang", "2nd commit", DateTime.UtcNow);
            githubRepo.CreateBranch("eric.huang", "branch 1", DateTime.UtcNow);
            githubRepo.CreateBranch("eric.huang", "branch 2", DateTime.UtcNow);

            Console.WriteLine("LastCommitAtUtc: {0}", githubRepo.LastCommitAtUtc);
            Console.WriteLine("LastEventAppliedAt: {0}", githubRepo.LastEventAppliedAt);

            foreach (var uncommittedEvent in githubRepo.UncommittedEvents)
            {
                Console.WriteLine("UncommittedEvent: {0}", JsonConvert.SerializeObject(uncommittedEvent));
            }

            Console.ReadLine();
        }
    }
}
