using System;

namespace Aggregate.Abstract.Example.AggregateEvents.GithubRepoEvents
{
    public class ChangesCommitted : GithubRepoEvent
    {
        public string Comments { get; private set; }

        public ChangesCommitted(Guid id, DateTime occurredAtUtc, Guid githubRepoId, string userName, string comments)
            : base(id, occurredAtUtc, githubRepoId, userName)
        {
            Comments = comments;
        }
    }
}