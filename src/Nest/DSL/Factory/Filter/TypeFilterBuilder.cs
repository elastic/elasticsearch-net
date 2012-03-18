using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Filter
{
    public class TypeFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.TypeFilterBuilder;
        private readonly string _type;

        public TypeFilterBuilder(string type)
        {
            _type = type;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            return new JObject(new JProperty(NAME, new JObject(new JProperty("value", _type))));
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        #endregion
    }
}