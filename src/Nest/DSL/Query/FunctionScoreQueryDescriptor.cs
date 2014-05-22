using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	//More info about it http://www.elasticsearch.org/guide/en/elasticsearch/reference/master/query-dsl-function-score-query.html
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "functions")]
		internal IEnumerable<FunctionScoreFunction<T>> _Functions { get; set; }

		[JsonProperty(PropertyName = "query")]
		internal BaseQuery _Query { get; set; }

		[JsonProperty(PropertyName = "score_mode")]
		[JsonConverter(typeof (StringEnumConverter))]
		private FunctionScoreMode? _ScoreMode { get; set; }

		[JsonProperty(PropertyName = "boost_mode")]
		[JsonConverter(typeof (StringEnumConverter))]
		private FunctionBoostMode? _BoostMode { get; set; }

		[JsonProperty(PropertyName = "random_score")]
		private RandomScoreFunction _RandomScore { get; set; }

		[JsonProperty(PropertyName = "script_score")]
		private ScriptFilterDescriptor _ScriptScore { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return (this._Query == null || this._Query.IsConditionless) && _RandomScore == null && _ScriptScore == null &&
				       (_Functions == null || !_Functions.Any());
			}
		}

		public FunctionScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			this._Query = q.IsConditionless ? null : q;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> Functions(
			params Func<FunctionScoreFunctionsDescriptor<T>, FunctionScoreFunction<T>>[] functions)
		{
			var descriptor = new FunctionScoreFunctionsDescriptor<T>();

			foreach (var f in functions)
			{
				f(descriptor);
			}

			_Functions = descriptor;

			return this;
		}

		public FunctionScoreQueryDescriptor<T> ScoreMode(FunctionScoreMode mode)
		{
			this._ScoreMode = mode;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> BoostMode(FunctionBoostMode mode)
		{
			this._BoostMode = mode;
			return this;
		}

		public FunctionScoreQueryDescriptor<T> RandomScore(int? seed = null)
		{
			this._RandomScore = new RandomScoreFunction();
			if (seed.HasValue)
			{
				_RandomScore._Seed = seed.Value;
			}
			return this;
		}

		public FunctionScoreQueryDescriptor<T> ScriptScore(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var descriptor = new ScriptFilterDescriptor();
			if (scriptSelector != null)
				scriptSelector(descriptor);

			this._ScriptScore = descriptor;

			return this;
		}
	}

	public enum FunctionScoreMode
	{
		multiply,
		sum,
		avg,
		first,
		max,
		min
	}

	public enum FunctionBoostMode
	{
		multiply,
		replace,
		sum,
		avg,
		max,
		min
	}

	public class FunctionScoreFunctionsDescriptor<T> : IEnumerable<FunctionScoreFunction<T>> where T : class
	{
		internal List<FunctionScoreFunction<T>> _Functions { get; set; }

		public FunctionScoreFunctionsDescriptor()
		{
			this._Functions = new List<FunctionScoreFunction<T>>();
		}

		public FunctionScoreFunction<T> Gauss(Expression<Func<T, object>> objectPath,
			Action<FunctionScoreDecayFieldDescriptor> db)
		{
			var fn = new GaussFunction<T>(objectPath, db);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Linear(Expression<Func<T, object>> objectPath,
			Action<FunctionScoreDecayFieldDescriptor> db)
		{
			var fn = new LinearFunction<T>(objectPath, db);
			this._Functions.Add(fn);
			return fn;
		}

		public FunctionScoreFunction<T> Exp(Expression<Func<T, object>> objectPath,
			Action<FunctionScoreDecayFieldDescriptor> db)
		{
			var fn = new ExpFunction<T>(objectPath, db);
			this._Functions.Add(fn);
			return fn;
		}

		public BoostFactorFunction<T> BoostFactor(double value)
		{
			var fn = new BoostFactorFunction<T>(value);
			this._Functions.Add(fn);
			return fn;
		}

		public FieldValueFactor<T> FieldValueFactor(Action<FieldValueFactorDescriptor<T>> db)
		{
			var fn = new FieldValueFactor<T>(db);
			this._Functions.Add(fn);
			return fn;
		}

		public ScriptScoreFunction<T> ScriptScore(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var fn = new ScriptScoreFunction<T>(scriptSelector);
			this._Functions.Add(fn);
			return fn;
		}

		public IEnumerator<FunctionScoreFunction<T>> GetEnumerator()
		{
			return _Functions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _Functions.GetEnumerator();
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreFunction<T>
	{
	}

	public class FieldValueFactorDescriptor<T>
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }

		[JsonProperty("factor")]
		internal double _Factor { get; set; }

		[JsonProperty("modifier")]
		[JsonConverter(typeof (StringEnumConverter))]
		internal FieldValueFactorModifier _Modifier { get; set; }

		public FieldValueFactorDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}

		public FieldValueFactorDescriptor<T> Factor(double factor)
		{
			this._Factor = factor;
			return this;
		}

		public FieldValueFactorDescriptor<T> Modifier(FieldValueFactorModifier modifier)
		{
			this._Modifier = modifier;
			return this;
		}
	}

	public class FunctionScoreDecayFieldDescriptor
	{
		[JsonProperty(PropertyName = "origin")]
		internal string _Origin { get; set; }

		[JsonProperty(PropertyName = "scale")]
		internal string _Scale { get; set; }

		[JsonProperty(PropertyName = "offset")]
		internal string _Offset { get; set; }

		[JsonProperty(PropertyName = "decay")]
		internal double? _Decay { get; set; }

		public FunctionScoreDecayFieldDescriptor Origin(string origin)
		{
			this._Origin = origin;
			return this;
		}

		public FunctionScoreDecayFieldDescriptor Scale(string scale)
		{
			this._Scale = scale;
			return this;
		}

		public FunctionScoreDecayFieldDescriptor Offset(string offset)
		{
			this._Offset = offset;
			return this;
		}

		public FunctionScoreDecayFieldDescriptor Decay(double? decay)
		{
			this._Decay = decay;
			return this;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreDecayFunction<T> : FunctionScoreFunction<T>
	{
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreFilteredFunction<T> : FunctionScoreFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "filter")]
		internal BaseFilter _Filter { get; set; }

		public FunctionScoreFunction<T> Filter(Func<FilterDescriptor<T>, BaseFilter> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			this._Filter = f;
			return this;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class GaussFunction<T> : FunctionScoreDecayFunction<T>
	{
		[JsonProperty(PropertyName = "gauss")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor> _GaussDescriptor { get; set; }

		public GaussFunction(Expression<Func<T, object>> objectPath,
			Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			_GaussDescriptor = new Dictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor>();

			var descriptor = new FunctionScoreDecayFieldDescriptor();
			descriptorBuilder(descriptor);
			_GaussDescriptor[objectPath] = descriptor;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class LinearFunction<T> : FunctionScoreDecayFunction<T>
	{
		[JsonProperty(PropertyName = "linear")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor> _LinearDescriptor { get; set; }

		public LinearFunction(Expression<Func<T, object>> objectPath,
			Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			_LinearDescriptor = new Dictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor>();

			var descriptor = new FunctionScoreDecayFieldDescriptor();
			descriptorBuilder(descriptor);
			_LinearDescriptor[objectPath] = descriptor;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ExpFunction<T> : FunctionScoreDecayFunction<T>
	{
		[JsonProperty(PropertyName = "exp")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor> _ExpDescriptor { get; set; }

		public ExpFunction(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			_ExpDescriptor = new Dictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor>();

			var descriptor = new FunctionScoreDecayFieldDescriptor();
			descriptorBuilder(descriptor);
			_ExpDescriptor[objectPath] = descriptor;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoostFactorFunction<T> : FunctionScoreFilteredFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "boost_factor")]
		internal double _BoostFactor { get; set; }

		public BoostFactorFunction(double boostFactor)
		{
			_BoostFactor = boostFactor;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FieldValueFactor<T> : FunctionScoreFilteredFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "field_value_factor")]
		internal FieldValueFactorDescriptor<T> _FieldValueFactor { get; set; }

		public FieldValueFactor(Action<FieldValueFactorDescriptor<T>> descriptorBuilder)
		{
			var descriptor = new FieldValueFactorDescriptor<T>();
			descriptorBuilder(descriptor);

			this._FieldValueFactor = descriptor;
		}
	}

	public enum FieldValueFactorModifier
	{
		none,
		log,
		log1p,
		log2p,
		ln,
		ln1p,
		ln2p,
		square,
		sqrt,
		reciprocal
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ScriptScoreFunction<T> : FunctionScoreFilteredFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "script_score")]
		internal ScriptFilterDescriptor _ScriptScore { get; set; }

		public ScriptScoreFunction(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var descriptor = new ScriptFilterDescriptor();
			if (scriptSelector != null)
				scriptSelector(descriptor);

			this._ScriptScore = descriptor;
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RandomScoreFunction
	{
		[JsonProperty(PropertyName = "seed")]
		internal int? _Seed { get; set; }

		public RandomScoreFunction(int seed)
		{
			_Seed = seed;
		}

		public RandomScoreFunction()
		{
		}
	}
}