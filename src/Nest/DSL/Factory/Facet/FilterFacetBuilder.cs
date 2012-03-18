using Newtonsoft.Json.Linq;
using Nest.FactoryDsl.Filter;

namespace Nest.FactoryDsl.Facet
{
    public class FilterFacetBuilder : AbstractFacetBuilder
    {
        private const string NAME = NameRegistry.FilterFacetBuilder;
        private IFilterBuilder _filter;

        public FilterFacetBuilder(string name) : base(name) { }

        public new FilterFacetBuilder Filter(IFilterBuilder filter)
        {
            _filter = filter;
            return this;
        }

        /// <summary>
        /// Should the facet run in global mode (not bounded by the search query) or not (bounded by
        /// the search query). Defaults to <tt>false</tt>.
        /// </summary>
        /// <param name="global"></param>
        /// <returns></returns>
        public new FilterFacetBuilder Global(bool global)
        {
            base.Global(global);
            return this;
        }

        /// <summary>
        /// Marks the facet to run in a specific scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public new FilterFacetBuilder Scope(string scope)
        {
            base.Scope(scope);
            return this;
        }

        /// <summary>
        /// An additional filter used to further filter down the set of documents the facet will run on.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public new FilterFacetBuilder FacetFilter(IFilterBuilder filter)
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
        public new FilterFacetBuilder Nested(string nested)
        {
            base.Nested(nested);
            return this;
        }

        public override object ToJsonObject()
        {
            if(_filter == null)
            {
                throw new SearchBuilderException("filter must be set on filter facet for facet [" + _name + "]");
            }

            var content = new JObject(new JProperty(NAME, _filter.ToJsonObject()));       
            content = (JObject)AddFilterFacetAndGlobal(content);
            return content;
        }
    }
}