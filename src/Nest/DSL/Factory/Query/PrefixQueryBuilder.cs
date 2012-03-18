using System;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    /// <summary>
    /// A Query that matches documents containing terms with a specified prefix.
    /// </summary>
    public class PrefixQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.PrefixQueryBuilder;
        private readonly string _name;
        private readonly string _prefix;
        private float? _boost;
        private string _rewrite;

        /// <summary>
        /// A Query that matches documents containing terms with a specified prefix.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <param name="prefix">The prefix query</param>
        public PrefixQueryBuilder(string name, string prefix)
        {
            _name = name;
            _prefix = prefix;
        }

        /// <summary>
        /// Sets the boost for this query.  Documents matching this query will (in addition to the normal
        /// weightings) have their score multiplied by the boost provided.
        /// </summary>
        /// <param name="boost"></param>
        /// <returns></returns>
        public PrefixQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        public PrefixQueryBuilder Rewrite(string rewrite)
        {
            _rewrite = rewrite;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));
            
            if(_boost == null && _rewrite == null)
            {
                content[NAME][_name] = _prefix;
            }
            else
            {
                content[NAME][_name] = new JObject();
                content[NAME][_name]["prefix"] = _prefix;
                
                if(_boost != null)
                {
                    content[NAME][_name]["boost"] = _boost;
                }

                if(string.IsNullOrEmpty(_rewrite))
                {
                    content[NAME][_name]["rewrite"] = _rewrite;
                }
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
