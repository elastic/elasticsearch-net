using System;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    public class SpanTermQueryBuilder : ISpanQueryBuilder
    {
        private const string NAME = NameRegistry.SpanTermQueryBuilder;
        private readonly string _name;
        private readonly object _value;
        private float? _boost;

        public SpanTermQueryBuilder(string name, string value) : this(name, (object)value) { }
        public SpanTermQueryBuilder(string name, int value) : this(name, (object)value) { }
        public SpanTermQueryBuilder(string name, long value) : this(name, (object)value) { }
        public SpanTermQueryBuilder(string name, float value) : this(name, (object)value) { }
        public SpanTermQueryBuilder(string name, double value) : this(name, (object)value) { }

        private SpanTermQueryBuilder(string name, object value)
        {
            _name = name;
            _value = value;
        }

        public SpanTermQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region ISpanQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));
            
            if(_boost == null)
            {
                content[NAME][_name] = new JValue(_value);
            }
            else
            {
                content[NAME][_name] = new JObject();
                content[NAME][_name]["value"] = new JValue(_value);
                content[NAME][_name]["boost"] = _boost;
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