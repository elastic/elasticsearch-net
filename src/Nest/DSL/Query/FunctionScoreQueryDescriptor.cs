using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	//More info about it http://www.elasticsearch.org/guide/en/elasticsearch/reference/master/query-dsl-function-score-query.html
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFunctionScoreQuery
	{
		[JsonProperty(PropertyName = "functions")]
		IEnumerable<IFunctionScoreFunction> _Functions { get; set; }

		[JsonProperty(PropertyName = "query")]
		IQueryDescriptor _Query { get; set; }

		[JsonProperty(PropertyName = "score_mode")]
		[JsonConverter(typeof (StringEnumConverter))]
		FunctionScoreMode? _ScoreMode { get; set; }

		[JsonProperty(PropertyName = "boost_mode")]
		[JsonConverter(typeof (StringEnumConverter))]
		FunctionBoostMode? _BoostMode { get; set; }

		[JsonProperty(PropertyName = "random_score")]
		RandomScoreFunction _RandomScore { get; set; }

		[JsonProperty(PropertyName = "script_score")]
		ScriptFilterDescriptor _ScriptScore { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreQueryDescriptor<T> : IQuery, IFunctionScoreQuery where T : class
	{
		IEnumerable<IFunctionScoreFunction> IFunctionScoreQuery._Functions { get; set; }

		IQueryDescriptor IFunctionScoreQuery._Query { get; set; }

		FunctionScoreMode? IFunctionScoreQuery._ScoreMode { get; set; }

		FunctionBoostMode? IFunctionScoreQuery._BoostMode { get; set; }

		RandomScoreFunction IFunctionScoreQuery._RandomScore { get; set; }

		ScriptFilterDescriptor IFunctionScoreQuery._ScriptScore { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return (((IFunctionScoreQuery)this)._Query == null || ((IFunctionScoreQuery)this)._Query.IsConditionless) 
					&& ((IFunctionScoreQuery)this)._RandomScore == null 
					&& ((IFunctionScoreQuery)this)._ScriptScore == null 
					&& !((IFunctionScoreQuery)this)._Functions.HasAny();
			}
		}

		public FunctionScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			((IFunctionScoreQuery)this)._Query = q;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> Functions(params Func<FunctionScoreFunctionsDescriptor<T>, FunctionScoreFunction<T>>[] functions)
		{
			var descriptor = new FunctionScoreFunctionsDescriptor<T>();

			foreach (var f in functions)
			{
				f(descriptor);
			}

			((IFunctionScoreQuery)this)._Functions = descriptor;

			return this;
		}

		public FunctionScoreQueryDescriptor<T> ScoreMode(FunctionScoreMode mode)
		{
			((IFunctionScoreQuery)this)._ScoreMode = mode;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> BoostMode(FunctionBoostMode mode)
		{
			((IFunctionScoreQuery)this)._BoostMode = mode;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> RandomScore(int? seed = null)
		{
			((IFunctionScoreQuery)this)._RandomScore = new RandomScoreFunction();
			if (seed.HasValue)
			{
				((IFunctionScoreQuery)this)._RandomScore._Seed = seed.Value;
			}
			return this;
		}

		public FunctionScoreQueryDescriptor<T> ScriptScore(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var descriptor = new ScriptFilterDescriptor();
			if (scriptSelector != null)
				scriptSelector(descriptor);

			((IFunctionScoreQuery)this)._ScriptScore = descriptor;

			return this;
		}
	}
}