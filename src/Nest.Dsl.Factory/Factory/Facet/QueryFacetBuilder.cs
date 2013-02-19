using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class QueryFacetBuilder : AbstractFacetBuilder
    {
        private const string NAME = NameRegistry.QueryFacetBuilder;
        private IQueryBuilder _query;

        public QueryFacetBuilder(string name) : base(name) { }

        public QueryFacetBuilder Query(IQueryBuilder query)
        {
            _query = query;
            return this;
        }

        /// <summary>
        /// Should the facet run in global mode (not bounded by the search query) or not (bounded by
        /// the search query). Defaults to <tt>false</tt>.
        /// </summary>
        /// <param name="global"></param>
        /// <returns></returns>
        public new QueryFacetBuilder Global(bool global)
        {
            base.Global(global);
            return this;
        }

        /// <summary>
        /// Marks the facet to run in a specific scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public new QueryFacetBuilder Scope(string scope)
        {
            base.Scope(scope);
            return this;
        }

        /// <summary>
        /// An additional filter used to further filter down the set of documents the facet will run on.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public new QueryFacetBuilder FacetFilter(IFilterBuilder filter)
        {
            base.FacetFilter(filter);
            return this;
        }

        /// <summary>
        /// Sets the nested path the facet will execute on. A match (root object) will then cause all the
        /// nested objects matching the path to be computed into the facet.
        /// </summary>
        /// <param name="nested"></param>
        /// <returns></returns>
        public new QueryFacetBuilder Nested(string nested)
        {
            base.Nested(nested);
            return this;
        }

        public override object ToJsonObject()
        {
            if(_query == null)
            {
                throw new SearchBuilderException("query must be set on query facet for facet [" + _name + "]");
            }

            var content = new JObject(new JProperty(NAME, _query.ToJsonObject()));

            content = (JObject)AddFilterFacetAndGlobal(content);

            return content;
        }
    }
}
