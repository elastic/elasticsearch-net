using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	//More info about it http://www.elasticsearch.org/guide/en/elasticsearch/reference/master/query-dsl-function-score-query.html
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
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }

		[JsonProperty(PropertyName = "score_mode")]
		[JsonConverter(typeof (StringEnumConverter))]
		FunctionScoreMode? ScoreMode { get; set; }

		[JsonProperty(PropertyName = "boost_mode")]
		[JsonConverter(typeof (StringEnumConverter))]
		FunctionBoostMode? BoostMode { get; set; }

		[JsonProperty("max_boost")]
		float? MaxBoost { get; set; }

		[JsonProperty(PropertyName = "min_score")]
		float? MinScore { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

	}

	public class FunctionScoreQuery : PlainQuery, IFunctionScoreQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.FunctionScore = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public string Name { get; set; }
		public IEnumerable<IFunctionScoreFunction> Functions { get; set; }
		public IQueryContainer Query { get; set; }
		public IFilterContainer Filter { get; set; }
		public FunctionScoreMode? ScoreMode { get; set; }
		public FunctionBoostMode? BoostMode { get; set; }
		public float? MaxBoost { get; set; }
		public double? Boost { get; set; }
		public float? MinScore { get; set; }
	}

	public class FunctionScoreQueryDescriptor<T> : IFunctionScoreQuery where T : class
	{
		private IFunctionScoreQuery Self { get { return this; }}

		IEnumerable<IFunctionScoreFunction> IFunctionScoreQuery.Functions { get; set; }

		IQueryContainer IFunctionScoreQuery.Query { get; set; }

		IFilterContainer IFunctionScoreQuery.Filter { get; set; }

		FunctionScoreMode? IFunctionScoreQuery.ScoreMode { get; set; }

		FunctionBoostMode? IFunctionScoreQuery.BoostMode { get; set; }

		float? IFunctionScoreQuery.MaxBoost { get; set; }

		float? IFunctionScoreQuery.MinScore { get; set; }

		double? IFunctionScoreQuery.Boost { get; set; }

		string IQuery.Name { get; set; }

		private bool _forcedConditionless = false;

		bool IQuery.IsConditionless
		{
			get
			{
				return _forcedConditionless
				       || ((Self.Query == null || Self.Query.IsConditionless) && (Self.Filter == null || Self.Filter.IsConditionless)
				            && !Self.Functions.HasAny());
			}
		}

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

		public FunctionScoreQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
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

		public FunctionScoreQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
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

		public FunctionScoreQueryDescriptor<T> MinScore(float minScore)
		{
			Self.MinScore = minScore;
			return this;
		}
	}
}