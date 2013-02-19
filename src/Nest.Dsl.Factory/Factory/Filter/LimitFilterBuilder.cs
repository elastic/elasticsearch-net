using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class LimitFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.LimitFilterBuilder;
        private readonly int _limit;

        public LimitFilterBuilder(int limit)
        {
            _limit = limit;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            return new JObject(new JProperty(NAME, new JObject(new JProperty("value", _limit))));
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        #endregion
    }
}