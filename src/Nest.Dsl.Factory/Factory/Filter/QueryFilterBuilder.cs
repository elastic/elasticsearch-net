using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class QueryFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.QueryFilterBuilder;
        private const string ALTNAME = NameRegistry.QueryFilterBuilderAlt;
        private readonly IQueryBuilder _queryBuilder;
        private bool _cache;
        private string _filterName;

        public QueryFilterBuilder(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public QueryFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        public QueryFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject();

            if (_filterName == null && _cache == false)
            {
                content[NAME] = _queryBuilder.ToJsonObject() as JObject;
            }
            else
            {
                content[ALTNAME] = new JObject();
                content[ALTNAME]["query"] = _queryBuilder.ToJsonObject() as JObject;

                if (_filterName != null)
                {
                    content[ALTNAME]["_name"] = _filterName;
                }

                if (_cache)
                {
                    content[ALTNAME]["_cache"] = _cache;
                }
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