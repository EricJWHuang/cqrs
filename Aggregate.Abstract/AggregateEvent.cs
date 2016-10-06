using System;

namespace Aggregate.Abstract
{
    /// <summary>
    /// Aggregate event base class.
    /// </summary>
    /// <typeparam name="TI">The type of Aggregate Id.</typeparam>
    [Serializable]
    public abstract class AggregateEvent<TI>
    {
        // An event is an object that
        //  has an Id (so that it can be uniquely identified and to de-duplicate)
        //  has an OccurredAtUtc indicating when it occurred
        //  can be applied on an aggregate

        protected AggregateEvent(Guid id, DateTime occurredAtUtc)
        {
            Id = id;
            OccurredAtUtc = occurredAtUtc;
        }

        public Guid Id { get; private set; }
        public DateTime OccurredAtUtc { get; private set; }

        /// <summary>
        /// Identifier of an aggregate on which this event is applied.
        /// </summary>
        public abstract TI AggregateId { get; }
    }
}
