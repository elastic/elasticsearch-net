using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    /// <summary>
    /// A query that will execute the wrapped query only for the specified indices, and "match_all" when
    /// it does not match those indices (by default).
    /// </summary>
    public class IndicesQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.IndicesQueryBuilder;
        private IQueryBuilder _queryBuilder;
        private string[] _indices;
        private string _sNoMatchQuery;
        private IQueryBuilder _noMatchQuery;

        public IndicesQueryBuilder(IQueryBuilder queryBuilder, params string[] indices)
        {
            _queryBuilder = queryBuilder;
            _indices = indices;
        }

        /// <summary>
        /// Sets the no match query, can either be <tt>all</tt> or <tt>none</tt>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IndicesQueryBuilder NoMatchQuery(string type)
        {
            _sNoMatchQuery = type;
            return this;
        }

        /// <summary>
        /// Sets the query to use when it executes on an index that does not match the indices provided.
        /// </summary>
        /// <param name="noMatchQuery"></param>
        /// <returns></returns>
        public IndicesQueryBuilder NoMatchQuery(IQueryBuilder noMatchQuery)
        {
            _noMatchQuery = noMatchQuery;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME), new JObject());
            content[NAME]["indicies"] = new JArray(_indices);

            if(_noMatchQuery != null)
            {
                content[NAME]["no_match_query"] = _noMatchQuery.ToJsonObject() as JObject;
            }
            else if(_sNoMatchQuery != null)
            {
                content[NAME]["no_match_query"] = _sNoMatchQuery;
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