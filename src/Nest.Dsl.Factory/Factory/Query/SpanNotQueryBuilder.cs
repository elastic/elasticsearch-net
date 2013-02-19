using System;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class SpanNotQueryBuilder : ISpanQueryBuilder
    {
        private const string NAME = NameRegistry.SpanNotQueryBuilder;
        private ISpanQueryBuilder _include;
        private ISpanQueryBuilder _exclude;
        private float? _boost;

        public SpanNotQueryBuilder Include(ISpanQueryBuilder include)
        {
            _include = include;
            return this;
        }

        public SpanNotQueryBuilder Exclude(ISpanQueryBuilder exclude)
        {
            _exclude = exclude;
            return this;
        }

        public SpanNotQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region ISpanQueryBuilder Members

        public object ToJsonObject()
        {
            if(_include == null)
            {
                throw new QueryBuilderException("Must specify include when using spanNot query");
            }

            if(_exclude == null)
            {
                throw new QueryBuilderException("Must specify exclude when using spanNot query");
            }

            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME]["include"] = _include.ToJsonObject() as JObject;

            content[NAME]["exclude"] = _exclude.ToJsonObject() as JObject;

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