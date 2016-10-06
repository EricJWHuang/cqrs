using System;

namespace Aggregate.Abstract.Example.AggregateEvents.GithubRepoEvents
{
    public class Initialised : GithubRepoEvent
    {
        public string Name { get; private set; }

        public Initialised(Guid id, DateTime occurredAtUtc, Guid githubRepoId, string userName, string name)
            : base(id, occurredAtUtc, githubRepoId, userName)
        {
            Name = name;
        }
    }
}
