using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;
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

		[JsonProperty(PropertyName = "score_mode")]
		[JsonConverter(typeof (StringEnumConverter))]
		FunctionScoreMode? ScoreMode { get; set; }

		[JsonProperty(PropertyName = "boost_mode")]
		[JsonConverter(typeof (StringEnumConverter))]
		FunctionBoostMode? BoostMode { get; set; }

		[JsonProperty(PropertyName = "random_score")]
		IRandomScoreFunction RandomScore { get; set; }

		[JsonProperty(PropertyName = "script_score")]
		IScriptFilter ScriptScore { get; set; }
	}

	public class FunctionScoreQuery : PlainQuery, IFunctionScoreQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.FunctionScore = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public IEnumerable<IFunctionScoreFunction> Functions { get; set; }
		public IQueryContainer Query { get; set; }
		public FunctionScoreMode? ScoreMode { get; set; }
		public FunctionBoostMode? BoostMode { get; set; }
		public IRandomScoreFunction RandomScore { get; set; }
		public IScriptFilter ScriptScore { get; set; }
	}

	public class FunctionScoreQueryDescriptor<T> : IFunctionScoreQuery where T : class
	{
		IEnumerable<IFunctionScoreFunction> IFunctionScoreQuery.Functions { get; set; }

		IQueryContainer IFunctionScoreQuery.Query { get; set; }

		FunctionScoreMode? IFunctionScoreQuery.ScoreMode { get; set; }

		FunctionBoostMode? IFunctionScoreQuery.BoostMode { get; set; }

		IRandomScoreFunction IFunctionScoreQuery.RandomScore { get; set; }

		IScriptFilter IFunctionScoreQuery.ScriptScore { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return (((IFunctionScoreQuery)this).Query == null || ((IFunctionScoreQuery)this).Query.IsConditionless) 
					&& ((IFunctionScoreQuery)this).RandomScore == null 
					&& ((IFunctionScoreQuery)this).ScriptScore == null 
					&& !((IFunctionScoreQuery)this).Functions.HasAny();
			}
		}

		public FunctionScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);
			((IFunctionScoreQuery)this).Query = q.IsConditionless ? null :q;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> Functions(params Func<FunctionScoreFunctionsDescriptor<T>, FunctionScoreFunction<T>>[] functions)
		{
			var descriptor = new FunctionScoreFunctionsDescriptor<T>();

			foreach (var f in functions)
			{
				f(descriptor);
			}

			((IFunctionScoreQuery)this).Functions = descriptor;

			return this;
		}

		public FunctionScoreQueryDescriptor<T> ScoreMode(FunctionScoreMode mode)
		{
			((IFunctionScoreQuery)this).ScoreMode = mode;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> BoostMode(FunctionBoostMode mode)
		{
			((IFunctionScoreQuery)this).BoostMode = mode;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> RandomScore(int? seed = null)
		{
			((IFunctionScoreQuery)this).RandomScore = new RandomScoreFunction();
			if (seed.HasValue)
			{
				((IFunctionScoreQuery)this).RandomScore.Seed = seed.Value;
			}
			return this;
		}

		public FunctionScoreQueryDescriptor<T> ScriptScore(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var descriptor = new ScriptFilterDescriptor();
			if (scriptSelector != null)
				scriptSelector(descriptor);

			((IFunctionScoreQuery)this).ScriptScore = descriptor;

			return this;
		}
	}
}