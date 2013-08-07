using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    /// <summary>
    /// A query that generates the union of documents produced by its sub-queries, and that scores each document
    /// with the maximum score for that document as produced by any sub-query, plus a tie breaking increment for any
    /// additional matching sub-queries.
    /// </summary>
    public class DisMaxQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.DisMaxQueryBuilder;
        private List<IQueryBuilder> _queries = new List<IQueryBuilder>();
        private float? _boost;
        private float? _tieBreaker;

        /// <summary>
        /// Add a sub-query to this disjunction.
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        public DisMaxQueryBuilder Add(IQueryBuilder queryBuilder)
        {
            _queries.Add(queryBuilder);
            return this;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public DisMaxQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        /// <summary>
        /// The score of each non-maximum disjunct for a document is multiplied by this weight
        /// and added into the final score.  If non-zero, the value should be small, on the order of 0.1, which says that
        /// 10 occurrences of word in a lower-scored field that is also in a higher scored field is just as good as a unique
        /// word in the lower scored field (i.e., one that is not in any higher scored field.
        /// </summary>
        /// <param name="tieBreaker"></param>
        /// <returns></returns>
        public DisMaxQueryBuilder TieBreaker(float tieBreaker)
        {
            _tieBreaker = tieBreaker;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME), new JObject());

            if(_tieBreaker != null)
            {
                content[NAME]["tie_breaker"] = _tieBreaker;
            }

            if(_boost != null)
            {
                content[NAME]["boost"] = _boost;
            }

            content[NAME]["queries"] = new JArray(_queries.Select(t => t.ToJsonObject()).ToArray());

            return content;
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        #endregion
    }
}