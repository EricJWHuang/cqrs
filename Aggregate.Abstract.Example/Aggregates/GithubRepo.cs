using System;
using System.Collections.Generic;
using System.Linq;
using Aggregate.Abstract.Example.AggregateEvents.GithubRepoEvents;

namespace Aggregate.Abstract.Example.Aggregates
{
    public class GithubRepo : Aggregate<Guid>
    {
        #region Event Sourcing

        public override Guid Id { get; protected set; }

        public override void Initialise(Guid githubRepoId)
        {
            Id = githubRepoId;
        }

        public override void Replay(IEnumerable<AggregateEvent<Guid>> aggregateEvents)
        {
            aggregateEvents = aggregateEvents.OrderBy(@event => @event.OccurredAtUtc);
            foreach (dynamic aggregateEvent in aggregateEvents)
            {
                // use dynamtic instead of var so that we can prevent complaints from compiler
                //  however, this does increase the runtime exceptions, but if we have enough tests coverage, should be fine
                Handle(aggregateEvent);
            }

            var lastAggregateEvent = aggregateEvents.LastOrDefault();
            if (lastAggregateEvent == null)
                return;

            _lastEventAppliedAt = lastAggregateEvent.OccurredAtUtc;
        }

        // events that have already occurred but not persisted yet
        private readonly List<GithubRepoEvent> _uncommittedEvents = new List<GithubRepoEvent>();
        public override IEnumerable<AggregateEvent<Guid>> UncommittedEvents
        {
            get { return _uncommittedEvents; }
        }

        private DateTime _lastEventAppliedAt;
        public override DateTime LastEventAppliedAt { get { return _lastEventAppliedAt; } }

        #region A set of Handlers for each aggregate events

        // each handler is responsible for updating underlying model and make sure the aggregate looks correct from its public interface's perspective

        private void Handle(ChangesCommitted changesCommitted)
        {
            // update first commit datetime if required
            //  and update last commit datetime
            if (!FirstCommitAtUtc.HasValue)
            {
                FirstCommitAtUtc = changesCommitted.OccurredAtUtc;
            }

            LastCommitAtUtc = changesCommitted.OccurredAtUtc;
        }

        private void Handle(BranchCreated branchCreated)
        {
            _branches.Add(branchCreated.NewBranchName);
        }

        private void Handle(Initialised initialised)
        {
            Name = initialised.Name;
        }

        #endregion

        #endregion

        #region Domain Model - Event Sourcing agnostic

        public GithubRepo()
        {
            _branches = new List<string>();
        }

        public GithubRepo(Guid id, string userName, string name, DateTime commitedAtUtc)
            : this()
        {
            Initialise(id);

            Initialise(userName, name, commitedAtUtc);
        }

        /// <summary>
        /// Name of the repo.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// When the first commit occurred.
        /// </summary>
        public DateTime? FirstCommitAtUtc { get; private set; }

        /// <summary>
        /// When the last commit occurred.
        /// </summary>
        public DateTime? LastCommitAtUtc { get; private set; }

        /// <summary>
        /// Total number of branches.
        /// </summary>
        public int NumberOfBranches {get { return _branches.Count; }}

        private readonly List<string> _branches;
        /// <summary>
        /// Get a list of branches
        /// </summary>
        public IEnumerable<string> Branches { get { return _branches; } }

        private void Initialise(string userName, string name, DateTime commitedAtUtc)
        {
            // describe it as an event, rather than modifying the underlying model
            var @event = new Initialised(Guid.NewGuid(), commitedAtUtc, Id, userName, name);

            // mark this event as uncommitted, so that they can be picked up and persisted
            _uncommittedEvents.Add(@event);

            // apply this event to aggregate, which then updates the underlying model
            Handle(@event);
        }

        public void Commit(string userName, string comments, DateTime commitedAtUtc)
        {
            // describe it as an event, rather than modifying the underlying model
            var @event = new ChangesCommitted(Guid.NewGuid(), commitedAtUtc, Id, userName, comments);

            // mark this event as uncommitted, so that they can be picked up and persisted
            _uncommittedEvents.Add(@event);

            // apply this event to aggregate, which then updates the underlying model
            Handle(@event);
        }

        public void CreateBranch(string userName, string branchName, DateTime commitedAtUtc)
        {
            // describe it as an event, rather than modifying the underlying model
            var @event = new BranchCreated(Guid.NewGuid(), commitedAtUtc, Id, userName, branchName);

            // mark this event as uncommitted, so that they can be picked up and persisted
            _uncommittedEvents.Add(@event);

            // apply this event to aggregate, which then updates the underlying model
            Handle(@event);
        }

        #endregion
    }
}
