using System;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    public class FieldMaskingSpanQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.FieldMaskingSpanQueryBuilder;
        private readonly ISpanQueryBuilder _queryBuilder;
        private readonly string _field;
        private float? _boost;

        public FieldMaskingSpanQueryBuilder(ISpanQueryBuilder queryBuilder, string field)
        {
            _queryBuilder = queryBuilder;
            _field = field;
        }

        public FieldMaskingSpanQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME), new JObject());
            content[NAME]["query"] = _queryBuilder.ToJsonObject() as JObject;
            content[NAME]["field"] = _field;

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