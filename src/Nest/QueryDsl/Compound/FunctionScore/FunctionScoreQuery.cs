using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<FunctionScoreQueryDescriptor<object>>))]
	public interface IFunctionScoreQuery : IQuery
	{
		[JsonProperty(PropertyName = "functions")]
		IEnumerable<IFunctionScoreFunction> Functions { get; set; }

		[JsonProperty(PropertyName = "query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		IQueryContainer Filter { get; set; }

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
		public IQueryContainer Query { get; set; }
		public IQueryContainer Filter { get; set; }
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

	public class FunctionScoreQueryDescriptor<T> : IFunctionScoreQuery where T : class
	{
		private IFunctionScoreQuery Self { get { return this; }}
		string IQuery.Name { get; set; }
		private bool _forcedConditionless = false;
		bool IQuery.Conditionless => FunctionScoreQuery.IsConditionless(this, _forcedConditionless);
		IEnumerable<IFunctionScoreFunction> IFunctionScoreQuery.Functions { get; set; }
		IQueryContainer IFunctionScoreQuery.Query { get; set; }
		IQueryContainer IFunctionScoreQuery.Filter { get; set; }
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

		public FunctionScoreQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);
			Self.Query = q.IsConditionless ? null : q;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> Filter(Func<QueryDescriptor<T>, QueryContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new QueryDescriptor<T>();
			var f = filterSelector(filter);
			Self.Filter = f.IsConditionless ? null : f;
			return this;
		} 

		public FunctionScoreQueryDescriptor<T> Functions(params Func<FunctionScoreFunctionsDescriptor<T>, FunctionScoreFunction<T>>[] functions)
		{
			var descriptor = new FunctionScoreFunctionsDescriptor<T>();

			foreach (var f in functions)
			{
				f(descriptor);
			}

			Self.Functions = descriptor;

			return this;
		}

		public FunctionScoreQueryDescriptor<T> Functions(IEnumerable<IFunctionScoreFunction> functions)
		{
			Self.Functions = functions;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> ScoreMode(FunctionScoreMode mode)
		{
			Self.ScoreMode = mode;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> BoostMode(FunctionBoostMode mode)
		{
			Self.BoostMode = mode;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> MaxBoost(float maxBoost)
		{
			Self.MaxBoost = maxBoost;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> RandomScore(int? seed = null)
		{
			Self.RandomScore = new RandomScoreFunction();
			if (seed.HasValue)
			{
				Self.RandomScore.Seed = seed.Value;
			}
			return this;
		}

		public FunctionScoreQueryDescriptor<T> ScriptScore(Func<ScriptQueryDescriptor<T>, IScriptQuery> scriptSelector)
		{
			Self.ScriptScore = scriptSelector?.Invoke(new ScriptQueryDescriptor<T>());
			return this;
		}

		public FunctionScoreQueryDescriptor<T> MinScore(float minScore)
		{
			Self.MinScore = minScore;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> Weight(double weight)
		{
			Self.Weight = weight;
			return this;
		}
	}
}