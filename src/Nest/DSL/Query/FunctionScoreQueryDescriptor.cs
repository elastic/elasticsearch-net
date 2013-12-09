using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class FunctionScoreQueryDescriptor<T> : IQuery where T : class
    {
        [JsonProperty(PropertyName = "functions")]
        internal IEnumerable<FunctionScoreFunction<T>> _Functions { get; set; }

        [JsonProperty(PropertyName = "query")]
        internal BaseQuery _Query { get; set; }

        [JsonProperty(PropertyName = "score_mode")]
        [JsonConverter(typeof(StringEnumConverter))]
        FunctionScoreMode _ScoreMode { get; set; }


		bool IQuery.IsConditionless
        {
            get
            {
                return this._Query == null || this._Query.IsConditionless;
            }
        }

        public FunctionScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
        {
            querySelector.ThrowIfNull("querySelector");
            var query = new QueryDescriptor<T>();
            var q = querySelector(query);

            this._Query = q;
            return this;
        }

        public FunctionScoreQueryDescriptor<T> Functions(params Func<FunctionScoreFunctionsDescriptor<T>, FunctionScoreFunction<T>>[] functions)
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

    public class FunctionScoreFunctionsDescriptor<T> : IEnumerable<FunctionScoreFunction<T>>
    {
        internal List<FunctionScoreFunction<T>>  _Functions { get; set; }

        public FunctionScoreFunctionsDescriptor()
        {
            this._Functions = new List<FunctionScoreFunction<T>>();
        }

        public FunctionScoreFunction<T> Gauss(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> db)
        {
            var fn = new GaussFunction<T>(objectPath, db);
            this._Functions.Add(fn);
            return fn;
        }

        public FunctionScoreFunction<T> Linear(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> db)
        {
            var fn = new LinearFunction<T>(objectPath, db);
            this._Functions.Add(fn);
            return fn;
        }

        public FunctionScoreFunction<T> Exp(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> db)
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
    public class GaussFunction<T> : FunctionScoreDecayFunction<T>
    {
        [JsonProperty(PropertyName = "gauss")]
        [JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
        internal IDictionary<string, FunctionScoreDecayFieldDescriptor> _GaussDescriptor { get; set; }

        public GaussFunction(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
        {
            _GaussDescriptor = new Dictionary<string, FunctionScoreDecayFieldDescriptor>();

            var resolver = new PropertyNameResolver();
            var fieldName = resolver.Resolve(objectPath);
            var descriptor = new FunctionScoreDecayFieldDescriptor();
            descriptorBuilder(descriptor);
            _GaussDescriptor[fieldName] = descriptor;
        }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class LinearFunction<T> : FunctionScoreDecayFunction<T>
    {
        [JsonProperty(PropertyName = "linear")]
        [JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
        internal IDictionary<string, FunctionScoreDecayFieldDescriptor> _LinearDescriptor { get; set; }

        public LinearFunction(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
        {
            _LinearDescriptor = new Dictionary<string, FunctionScoreDecayFieldDescriptor>();

            var resolver = new PropertyNameResolver();
            var fieldName = resolver.Resolve(objectPath);
            var descriptor = new FunctionScoreDecayFieldDescriptor();
            descriptorBuilder(descriptor);
            _LinearDescriptor[fieldName] = descriptor;
        }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ExpFunction<T> : FunctionScoreDecayFunction<T>
    {
        [JsonProperty(PropertyName = "exp")]
        [JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
        internal IDictionary<string, FunctionScoreDecayFieldDescriptor> _ExpDescriptor { get; set; }

        public ExpFunction(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
        {
            _ExpDescriptor = new Dictionary<string, FunctionScoreDecayFieldDescriptor>();

            var resolver = new PropertyNameResolver();
            var fieldName = resolver.Resolve(objectPath);
            var descriptor = new FunctionScoreDecayFieldDescriptor();
            descriptorBuilder(descriptor);
            _ExpDescriptor[fieldName] = descriptor;
        }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class BoostFactorFunction<T> : FunctionScoreFunction<T>
    {
        [JsonProperty(PropertyName = "boost_factor")]
        internal double _BoostFactor { get; set; }

        public BoostFactorFunction(double boostFactor)
        {
            _BoostFactor = boostFactor;
        }
    }
}
