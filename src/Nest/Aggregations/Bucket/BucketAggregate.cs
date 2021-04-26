/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using Elasticsearch.Net;

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

		[Obsolete("Use AutoInterval. This property is incorrectly mapped to the wrong type")]
		public Time Interval { get; set; }

		public DateMathTime AutoInterval { get; set; }
	}
}
