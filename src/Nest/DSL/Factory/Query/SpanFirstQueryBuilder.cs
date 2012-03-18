using System;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    public class SpanFirstQueryBuilder : ISpanQueryBuilder
    {
        private const string NAME = NameRegistry.SpanFirstQueryBuilder;
        private readonly ISpanQueryBuilder _matchBuilder;
        private readonly int _end;
        private float? _boost;

        public SpanFirstQueryBuilder(ISpanQueryBuilder matchBuilder, int end)
        {
            _matchBuilder = matchBuilder;
            _end = end;
        }

        public SpanFirstQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region ISpanQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));
            content[NAME]["match"] = _matchBuilder.ToJsonObject() as JObject;
            content[NAME]["end"] = _end;

            if (_boost != null)
            {
                content[NAME]["boost"] = _boost;
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