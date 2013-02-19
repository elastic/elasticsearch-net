using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class StatisticalFacetBuilder : AbstractFacetBuilder
    {
        private const string NAME = NameRegistry.StatisticalFacetBuilder;
        private string[] _fieldsNames;
        private string _fieldName;

        public StatisticalFacetBuilder(string name) : base(name) { }

        public StatisticalFacetBuilder Field(string field)
        {
            _fieldName = field;
            return this;
        }

        /// <summary>
        /// The fields the terms will be collected from.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public StatisticalFacetBuilder Fields(params string[] fields)
        {
            _fieldsNames = fields;
            return this;
        }

        /// <summary>
        /// Should the facet run in global mode (not bounded by the search query) or not (bounded by
        /// the search query). Defaults to <tt>false</tt>.
        /// </summary>
        /// <param name="global"></param>
        /// <returns></returns>
        public new StatisticalFacetBuilder Global(bool global)
        {
            base.Global(global);
            return this;
        }

        /// <summary>
        /// Marks the facet to run in a specific scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public new StatisticalFacetBuilder Scope(string scope)
        {
            base.Scope(scope);
            return this;
        }

        /// <summary>
        /// An additional filter used to further filter down the set of documents the facet will run on.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public new StatisticalFacetBuilder FacetFilter(IFilterBuilder filter)
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
        public new StatisticalFacetBuilder Nested(string nested)
        {
            base.Nested(nested);
            return this;
        }

        public override object ToJsonObject()
        {
            if(_fieldName == null && _fieldsNames == null)
            {
                throw new SearchBuilderException("field must be set on statistical facet for facet [" + _name + "]");
            }

            var content = new JObject(new JProperty(NAME, new JObject()));

            if(_fieldsNames != null)
            {
                if(_fieldsNames.Length == 1)
                {
                    content[NAME]["field"] = _fieldsNames[0];
                }
                else
                {
                   content[NAME]["fields"] = new JArray(_fieldsNames);   
                }
            }
            else
            {
                content[NAME]["field"] = _fieldName;
            }

            content = (JObject)AddFilterFacetAndGlobal(content);

            return content;
        }
    }
}
