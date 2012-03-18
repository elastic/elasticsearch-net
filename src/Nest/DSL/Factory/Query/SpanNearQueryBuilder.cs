using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    public class SpanNearQueryBuilder : ISpanQueryBuilder
    {
        private const string NAME = NameRegistry.SpanNearQueryBuilder;
        private readonly List<ISpanQueryBuilder> _clauses = new List<ISpanQueryBuilder>();
        private int? _slop;
        private Boolean _inOrder;
        private Boolean _collectPayloads;
        private float? _boost;

        public SpanNearQueryBuilder Clause(ISpanQueryBuilder clause)
        {
            _clauses.Add(clause);
            return this;
        }

        public SpanNearQueryBuilder Slop(int slop)
        {
            _slop = slop;
            return this;
        }

        public SpanNearQueryBuilder InOrder(bool inOrder)
        {
            _inOrder = inOrder;
            return this;
        }

        public SpanNearQueryBuilder CollectPayloads(bool collectPayloads)
        {
            _collectPayloads = collectPayloads;
            return this;
        }

        public SpanNearQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region ISpanQueryBuilder Members

        public object ToJsonObject()
        {
            if(_clauses.Count == 0)
            {
                throw new QueryBuilderException("Must have at least one clause when building a spanNear query");
            }

            if(_slop == null)
            {
                throw new QueryBuilderException("Must set the slop when building a spanNear query");
            }

            var content = new JObject(new JProperty(NAME, new JObject()));
            
            content[NAME]["clauses"] = new JArray(_clauses.Select(t => t.ToJsonObject()).ToArray());

            content[NAME]["slop"] = _slop;

            if(_inOrder)
            {
                content[NAME]["in_order"] = _inOrder;
            }

            if (_collectPayloads)
            {
                content[NAME]["collect_payloads"] = _collectPayloads;
            }

            if(_boost != null)
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