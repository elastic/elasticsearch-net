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
		public SingleBucketAggregate(IReadOnlyDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		/// <summary>
		/// Count of documents in the bucket
		/// </summary>
		public long DocCount { get; internal set; }
	}

	/// <summary>
	/// Aggregation response for a bucket aggregation
	/// </summary>
	/// <typeparam name="TBucket"></typeparam>
	public class MultiBucketAggregate<TBucket> : IAggregate
		where TBucket : IBucket
	{
		/// <inheritdoc />
		public IReadOnlyDictionary<string, object> Meta { get; set; }

		/// <summary>
		/// The buckets into which results are grouped
		/// </summary>
		public IReadOnlyCollection<TBucket> Buckets { get; set; } = EmptyReadOnly<TBucket>.Collection;
	}

	/// <summary>
	/// Aggregation response of <see cref="CompositeAggregation"/>
	/// </summary>
	public class CompositeBucketAggregate : MultiBucketAggregate<CompositeBucket>
	{
		/// <summary>
		/// The composite key of the last bucket returned
		/// in the response before any filtering by pipeline aggregations.
		/// If all buckets are filtered/removed by pipeline aggregations,
		/// <see cref="AfterKey"/> will contain the composite key of the last bucket before filtering.
		/// </summary>
		/// <remarks> Valid for Elasticsearch 6.3.0+ </remarks>
		public CompositeKey AfterKey { get; set; }
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
