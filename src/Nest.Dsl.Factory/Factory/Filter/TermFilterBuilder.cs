using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class TermFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.TermFilterBuilder;
        private readonly string _name;
        private readonly object _value;
        private bool _cache;
        private string _cacheKey;
        private string _filterName;

        public TermFilterBuilder(string name, string value) : this(name, (object) value)
        {
        }

        public TermFilterBuilder(string name, int value) : this(name, (object) value)
        {
        }

        public TermFilterBuilder(string name, long value) : this(name, (object) value)
        {
        }

        public TermFilterBuilder(string name, float value) : this(name, (object) value)
        {
        }

        public TermFilterBuilder(string name, double value) : this(name, (object) value)
        {
        }

        public TermFilterBuilder(string name, object value)
        {
            _name = name;
            _value = value;
        }

        public TermFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        public TermFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public TermFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject(new JProperty(_name, _value))));

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