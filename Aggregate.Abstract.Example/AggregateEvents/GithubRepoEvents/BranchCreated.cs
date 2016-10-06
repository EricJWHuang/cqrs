using System;

namespace Aggregate.Abstract.Example.AggregateEvents.GithubRepoEvents
{
    public class BranchCreated : GithubRepoEvent
    {
        public string NewBranchName { get; private set; }

        public BranchCreated(Guid id, DateTime occurredAtUtc, Guid githubRepoId, string userName, string newBranchName)
            : base(id, occurredAtUtc, githubRepoId, userName)
        {
            NewBranchName = newBranchName;
        }
    }
}