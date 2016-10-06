using System;
using System.Collections.Generic;

namespace Aggregate.Abstract
{
    /// <summary>
    /// Aggregate base class.
    /// </summary>
    /// <typeparam name="T">the type of aggregate's identifier</typeparam>
    [Serializable]
    public abstract class Aggregate<T>
    {
        // see https://vaughnvernon.co/wordpress/wp-content/uploads/2014/10/DDD_COMMUNITY_ESSAY_AGGREGATES_PART_1.pdf
        //  about what the small aggregates are or aggregates with only necessary attributes

        /// <summary>
        /// Aggregate Id.
        /// </summary>
        public abstract T Id { get; protected set; }

        // as a workaround
        //  http://stackoverflow.com/questions/840261/passing-arguments-to-c-sharp-generic-new-of-templated-type
        //  http://stackoverflow.com/questions/1852837/is-there-a-generic-constructor-with-parameter-constraint-in-c

        /// <summary>
        /// Initialise Aggregate with its Id.
        /// </summary>
        /// <param name="id">the identifier of aggregate</param>
        public abstract void Initialise(T id);

        /// <summary>
        /// Replay a list of aggregate events.
        /// </summary>
        /// <param name="aggregateEvents">a list of aggregate events</param>
        public abstract void Replay(IEnumerable<AggregateEvent<T>> aggregateEvents);

        /// <summary>
        /// Get a list of aggregate events that have not been persisted yet.
        /// </summary>
        public abstract IEnumerable<AggregateEvent<T>> UncommittedEvents { get; }

        /// <summary>
        /// The datetime when the last event was applied. This acts as a version/timestamp attribute for supporting optimistic concurrency.
        /// </summary>
        public abstract DateTime LastEventAppliedAt { get; }
    }
}