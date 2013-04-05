using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class MissingFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.MissingFilterBuilder;
        private readonly string _name;
        private string _filterName;

        public MissingFilterBuilder(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Sets the filter name for the filter that can be used when searching for matched_filters per hit.
        /// </summary>
        /// <param name="filterName"></param>
        /// <returns></returns>
        public MissingFilterBuilder FilterName(string filterName)
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