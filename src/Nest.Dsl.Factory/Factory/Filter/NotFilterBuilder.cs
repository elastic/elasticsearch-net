using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class NotFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.NotFilterBuilder;
        private readonly IFilterBuilder _filter;
        private bool _cache;
        private string _filterName;

        public NotFilterBuilder(IFilterBuilder filter)
        {
            _filter = filter;
        }

        public NotFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public NotFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME]["filter"] = _filter.ToJsonObject() as JObject;

            if (_filterName != null)
            {
                content[NAME]["_name"] = _filterName;
            }

            if (_cache)
            {
                content[NAME]["_cache"] = _cache;
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