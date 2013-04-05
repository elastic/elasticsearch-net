using System;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class NestedQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.NestedQueryBuilder;
        private readonly IQueryBuilder _queryBuilder;
        private readonly IFilterBuilder _filterBuilder;
        private readonly string _path;
        private string _scoreMode;
        private float? _boost = 1.0f;
        private string _scope;

        public NestedQueryBuilder(string path, IQueryBuilder queryBuilder)
        {
            _path = path;
            _queryBuilder = queryBuilder;
            _filterBuilder = null;
        }

        public NestedQueryBuilder(string path, IFilterBuilder filterBuilder)
        {
            _path = path;
            _queryBuilder = null;
            _filterBuilder = filterBuilder;
        }

        /// <summary>
        /// The score mode.
        /// </summary>
        /// <param name="scoreMode"></param>
        /// <returns></returns>
        public NestedQueryBuilder ScoreMode(string scoreMode)
        {
            _scoreMode = scoreMode;
            return this;
        }

        public NestedQueryBuilder Scope(String scope)
        {
            _scope = scope;
            return this;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public NestedQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            if (_queryBuilder != null)
            {
                content[NAME]["query"] = _queryBuilder.ToJsonObject() as JObject;
            }
            else
            {
                content[NAME]["filter"] = _filterBuilder.ToJsonObject() as JObject;
            }

            content[NAME]["path"] = _path;

            if (string.IsNullOrEmpty(_scoreMode))
            {
                content[NAME]["score_mode"] = _scoreMode;
            }

            if (_scope != null)
            {
                content[NAME]["_scope"] = _scope;
            }

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
