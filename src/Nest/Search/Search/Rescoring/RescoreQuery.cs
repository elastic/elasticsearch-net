// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(RescoreQuery))]
	public interface IRescoreQuery
	{
		[DataMember(Name = "rescore_query")]
		QueryContainer Query { get; set; }

		[DataMember(Name = "query_weight")]
		double? QueryWeight { get; set; }

		[DataMember(Name = "rescore_query_weight")]
		double? RescoreQueryWeight { get; set; }

		[DataMember(Name = "score_mode")]
		ScoreMode? ScoreMode { get; set; }
	}

	public class RescoreQuery : IRescoreQuery
	{
		public QueryContainer Query { get; set; }
		public double? QueryWeight { get; set; }
		public double? RescoreQueryWeight { get; set; }
		public ScoreMode? ScoreMode { get; set; }
	}

	public class RescoreQueryDescriptor<T> : DescriptorBase<RescoreQueryDescriptor<T>, IRescoreQuery>, IRescoreQuery
		where T : class
	{
		QueryContainer IRescoreQuery.Query { get; set; }
		double? IRescoreQuery.QueryWeight { get; set; }
		double? IRescoreQuery.RescoreQueryWeight { get; set; }
		ScoreMode? IRescoreQuery.ScoreMode { get; set; }

		public virtual RescoreQueryDescriptor<T> QueryWeight(double? queryWeight) => Assign(queryWeight, (a, v) => a.QueryWeight = v);

		public virtual RescoreQueryDescriptor<T> RescoreQueryWeight(double? rescoreQueryWeight) =>
			Assign(rescoreQueryWeight, (a, v) => a.RescoreQueryWeight = v);

		public virtual RescoreQueryDescriptor<T> ScoreMode(ScoreMode? scoreMode) => Assign(scoreMode, (a, v) => a.ScoreMode = v);

		public virtual RescoreQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) =>
			Assign(query, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
