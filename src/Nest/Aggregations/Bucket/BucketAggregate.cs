// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace Nest
{
	public abstract class BucketAggregateBase : AggregateDictionary, IAggregate
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
		/// <summary>
		/// The buckets into which results are grouped
		/// </summary>
		public IReadOnlyCollection<TBucket> Buckets { get; set; } = EmptyReadOnly<TBucket>.Collection;

		/// <inheritdoc />
		public IReadOnlyDictionary<string, object> Meta { get; set; } = EmptyReadOnly<string, object>.Dictionary;
	}

	/// <summary>
	/// Aggregation response of <see cref="CompositeAggregation" />
	/// </summary>
	public class CompositeBucketAggregate : MultiBucketAggregate<CompositeBucket>
	{
		/// <summary>
		/// The composite key of the last bucket returned
		/// in the response before any filtering by pipeline aggregations.
		/// If all buckets are filtered/removed by pipeline aggregations,
		/// <see cref="AfterKey" /> will contain the composite key of the last bucket before filtering.
		/// </summary>
		/// <remarks> Valid for Elasticsearch 6.3.0+ </remarks>
		public CompositeKey AfterKey { get; set; }
	}

	/// <summary>
	/// Intermediate Aggregation response, transformed to a more specific
	/// aggregation response when requested.
	/// </summary>
	public class BucketAggregate : IAggregate
	{
		public CompositeKey AfterKey { get; set; }
		public long BgCount { get; set; }
		public long DocCount { get; set; }
		public long? DocCountErrorUpperBound { get; set; }
		public IReadOnlyCollection<IBucket> Items { get; set; } = EmptyReadOnly<IBucket>.Collection;
		public IReadOnlyDictionary<string, object> Meta { get; set; } = EmptyReadOnly<string, object>.Dictionary;
		public long? SumOtherDocCount { get; set; }
		public DateMathTime Interval { get; set; }
	}
}
