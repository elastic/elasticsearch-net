using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Filter
{
    public class ExistsFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.ExistsFilterBuilder;
        private readonly string _name;
        private string _filterName;


        public ExistsFilterBuilder(string name)
        {
            _name = name;
        }

        public ExistsFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject(new JProperty("field", _name))));

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