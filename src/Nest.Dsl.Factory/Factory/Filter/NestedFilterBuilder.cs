using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class NestedFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.NestedFilterBuilder;
        private readonly IFilterBuilder _filterBuilder;
        private readonly string _path;
        private readonly IQueryBuilder _queryBuilder;
        private bool _cache;
        private string _cacheKey;
        private string _filterName;
        private string _scope;

        public NestedFilterBuilder(string path, IQueryBuilder queryBuilder)
        {
            _path = path;
            _queryBuilder = queryBuilder;
            _filterBuilder = null;
        }

        public NestedFilterBuilder(string path, IFilterBuilder filterBuilder)
        {
            _path = path;
            _queryBuilder = null;
            _filterBuilder = filterBuilder;
        }

        public NestedFilterBuilder Scope(string scope)
        {
            _scope = scope;
            return this;
        }

        public NestedFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        public NestedFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public NestedFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            if (_queryBuilder != null)
            {
                content[NAME]["query"] = _queryBuilder.ToJsonObject() as JObject;
            }
            else
            {
                content[NAME]["filter"] = _filterBuilder.ToJsonObject() as JObject;
            }

            content[NAME]["path"] = _path;

            if (_scope != null)
            {
                content[NAME]["_scope"] = _scope;
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