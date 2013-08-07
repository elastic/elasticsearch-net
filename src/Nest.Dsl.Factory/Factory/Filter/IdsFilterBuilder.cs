using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class IdsFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.IdsFilterBuilder;
        private readonly List<string> _types;
        private readonly List<string> _values = new List<string>();
        private string _filterName;

        /// <summary>
        /// Create an ids filter based on the type.
        /// </summary>
        /// <param name="types"></param>
        public IdsFilterBuilder(params string[] types)
        {
            _types = types == null ? null : new List<string>(types);
        }

        /// <summary>
        /// Adds ids to the filter.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IdsFilterBuilder AddIds(params string[] ids)
        {
            _values.AddRange(ids);
            return this;
        }

        /// <summary>
        /// Adds ids to the filter.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IdsFilterBuilder Ids(params string[] ids)
        {
            return AddIds(ids);
        }

        /// <summary>
        /// Sets the filter name for the filter that can be used when searching for matched_filters per hit.
        /// </summary>
        /// <param name="filterName"></param>
        /// <returns></returns>
        public IdsFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME]["values"] = new JArray(_values);

            if (_types != null && _types.Count > 0)
            {
                if (_types.Count == 1)
                {
                     content[NAME]["type"] = _types[0];
                }
                else
                {
                     content[NAME]["types"] = new JArray(_types);
                }
            }

            if (_filterName != null)
            {
                 content[NAME]["_name"] = _filterName;
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