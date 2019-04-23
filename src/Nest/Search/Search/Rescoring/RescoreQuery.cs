using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RescoreQuery>))]
	public interface IRescoreQuery
	{
		[JsonProperty("rescore_query")]
		QueryContainer Query { get; set; }

		[JsonProperty("query_weight")]
		double? QueryWeight { get; set; }

		[JsonProperty("rescore_query_weight")]
		double? RescoreQueryWeight { get; set; }

		[JsonProperty("score_mode")]
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
