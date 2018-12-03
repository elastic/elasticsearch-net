using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(FunctionScoreQuery))]
	public interface IFunctionScoreQuery : IQuery
	{
		[DataMember(Name = "boost_mode")]
		FunctionBoostMode? BoostMode { get; set; }

		[DataMember(Name = "functions")]
		IEnumerable<IScoreFunction> Functions { get; set; }

		[DataMember(Name = "max_boost")]
		double? MaxBoost { get; set; }

		[DataMember(Name = "min_score")]
		double? MinScore { get; set; }

		[DataMember(Name = "query")]
		QueryContainer Query { get; set; }

		[DataMember(Name = "score_mode")]
		FunctionScoreMode? ScoreMode { get; set; }
	}

	public class FunctionScoreQuery : QueryBase, IFunctionScoreQuery
	{
		public FunctionBoostMode? BoostMode { get; set; }
		public IEnumerable<IScoreFunction> Functions { get; set; }
		public double? MaxBoost { get; set; }
		public double? MinScore { get; set; }
		public QueryContainer Query { get; set; }
		public FunctionScoreMode? ScoreMode { get; set; }
		protected override bool Conditionless => IsConditionless(this);

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
		FunctionBoostMode? IFunctionScoreQuery.BoostMode { get; set; }
		IEnumerable<IScoreFunction> IFunctionScoreQuery.Functions { get; set; }
		double? IFunctionScoreQuery.MaxBoost { get; set; }
		double? IFunctionScoreQuery.MinScore { get; set; }
		QueryContainer IFunctionScoreQuery.Query { get; set; }
		FunctionScoreMode? IFunctionScoreQuery.ScoreMode { get; set; }

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
