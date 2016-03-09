using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FunctionScoreQueryDescriptor<object>>))]
	public interface IFunctionScoreQuery : IQuery
	{
		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		[JsonProperty("functions")]
		IEnumerable<IScoreFunction> Functions { get; set; }

		[JsonProperty("max_boost")]
		double? MaxBoost { get; set; }

		[JsonProperty("score_mode")]
		FunctionScoreMode? ScoreMode { get; set; }

		[JsonProperty("boost_mode")]
		FunctionBoostMode? BoostMode { get; set; }

		[JsonProperty("min_score")]
		double? MinScore { get; set; }
	}

	public class FunctionScoreQuery : QueryBase, IFunctionScoreQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public IEnumerable<IScoreFunction> Functions { get; set; }
		public QueryContainer Query { get; set; }
		public FunctionScoreMode? ScoreMode { get; set; }
		public FunctionBoostMode? BoostMode { get; set; }
		public double? MaxBoost { get; set; }
		public double? MinScore { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.FunctionScore = this;

		internal static bool IsConditionless(IFunctionScoreQuery q, bool force = false) => 
			force || !q.Functions.HasAny();
	}

	public class FunctionScoreQueryDescriptor<T> 
		: QueryDescriptorBase<FunctionScoreQueryDescriptor<T>, IFunctionScoreQuery>
		, IFunctionScoreQuery where T : class
	{
		private bool _forcedConditionless = false;
		protected override bool Conditionless => FunctionScoreQuery.IsConditionless(this, _forcedConditionless);
		IEnumerable<IScoreFunction> IFunctionScoreQuery.Functions { get; set; }
		QueryContainer IFunctionScoreQuery.Query { get; set; }
		FunctionScoreMode? IFunctionScoreQuery.ScoreMode { get; set; }
		FunctionBoostMode? IFunctionScoreQuery.BoostMode { get; set; }
		double? IFunctionScoreQuery.MaxBoost { get; set; }
		double? IFunctionScoreQuery.MinScore { get; set; }

		public FunctionScoreQueryDescriptor<T> ConditionlessWhen(bool isConditionless) => Assign(a => _forcedConditionless = isConditionless);

		public FunctionScoreQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.Query = selector?.Invoke(new QueryContainerDescriptor<T>()));

		public FunctionScoreQueryDescriptor<T> Functions(Func<ScoreFunctionsDescriptor<T>, IPromise<IList<IScoreFunction>>> functions) =>
			Assign(a => a.Functions = functions?.Invoke(new ScoreFunctionsDescriptor<T>())?.Value);

		public FunctionScoreQueryDescriptor<T> Functions(IEnumerable<IScoreFunction> functions) => Assign(a => a.Functions = functions);

		public FunctionScoreQueryDescriptor<T> ScoreMode(FunctionScoreMode? mode) => Assign(a => a.ScoreMode = mode);

		public FunctionScoreQueryDescriptor<T> BoostMode(FunctionBoostMode? mode) => Assign(a => a.BoostMode = mode);

		public FunctionScoreQueryDescriptor<T> MaxBoost(double? maxBoost) => Assign(a => a.MaxBoost = maxBoost);

		public FunctionScoreQueryDescriptor<T> MinScore(double? minScore) => Assign(a => a.MinScore = minScore);
	}
}