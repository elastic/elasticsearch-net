using System;
using System.Collections.Generic;

namespace Nest
{
	public abstract class BucketAggregateBase : AggregateDictionary , IAggregate
	{
		protected BucketAggregateBase(IReadOnlyDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public IReadOnlyDictionary<string, object> Meta { get; set; } = EmptyReadOnly<string, object>.Dictionary;
	}

	public class SingleBucketAggregate : BucketAggregateBase
	{
		public SingleBucketAggregate(IReadOnlyDictionary<string, IAggregate> aggregations) : base(aggregations)
		{
#pragma warning disable 618
			// TODO: Remove in NEST 7.x.
			Aggregations = this;
#pragma warning restore 618
		}

		[Obsolete("Use methods on this instance to access sub aggregations. Will be removed in NEST 7.x")]
		public AggregateDictionary Aggregations { get; protected internal set; }

		public long DocCount { get; internal set; }
	}

	public class MultiBucketAggregate<TBucket> : IAggregate
		where TBucket : IBucket
	{
		public IReadOnlyDictionary<string, object> Meta { get; set; }

		public IReadOnlyCollection<TBucket> Buckets { get; set; } = EmptyReadOnly<TBucket>.Collection;
	}

	public class CompositeBucketAggregate : IAggregate
	{
		public IReadOnlyDictionary<string, object> Meta { get; set; }

		public IReadOnlyCollection<CompositeBucket> Buckets { get; set; } = EmptyReadOnly<CompositeBucket>.Collection;

		/// <summary>
		/// The after_key is equals to the last bucket returned in the response before any filtering that could be done by Pipeline aggregations.
		/// If all buckets are filtered/removed by a pipeline aggregation, the after_key will contain the last bucket before filtering.
		/// </summary>
		/// <remarks> Valid for Elasticsearch 6.3.0+ </remarks>
		public IReadOnlyDictionary<string, object> AfterKey { get; set; } = EmptyReadOnly<string, object>.Dictionary;
	}


	// Intermediate object used for deserialization
	public class BucketAggregate : IAggregate
	{
		public IReadOnlyCollection<IBucket> Items { get; set; } = EmptyReadOnly<IBucket>.Collection;
		public long? DocCountErrorUpperBound { get; set; }
		public long? SumOtherDocCount { get; set; }
		public IReadOnlyDictionary<string, object> Meta { get; set; } = EmptyReadOnly<string, object>.Dictionary;
		public long DocCount { get; set; }
		public long BgCount { get; set; }
		public IReadOnlyDictionary<string, object> AfterKey { get; set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}
