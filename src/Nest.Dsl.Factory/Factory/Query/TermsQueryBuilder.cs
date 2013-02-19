using System;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class TermsQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.TermsQueryBuilder;
        private readonly string _name;
        private readonly object[] _values;
        private int? _minimumMatch;
        private bool _disableCoord;
        private float? _boost;

        public TermsQueryBuilder(string name, params string[] values) : this(name, (object)values) { }
        public TermsQueryBuilder(string name, params int[] values) : this(name, (object)values) { }
        public TermsQueryBuilder(string name, params long[] values) : this(name, (object)values) { }
        public TermsQueryBuilder(string name, params float[] values) : this(name, (object)values) { }
        public TermsQueryBuilder(string name, params double[] values) : this(name, (object)values) { }

        /// <summary>
        /// A query for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        public TermsQueryBuilder(string name, params object[] values)
        {
            _name = name;
            _values = values;
        }

        /// <summary>
        /// Sets the minimum number of matches across the provided terms. Defaults to <tt>1</tt>.
        /// </summary>
        /// <param name="minimumMatch"></param>
        /// <returns></returns>
        public TermsQueryBuilder MinimumMatch(int minimumMatch)
        {
            _minimumMatch = minimumMatch;
            return this;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public TermsQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        /// <summary>
        /// Disables <tt>Similarity#coord(int,int)</tt> in scoring. Defualts to <tt>false</tt>.
        /// </summary>
        /// <param name="disableCoord"></param>
        /// <returns></returns>
        public TermsQueryBuilder DisableCoord(bool disableCoord)
        {
            _disableCoord = disableCoord;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME][_name] = new JArray(_values);

            if (_minimumMatch != -1)
            {
                content[NAME]["minimum_match"] = _minimumMatch;
            }
            if (_disableCoord)
            {
                content[NAME]["disable_coord"] = _disableCoord;
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