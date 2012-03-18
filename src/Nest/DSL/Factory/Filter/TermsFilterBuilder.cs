using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Filter
{
    public class TermsFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.TermsFilterBuilder;
        private readonly string _name;
        private readonly object[] _values;
        private bool _cache;
        private string _cacheKey;
        private string _filterName;
        private string _execution;

        /// <summary>
        /// A filter for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        public TermsFilterBuilder(string name, params string[] values) : this(name, (object[])values) { }


        /// <summary>
        /// A filter for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        public TermsFilterBuilder(string name, params int[] values)
        {
            _name = name;
            _values = values.Select(t => (object)t).ToArray();
        }

        /// <summary>
        /// A filter for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        public TermsFilterBuilder(string name, params long[] values)
        {
            _name = name;
            _values = values.Select(t => (object)t).ToArray();
        }

        /// <summary>
        /// A filter for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        public TermsFilterBuilder(string name, params double[] values)
        {
            _name = name;
            _values = values.Select(t => (object)t).ToArray();
        }

        /// <summary>
        /// A filter for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        public TermsFilterBuilder(string name, params float[] values)
        {
            _name = name;
            _values = values.Select(t => (object)t).ToArray();
        }

        /// <summary>
        /// A filter for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        public TermsFilterBuilder(string name, params object[] values)
        {
            _name = name;
            _values = values;
        }

        public TermsFilterBuilder(string name, IEnumerable<object> values)
        {
            _name = name;
            _values = values.ToArray();
        }

        /// <summary>
        /// Sets the execution mode for the terms filter. Can be either "plain", "bool"
        /// "and". Defaults to "plain".
        /// </summary>
        /// <param name="execution"></param>
        /// <returns></returns>
        public TermsFilterBuilder Execution(string execution)
        {
            _execution = execution;
            return this;
        }

        public TermsFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        /// <summary>
        /// Should the filter be cached or not. Defaults to <tt>false</tt>.
        /// </summary>
        /// <param name="cache"></param>
        /// <returns></returns>
        public TermsFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public TermsFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject(new JProperty(_name, new JArray(_values)))));

            if (_execution != null)
            {
                content[NAME]["execution"] = _execution;
            }

            if (_filterName != null)
            {
                content[NAME]["_name"] = _filterName;
            }

            if (_cache)
            {
                content[NAME]["_cache"] = _cache;
            }

            if (_cacheKey != null)
            {
                content[NAME]["_cache_key"] = _cacheKey;
            }

            return content;
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        #endregion
    }
}