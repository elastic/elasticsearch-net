using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class SpanOrQueryBuilder : ISpanQueryBuilder
    {
        private const string NAME = NameRegistry.SpanOrQueryBuilder;
        private readonly List<ISpanQueryBuilder> _clauses = new List<ISpanQueryBuilder>();
        private float? _boost;

        public SpanOrQueryBuilder Clause(ISpanQueryBuilder clause)
        {
            _clauses.Add(clause);
            return this;
        }

        public SpanOrQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region ISpanQueryBuilder Members

        public object ToJsonObject()
        {
            if(_clauses.Count == 0)
            {
                throw new QueryBuilderException("Must have at least one clause when building a spanOr query");    
            }

            var content = new JObject(new JProperty(NAME, new JObject()));
            
            content[NAME]["clauses"] = new JArray(_clauses.Select(t => t.ToJsonObject()).ToArray());

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