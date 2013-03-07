using System;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    /// <summary>
    /// The BoostingQuery class can be used to effectively demote results that match a given query.
    /// Unlike the "NOT" clause, this still selects documents that contain undesirable terms,
    /// but reduces their overall score:
    ///
    /// Query balancedQuery = new BoostingQuery(positiveQuery, negativeQuery, 0.01f);
    /// In this scenario the positiveQuery contains the mandatory, desirable criteria which is used to
    /// select all matching documents, and the negativeQuery contains the undesirable elements which
    /// are simply used to lessen the scores. Documents that match the negativeQuery have their score
    /// multiplied by the supplied "boost" parameter, so this should be less than 1 to achieve a
    /// demoting effect
    /// </summary>
    public class BoostingQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.BoostingQueryBuilder;
        private IQueryBuilder _positiveQuery;
        private IQueryBuilder _negativeQuery;
        private float? _negativeBoost;
        private float? _boost;

        public BoostingQueryBuilder Positive(IQueryBuilder positiveQuery)
        {
            _positiveQuery = positiveQuery;
            return this;
        }

        public BoostingQueryBuilder Negative(IQueryBuilder negativeQuery)
        {
            _negativeQuery = negativeQuery;
            return this;
        }

        public BoostingQueryBuilder NegativeBoost(float negativeBoost)
        {
            _negativeBoost = negativeBoost;
            return this;
        }

        public BoostingQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            if (_positiveQuery == null)
            {
                throw new QueryBuilderException("Boosting query requires positive query to be set");
            }
            if (_negativeQuery == null)
            {
                throw new QueryBuilderException("Boosting query requires negative query to be set");
            }
            if (_negativeBoost == null)
            {
                throw new QueryBuilderException("Boosting query requires negativeBoost to be set");
            }

            var content = new JObject(new JProperty(NAME, new JObject()));
            content[NAME]["positive"] = _positiveQuery.ToJsonObject() as JObject;
            content[NAME]["negative"] = _negativeQuery.ToJsonObject() as JObject;
            content[NAME]["negative_boost"] = _negativeBoost;

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
