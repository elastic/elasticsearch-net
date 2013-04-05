using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class AndFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.AndFilterBuilder;
        private readonly List<IFilterBuilder> _filters = new List<IFilterBuilder>();
        private bool _cache;
        private string _cacheKey;
        private string _filterName;

        public AndFilterBuilder(IEnumerable<IFilterBuilder> filters)
        {
            _filters.AddRange(filters);
        }

        public AndFilterBuilder(params IFilterBuilder[] filters)
        {
            _filters.AddRange(filters);
        }

        public AndFilterBuilder Add(IFilterBuilder filterBuilder)
        {
            _filters.Add(filterBuilder);
            return this;
        }

        public AndFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public AndFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        public AndFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject();

            content["and"] = new JObject();

            content["and"]["filters"] = new JArray(_filters.Select(t => t.ToJsonObject()));

            if (_cache)
            {
                content["and"]["_cache"] = _cache;
            }

            if (_cacheKey != null)
            {
                content["and"]["_cache_key"] = _cacheKey;
            }

            if (_filterName != null)
            {
                content["and"]["_name"] = _filterName;
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