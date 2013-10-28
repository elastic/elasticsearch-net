using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest.DSL.Query
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class FunctionScoreQueryDescriptor<T> : IQuery where T : class
    {
        [JsonProperty(PropertyName = "query")]
        internal BaseQuery _Query { get; set; }

        [JsonProperty(PropertyName = "functions")]
        internal IEnumerable<FunctionScoreFunction<T>> _Functions { get; set; }

        internal bool IsConditionless
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
    }

    public class FunctionScoreFunctionsDescriptor<T> : IEnumerable<FunctionScoreFunction<T>>
    {
        internal List<FunctionScoreFunction<T>>  _Functions { get; set; }

        public FunctionScoreFunctionsDescriptor()
        {
            this._Functions = new List<FunctionScoreFunction<T>>();
        }

        public FunctionScoreFunction<T> Gauss(Expression<Func<T, object>> objectPath, string scale)
        {
            var fn = new GaussFunction<T>(objectPath, scale);
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

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class GaussFunction<T> : FunctionScoreFunction<T>
    {
        [JsonProperty(PropertyName = "gauss")]
        [JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
        internal IDictionary<string, object> _GaussDescriptor { get; set; }

        public GaussFunction(Expression<Func<T, object>> objectPath, string scale)
        {
            _GaussDescriptor = new Dictionary<string, object>();

            var resolver = new PropertyNameResolver();
            var fieldName = resolver.Resolve(objectPath);
            _GaussDescriptor[fieldName] = new { scale = scale};
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
