using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class PrefixFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.PrefixFilterBuilder;
        private readonly string _name;
        private readonly string _prefix;
        private bool _cache;
        private string _cacheKey;
        private string _filterName;

        public PrefixFilterBuilder(string name, string prefix)
        {
            _name = name;
            _prefix = prefix;
        }

        public PrefixFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        public PrefixFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public PrefixFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject(new JProperty(_name, _prefix))));

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