using Newtonsoft.Json.Linq;
using Nest.FactoryDsl.Filter;

namespace Nest.FactoryDsl.Facet
{
    public abstract class AbstractFacetBuilder
    {
        protected IFilterBuilder _facetFilter;
        protected readonly string _name;
        protected string _nested;
        private string _scope;

        protected AbstractFacetBuilder(string name)
        {
            _name = name;
        }

        public string Name
        {
			
            get { return _name; }
        }

        protected AbstractFacetBuilder FacetFilter(IFilterBuilder filter)
        {
            _facetFilter = filter;
            return this;
        }

        protected AbstractFacetBuilder Nested(string nested)
        {
            _nested = nested;
            return this;
        }

        protected AbstractFacetBuilder Global(bool globalScope)
        {
            _scope = NameRegistry.AbstractFacetBuilder;
            return this;
        }

        protected AbstractFacetBuilder Scope(string scope)
        {
            _scope = scope;
            return this;
        }

        protected object AddFilterFacetAndGlobal(object passedObject)
        {
            var content = (JObject) passedObject;

            if (_facetFilter != null)
            {
                content["facet_filter"] = _facetFilter.ToJsonObject() as JObject;
            }

            if (_nested != null)
            {
                content["nested"] = _nested;
            }

            if (_scope != null)
            {
                content["scope"] = _scope;
            }

            return content;
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        public abstract object ToJsonObject();
    }
}