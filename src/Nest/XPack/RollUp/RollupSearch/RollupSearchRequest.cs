using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("xpack.rollup.rollup_search.json")]
	public partial interface IRollupSearchRequest
	{
		/// <summary> Describe the aggregations to perform</summary>
		[JsonProperty("aggs")]
		AggregationDictionary Aggregations { get; set; }

		/// <summary> Describe the query to perform using a query descriptor lambda</summary>
		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		/// <summary>When doing rollup searches against rolled up and live indices size needs to be set to 0 explicitly </summary>
		[JsonProperty("size")]
		int? Size { get; set; }
	}

	public partial class RollupSearchRequest
	{
		/// <inheritdoc />
		public AggregationDictionary Aggregations { get; set; }

		/// <inheritdoc />
		public QueryContainer Query { get; set; }

		/// <inheritdoc />
		public int? Size { get; set; }
	}

	public partial class RollupSearchDescriptor<T> where T : class
	{
		AggregationDictionary IRollupSearchRequest.Aggregations { get; set; }
		QueryContainer IRollupSearchRequest.Query { get; set; }
		int? IRollupSearchRequest.Size { get; set; }

		/// <inheritdoc cref="IRollupSearchRequest.Size" />
		public RollupSearchDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="IRollupSearchRequest.Aggregations" />
		public RollupSearchDescriptor<T> Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector) =>
			Assign(aggregationsSelector(new AggregationContainerDescriptor<T>())?.Aggregations, (a, v) => a.Aggregations = v);

		/// <inheritdoc cref="IRollupSearchRequest.Aggregations" />
		public RollupSearchDescriptor<T> Aggregations(AggregationDictionary aggregations) =>
			Assign(aggregations, (a, v) => a.Aggregations = v);

		/// <inheritdoc cref="IRollupSearchRequest.Query" />
		public RollupSearchDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) =>
			Assign(query, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
