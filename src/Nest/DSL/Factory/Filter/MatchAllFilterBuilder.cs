using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Filter
{
    public class MatchAllFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.MatchAllFilterBuilder;

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            return new JObject(new JProperty(NAME, new JObject()));
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        #endregion
    }
}