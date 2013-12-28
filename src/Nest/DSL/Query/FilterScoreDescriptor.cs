using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.DSL.Query
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class FilterScoreDescriptor<T> : IQuery where T : class
    {
		bool IQuery.IsConditionless { get { return this._Filter == null; }}

        [JsonProperty(PropertyName = "filter")]
        internal BaseFilter _Filter { get; set; }

        [JsonProperty(PropertyName = "script")]
        internal string _Script { get; set; }

        [JsonProperty(PropertyName = "boost")]
        internal float? _Boost { get; set; }

        public FilterScoreDescriptor<T> Boost(float boost)
        {
            this._Boost = boost;

            return this;
        }

        public FilterScoreDescriptor<T> Script(string script)
        {
            this._Script = script;

            return this;
        }

        public FilterScoreDescriptor<T> Filter(Func<FilterDescriptor<T>, BaseFilter> filterSelector)
        {
            filterSelector.ThrowIfNull("filterSelector");
            var filter = new FilterDescriptor<T>();
            this._Filter = filterSelector(filter);

            return this;
        }
    }
}
