using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Nest.DSL.Query;
using Newtonsoft.Json.Converters;

namespace Nest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CustomFiltersScoreDescriptor<T> : IQuery where T : class
    {
        [JsonProperty(PropertyName = "query")]
        internal BaseQuery _Query { get; set; }

        [JsonProperty(PropertyName = "filters")]
        internal List<FilterScoreDescriptor<T>> _Filters { get; set; }

        [JsonProperty(PropertyName = "score_mode")]
        [JsonConverter(typeof(StringEnumConverter))]
        internal ScoreMode _ScoreMode { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> _Params { get; set; }

        [JsonProperty(PropertyName = "lang")]
        internal string _Lang { get; set; }

        [JsonProperty(PropertyName = "max_boost")]
        internal string _MaxBoost { get; set; }

        internal bool IsConditionless
        {
            get
            {
                return this._Query == null || this._Query.IsConditionless;
            }
        }

        public CustomFiltersScoreDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
        {
            querySelector.ThrowIfNull("querySelector");
            var query = new QueryDescriptor<T>();
            var q = querySelector(query);

            this._Query = q;
            return this;
        }

        public CustomFiltersScoreDescriptor<T> ScoreMode(ScoreMode scoreMode)
        {
            this._ScoreMode = scoreMode;
            return this;
        }

        public CustomFiltersScoreDescriptor<T> Filters(params Func<FilterScoreDescriptor<T>, FilterScoreDescriptor<T>>[] filterSelectors)
        {
            filterSelectors.ThrowIfNull("filterSelectors");

            this._Filters = new List<FilterScoreDescriptor<T>>();

            foreach (var filterSelector in filterSelectors)
            {
                var filter = new FilterScoreDescriptor<T>();
                filterSelector.ThrowIfNull("filterSelector");
                this._Filters.Add(filterSelector(filter));
            }

            return this;
        }

        public CustomFiltersScoreDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
        {
            paramDictionary.ThrowIfNull("paramDictionary");
            this._Params = paramDictionary(new FluentDictionary<string, object>());
            return this;
        }

        public CustomFiltersScoreDescriptor<T> Language(string language)
        {
            this._Lang = language;
            return this;
        }

        public CustomFiltersScoreDescriptor<T> MaxBoost(string maxBoost)
        {
            this._MaxBoost = maxBoost;
            return this;
        }
    }
}
