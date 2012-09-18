using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    public class MatchQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.MatchQueryBuilder;
        private readonly string _name;
        private readonly object _value;
        private float? _boost;
        public MatchQueryBuilder(string name, string value) : this(name, (object)value) { }
        public MatchQueryBuilder(string name, int value) : this(name, (object)value) { }
        public MatchQueryBuilder(string name, long value) : this(name, (object)value) { }
        public MatchQueryBuilder(string name, float value) : this(name, (object)value) { }
        public MatchQueryBuilder(string name, double value) : this(name, (object)value) { }
        public MatchQueryBuilder(string name, bool value) : this(name, (object)value) { }

		public MatchQueryBuilder(string name, object value)
        {
            _name = name;
            _value = value;
        }

		public MatchQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            if (_boost == null)
            {
                content[NAME][_name] = new JValue(_value);
            }
            else
            {
                content[NAME][_name] = new JObject(new JProperty("query", _value), new JProperty("boost", _boost));
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