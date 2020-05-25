// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("rollup.rollup_search.json")]
	public partial interface IRollupSearchRequest
	{
		/// <summary> Describe the aggregations to perform</summary>
		[DataMember(Name ="aggs")]
		AggregationDictionary Aggregations { get; set; }

		/// <summary> Describe the query to perform using a query descriptor lambda</summary>
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }

		/// <summary>When doing rollup searches against rolled up and live indices size needs to be set to 0 explicitly </summary>
		[DataMember(Name ="size")]
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

	public partial class RollupSearchDescriptor<TDocument> where TDocument : class
	{
		AggregationDictionary IRollupSearchRequest.Aggregations { get; set; }
		QueryContainer IRollupSearchRequest.Query { get; set; }
		int? IRollupSearchRequest.Size { get; set; }

		/// <inheritdoc cref="IRollupSearchRequest.Size" />
		public RollupSearchDescriptor<TDocument> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="IRollupSearchRequest.Aggregations" />
		public RollupSearchDescriptor<TDocument> Aggregations(Func<AggregationContainerDescriptor<TDocument>, IAggregationContainer> aggregationsSelector) =>
			Assign(aggregationsSelector(new AggregationContainerDescriptor<TDocument>())?.Aggregations, (a, v) => a.Aggregations = v);

		/// <inheritdoc cref="IRollupSearchRequest.Aggregations" />
		public RollupSearchDescriptor<TDocument> Aggregations(AggregationDictionary aggregations) =>
			Assign(aggregations, (a, v) => a.Aggregations = v);

		/// <inheritdoc cref="IRollupSearchRequest.Query" />
		public RollupSearchDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> query) =>
			Assign(query, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TDocument>()));
	}
}
