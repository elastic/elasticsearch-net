using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    /// <summary>
    /// A Query that matches documents matching boolean combinations of other queries.
    /// </summary>
    public class BoolQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.BoolQueryBuilder;
        private readonly List<IQueryBuilder> _mustClauses = new List<IQueryBuilder>();
        private readonly List<IQueryBuilder> _mustNotClauses = new List<IQueryBuilder>();
        private readonly List<IQueryBuilder> _shouldClauses = new List<IQueryBuilder>();
        private float? _boost;
        private bool _disableCoord;
        private int? _minimumNumberShouldMatch;

        /// <summary>
        /// Adds a query that <b>must</b> appear in the matching documents.
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        public BoolQueryBuilder Must(IQueryBuilder queryBuilder)
        {
            _mustClauses.Add(queryBuilder);
            return this;
        }

        /// <summary>
        /// Adds a query that <b>must not</b> appear in the matching documents.
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        public BoolQueryBuilder MustNot(IQueryBuilder queryBuilder)
        {
            _mustNotClauses.Add(queryBuilder);
            return this;
        }

        /// <summary>
        /// Adds a query that <i>should</i> appear in the matching documents. For a boolean query with no
        /// <tt>MUST</tt> clauses one or more <code>SHOULD</code> clauses must match a document
        /// for the BooleanQuery to match.
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        public BoolQueryBuilder Should(IQueryBuilder queryBuilder)
        {
            _shouldClauses.Add(queryBuilder);
            return this;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public BoolQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        /// <summary>
        /// Disables <tt>Similarity#coord(int,int)</tt> in scoring. Defaults to <tt>false</tt>.
        /// </summary>
        /// <param name="disableCoord"></param>
        /// <returns></returns>
        public BoolQueryBuilder DisableCoord(bool disableCoord)
        {
            _disableCoord = disableCoord;
            return this;
        }

        /// <summary>
        /// Specifies a minimum number of the optional (should) boolean clauses which must be satisfied.
        /// By default no optional clauses are necessary for a match
        /// (unless there are no required clauses). If this method is used,
        /// then the specified number of clauses is required.
        ///
        /// Use of this method is totally independent of specifying that
        /// any specific clauses are required (or prohibited).  This number will
        /// only be compared against the number of matching optional clauses.
        ///
        /// minimumNumberShouldMatch the number of optional clauses that must match
        /// </summary>
        /// <param name="minimumNumberShouldMatch"></param>
        /// <returns></returns>
        public BoolQueryBuilder MinimumNumberShouldMatch(int minimumNumberShouldMatch)
        {
            _minimumNumberShouldMatch = minimumNumberShouldMatch;
            return this;
        }

        /// <summary>
        /// Return <code>true</code> if the query being built has no clause yet.
        /// </summary>
        /// <returns></returns>
        public bool HasClauses()
        {
            return (_mustClauses.Count > 0) || (_mustNotClauses.Count > 0) || (_shouldClauses.Count > 0);
        }


        private object ArrayToObject(string field, IList<IQueryBuilder> array, object contentObject)
        {
            var content = (JObject) contentObject;

            if (array.Count == 0)
            {
                return content;
            }

            if (array.Count == 1)
            {
                ((JObject) content.SelectToken(NAME)).Add(new JProperty(field, array[0].ToJsonObject()));
            }
            else
            {
                ((JObject) content.SelectToken(NAME)).Add(new JProperty(field, new JArray()));

                foreach (IQueryBuilder item in array)
                {
                    ((JArray) content.SelectToken(NAME + "." + field)).Add(item.ToJsonObject());
                }
            }

            return content;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            content = ArrayToObject("must", _mustClauses, content) as JObject;
            content = ArrayToObject("must_not", _mustNotClauses, content) as JObject;
            content = ArrayToObject("should", _shouldClauses, content) as JObject;

            if (_boost != null)
            {
                content[NAME]["boost"] = _boost;
            }

            if (_disableCoord)
            {
                 content[NAME]["disable_coord"] = _disableCoord;
            }

            if (_minimumNumberShouldMatch != null)
            {
                 content[NAME]["minimum_number_should_match"] = _minimumNumberShouldMatch;
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