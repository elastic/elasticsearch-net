using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FunctionScoreQueryDescriptor<object>>))]
	public interface IFunctionScoreQuery : IQuery
	{
		[JsonProperty(PropertyName = "functions")]
		IEnumerable<IFunctionScoreFunction> Functions { get; set; }

		[JsonProperty(PropertyName = "query")]
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		QueryContainer Filter { get; set; }

		[JsonProperty(PropertyName = "score_mode")]
		[JsonConverter(typeof (StringEnumConverter))]
		FunctionScoreMode? ScoreMode { get; set; }

		[JsonProperty(PropertyName = "boost_mode")]
		[JsonConverter(typeof (StringEnumConverter))]
		FunctionBoostMode? BoostMode { get; set; }

		[JsonProperty("max_boost")]
		float? MaxBoost { get; set; }

		[JsonProperty(PropertyName = "random_score")]
		IRandomScoreFunction RandomScore { get; set; }

		[JsonProperty(PropertyName = "script_score")]
		IScriptQuery ScriptScore { get; set; }

		[JsonProperty("weight")]
		double? Weight { get; set; }

		[JsonProperty(PropertyName = "min_score")]
		float? MinScore { get; set; }
	}

	public class FunctionScoreQuery : QueryBase, IFunctionScoreQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IEnumerable<IFunctionScoreFunction> Functions { get; set; }
		public QueryContainer Query { get; set; }
		public QueryContainer Filter { get; set; }
		public FunctionScoreMode? ScoreMode { get; set; }
		public FunctionBoostMode? BoostMode { get; set; }
		public float? MaxBoost { get; set; }
		public IRandomScoreFunction RandomScore { get; set; }
		public IScriptQuery ScriptScore { get; set; }
		public double? Weight { get; set; }
		public float? MinScore { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.FunctionScore = this;

		internal static bool IsConditionless(IFunctionScoreQuery q, bool force = false)
		{
			return force
				|| (((q.Query == null || q.Query.IsConditionless) && (q.Filter == null || q.Filter.IsConditionless))
				&& q.RandomScore == null && q.ScriptScore == null && !q.Functions.HasAny());
		}
	}

	public class FunctionScoreQueryDescriptor<T> 
		: QueryDescriptorBase<FunctionScoreQueryDescriptor<T>, IFunctionScoreQuery>
		, IFunctionScoreQuery where T : class
	{
		private bool _forcedConditionless = false;
		bool IQuery.Conditionless => FunctionScoreQuery.IsConditionless(this, _forcedConditionless);
		IEnumerable<IFunctionScoreFunction> IFunctionScoreQuery.Functions { get; set; }
		QueryContainer IFunctionScoreQuery.Query { get; set; }
		QueryContainer IFunctionScoreQuery.Filter { get; set; }
		FunctionScoreMode? IFunctionScoreQuery.ScoreMode { get; set; }
		FunctionBoostMode? IFunctionScoreQuery.BoostMode { get; set; }
		float? IFunctionScoreQuery.MaxBoost { get; set; }
		IRandomScoreFunction IFunctionScoreQuery.RandomScore { get; set; }
		IScriptQuery IFunctionScoreQuery.ScriptScore { get; set; }
		double? IFunctionScoreQuery.Weight { get; set; }
		float? IFunctionScoreQuery.MinScore { get; set; }
		
		public FunctionScoreQueryDescriptor<T> ConditionlessWhen(bool isConditionless)
		{
			this._forcedConditionless = isConditionless;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => Assign(a =>
		{
			selector.ThrowIfNull(nameof(selector));
			var query = new QueryContainerDescriptor<T>();
			var q = selector(query);
			a.Query = q.IsConditionless ? null : q;
		});

		public FunctionScoreQueryDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => Assign(a =>
		{
			selector.ThrowIfNull(nameof(selector));
			var filter = new QueryContainerDescriptor<T>();
			var f = selector(filter);
			a.Filter = f.IsConditionless ? null : f;
		});

		public FunctionScoreQueryDescriptor<T> Functions(params Func<FunctionScoreFunctionsDescriptor<T>, FunctionScoreFunction<T>>[] functions) => Assign(a =>
		{
			var descriptor = new FunctionScoreFunctionsDescriptor<T>();
			foreach (var f in functions)
				f(descriptor);
			a.Functions = descriptor;
		});

		public FunctionScoreQueryDescriptor<T> Functions(IEnumerable<IFunctionScoreFunction> functions) => Assign(a => a.Functions = functions);

		public FunctionScoreQueryDescriptor<T> ScoreMode(FunctionScoreMode mode) => Assign(a => a.ScoreMode = mode);

		public FunctionScoreQueryDescriptor<T> BoostMode(FunctionBoostMode mode) => Assign(a => a.BoostMode = mode);

		public FunctionScoreQueryDescriptor<T> MaxBoost(float maxBoost) => Assign(a => a.MaxBoost = maxBoost);

		public FunctionScoreQueryDescriptor<T> RandomScore(int? seed = null) => Assign(a =>
		{
			a.RandomScore = new RandomScoreFunction();
			if (seed.HasValue)
				a.RandomScore.Seed = seed.Value;
		});

		public FunctionScoreQueryDescriptor<T> ScriptScore(Func<ScriptQueryDescriptor<T>, IScriptQuery> selector) => 
			Assign(a => a.ScriptScore = selector?.Invoke(new ScriptQueryDescriptor<T>()));

		public FunctionScoreQueryDescriptor<T> MinScore(float minScore) => Assign(a => a.MinScore = minScore);

		public FunctionScoreQueryDescriptor<T> Weight(double weight) => Assign(a => a.Weight = weight);
	}
}