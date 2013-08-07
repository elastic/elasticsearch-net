using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    /// <summary>
    /// Script sort builder allows to sort based on a custom script expression.
    /// </summary>
    public class ScriptSortBuilder : ISortBuilder
    {
        private const string NAME = NameRegistry.ScriptSortBuilder;
        private string _lang;
        private readonly string _script;
        private readonly string _type;
        private SortOrder _order;
        private Dictionary<string, object> _params;

        /// <summary>
        /// Constructs a script sort builder with the script and the type.
        /// </summary>
        /// <param name="script">The script to use.</param>
        /// <param name="type">The type, can either be "string" or "number".</param>
        public ScriptSortBuilder(string script, string type)
        {
            _script = script;
            _type = type;
        }

        ///<summary>
        /// A parameter that will be passed to the script.
        /// </summary>
        /// <param name="name">The name of the script parameter.</param>
        /// <param name="value">The value of the script parameter.</param>
        /// <returns></returns>
        public ScriptSortBuilder Param(string name, object value)
        {
            if (_params == null)
            {
                _params = new Dictionary<string, object>();
            }
            _params.Add(name, value);
            return this;
        }

        /// <summary>
        /// The language of the script.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public ScriptSortBuilder Lang(string lang)
        {
            _lang = lang;
            return this;
        }

        /// <summary>
        /// Sets the sort order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ISortBuilder Order(SortOrder order)
        {
            _order = order;
            return this;
        }

        /// <summary>
        /// Not really relevant.
        /// </summary>
        /// <param name="missing"></param>
        /// <returns></returns>
        public ISortBuilder Missing(object missing)
        {
            return this;
        }

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));

            content[NAME]["script"] = _script;
            content[NAME]["type"] = _type;

            if (_order == SortOrder.DESC)
            {
                content[NAME]["reverse"] = true;
            }

            if (_lang != null)
            {
                content[NAME]["lang"] = _lang;
            }

            if (_params != null)
            {
                content[NAME]["params"] = new JObject();

                foreach (var param in _params)
                {
                    content[NAME]["params"][param.Key] = new JValue(param.Value);
                }
            }

            return content;
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }
    }
}
