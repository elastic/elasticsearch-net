using Newtonsoft.Json.Linq;
using Nest.FactoryDsl.Query;

namespace Nest.FactoryDsl.Filter
{
    public class HasChildFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.HasChildFilterBuilder;
        private readonly string _childType;
        private readonly IQueryBuilder _queryBuilder;
        private string _filterName;
        private string _scope;

        public HasChildFilterBuilder(string type, IQueryBuilder queryBuilder)
        {
            _childType = type;
            _queryBuilder = queryBuilder;
        }

        public HasChildFilterBuilder Scope(string scope)
        {
            _scope = scope;
            return this;
        }

        /// <summary>
        /// Sets the filter name for the filter that can be used when searching for matched_filters per hit.
        /// </summary>
        /// <param name="filterName"></param>
        /// <returns></returns>
        public HasChildFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME), new JObject());
            content[NAME]["query"] = _queryBuilder.ToJsonObject() as JObject;

            content[NAME]["type"] = _childType;

            if (_scope != null)
            {
                content[NAME]["_scope"] = _scope;
            }

            if (_filterName != null)
            {
                content[NAME]["_name"] = _filterName;
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