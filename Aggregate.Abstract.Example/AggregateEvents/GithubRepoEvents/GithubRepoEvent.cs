using System;

namespace Aggregate.Abstract.Example.AggregateEvents.GithubRepoEvents
{
    public abstract class GithubRepoEvent : AggregateEvent<Guid>
    {
        public Guid GithubRepoId { get; private set; }
        public string UserName { get; private set; }

        public override Guid AggregateId
        {
            get { return GithubRepoId; }
        }

        protected GithubRepoEvent(Guid id, DateTime occurredAtUtc, Guid githubRepoId, string userName)
            : base(id, occurredAtUtc)
        {
            GithubRepoId = githubRepoId;
            UserName = userName;
        }
    }
}