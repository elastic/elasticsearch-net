using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Filter
{
    public class OrFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.OrFilterBuilder;
        private readonly List<IFilterBuilder> _filters = new List<IFilterBuilder>();
        private bool _cache;
        private string _cacheKey;
        private string _filterName;

        public OrFilterBuilder(params IFilterBuilder[] filters)
        {
            _filters.AddRange(filters);
        }

        /// <summary>
        /// Adds a filter to the list of filters to "or".
        /// </summary>
        /// <param name="filterBuilder"></param>
        /// <returns></returns>
        public OrFilterBuilder Add(IFilterBuilder filterBuilder)
        {
            _filters.Add(filterBuilder);
            return this;
        }

        public OrFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        /// <summary>
        /// Should the filter be cached or not. Defaults to false.
        /// </summary>
        /// <param name="cache"></param>
        /// <returns></returns>
        public OrFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public OrFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME]["filters"] = new JArray(_filters.Select(t => t.ToJsonObject()));

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