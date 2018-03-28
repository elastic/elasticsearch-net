using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A parent pipeline aggregation which sorts the buckets of its parent multi-bucket aggregation.
	/// Zero or more sort fields may be specified together with the corresponding sort order.
	/// Each bucket may be sorted based on its _key, _count or its sub-aggregations.
	/// In addition, parameters from and size may be set in order to truncate the result buckets.
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<BucketSortAggregation>))]
	public interface IBucketSortAggregation : IAggregation
	{
		/// <summary>
		/// The list of fields to sort on
		/// </summary>
		[JsonProperty("sort")]
		IList<ISort> Sort { get; set; }

		/// <summary>
		/// Buckets in positions prior to the set value will be truncated
		/// </summary>
		[JsonProperty("from")]
		int? From { get; set; }

		/// <summary>
		///	The number of buckets to return. Defaults to all buckets of the parent aggregation
		/// </summary>
		[JsonProperty("size")]
		int? Size { get; set; }

		/// <summary>
		/// The policy to apply when gaps are found in the data
		/// </summary>
		[JsonProperty("gap_policy")]
		GapPolicy? GapPolicy { get; set; }
	}

	/// <inheritdoc cref="IBucketSortAggregation" />
	public class BucketSortAggregation
		: AggregationBase, IBucketSortAggregation
	{
		internal BucketSortAggregation()
		{
		}

		public BucketSortAggregation(string name)
			: base(name)
		{
		}

		/// <inheritdoc />
		public IList<ISort> Sort { get; set; }

		/// <inheritdoc />
		public int? From { get; set; }

		/// <inheritdoc />
		public int? Size { get; set; }

		/// <inheritdoc />
		public GapPolicy? GapPolicy { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.BucketSort = this;
	}

	/// <inheritdoc cref="IBucketSortAggregation" />
	public class BucketSortAggregationDescriptor<T>
		: DescriptorBase<BucketSortAggregationDescriptor<T>, IBucketSortAggregation>
			, IBucketSortAggregation
		where T : class
	{
		string IAggregation.Name { get; set; }
		IDictionary<string, object> IAggregation.Meta { get; set; }

		int? IBucketSortAggregation.From { get; set; }
		int? IBucketSortAggregation.Size { get; set; }
		GapPolicy? IBucketSortAggregation.GapPolicy { get; set; }
		IList<ISort> IBucketSortAggregation.Sort { get; set; }

		/// <summary>
		/// The list of fields to sort on
		/// </summary>
		public BucketSortAggregationDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector) =>
			Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<T>())?.Value);

		/// <summary>
		/// Buckets in positions prior to the set value will be truncated
		/// </summary>
		public BucketSortAggregationDescriptor<T> From(int? from) => Assign(a => a.From = from);

		/// <summary>
		///	The number of buckets to return. Defaults to all buckets of the parent aggregation
		/// </summary>
		public BucketSortAggregationDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		/// <summary>
		/// The policy to apply when gaps are found in the data
		/// </summary>
		public BucketSortAggregationDescriptor<T> GapPolicy(GapPolicy? gapPolicy) => Assign(a => a.GapPolicy = gapPolicy);

		/// <summary>
		/// The metadata for the aggregation
		/// </summary>
		public BucketSortAggregationDescriptor<T> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Meta = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}
