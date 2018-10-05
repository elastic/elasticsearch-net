using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("xpack.rollup.rollup_search.json")]
	public partial interface IRollupSearchRequest
	{
		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		[JsonProperty("aggs")]
		AggregationDictionary Aggregations { get; set; }
	}

	public partial class RollupSearchRequest
	{
		public int? Size { get; set; }
		public QueryContainer Query { get; set; }
		public AggregationDictionary Aggregations { get; set; }
	}

	public partial class RollupSearchDescriptor<T> where T : class
	{
		QueryContainer IRollupSearchRequest.Query { get; set; }
		AggregationDictionary IRollupSearchRequest.Aggregations { get; set; }
		int? IRollupSearchRequest.Size { get; set; }

		/// <summary>When doing rollup searches against rolled up and live indices size needs to be set to 0 explicitly </summary>
		public RollupSearchDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		public RollupSearchDescriptor<T> Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector) =>
			Assign(a => a.Aggregations = aggregationsSelector(new AggregationContainerDescriptor<T>())?.Aggregations);

		public RollupSearchDescriptor<T> Aggregations(AggregationDictionary aggregations) =>
			Assign(a => a.Aggregations = aggregations);

		/// <summary> Describe the query to perform using a query descriptor lambda</summary>
		public RollupSearchDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) =>
			Assign(a => a.Query = query?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
