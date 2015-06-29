using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<RescoreQuery>))]
	public interface IRescoreQuery
	{
		[JsonProperty("rescore_query")]
		IQueryContainer Query { get; set; }

		[JsonProperty("query_weight")]
		double? QueryWeight { get; set; }

		[JsonProperty("rescore_query_weight")]
		double? RescoreQueryWeight { get; set; }

		[JsonProperty("score_mode")]
		ScoreMode? ScoreMode { get; set; }
	}
	
	public class RescoreQuery : IRescoreQuery
	{
		public IQueryContainer Query { get; set; }
		public double? QueryWeight { get; set; }
		public double? RescoreQueryWeight { get; set; }
		public ScoreMode? ScoreMode { get; set; }
	}

	public class RescoreQueryDescriptor<T> : IRescoreQuery where T : class
	{
		public IRescoreQuery Self => this;

		IQueryContainer IRescoreQuery.Query { get; set; }

		double? IRescoreQuery.QueryWeight { get; set; }

		double? IRescoreQuery.RescoreQueryWeight { get; set; }

		ScoreMode? IRescoreQuery.ScoreMode { get; set; }

		public virtual RescoreQueryDescriptor<T> QueryWeight(double queryWeight)
		{
			Self.QueryWeight = queryWeight;
			return this;
		}

		public virtual RescoreQueryDescriptor<T> RescoreQueryWeight(double rescoreQueryWeight)
		{
			Self.RescoreQueryWeight = rescoreQueryWeight;
			return this;
		}

		public virtual RescoreQueryDescriptor<T> ScoreMode(ScoreMode scoreMode)
		{
			Self.ScoreMode = scoreMode;
			return this;
		}

		public virtual RescoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> query)
		{
			query.ThrowIfNull("query");
			var q = new QueryDescriptor<T>();

			var bq = query(q);
			IQueryContainer container = bq;
			if (container.IsStrict && !container.IsVerbatim && bq.IsConditionless)
				throw new DslException("Query resulted in a conditionless query:\n{0}".F(JsonConvert.SerializeObject(bq, Formatting.Indented)));

			else if (bq.IsConditionless && !container.IsVerbatim)
				return this;
			Self.Query = bq;
			return this;
		}
		/// <summary>
		/// Describe the query to perform using the static Query class
		/// </summary>
		public virtual RescoreQueryDescriptor<T> Query(QueryContainer query)
		{
			query.ThrowIfNull("query");
			if (query.IsConditionless && !query.IsVerbatim)
				return this;
			Self.Query = query;
			return this;
		}
	}
}