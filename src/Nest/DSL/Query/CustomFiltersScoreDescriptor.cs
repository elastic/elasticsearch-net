using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    class CustomFiltersScoreDescriptor<T> : IQuery where T : class
    {
        [JsonProperty(PropertyName = "query")]
        internal BaseQuery _Query { get; set; }

        [JsonProperty(PropertyName = "filters")]
        internal List<BaseFilter> _Filters { get; set; }

        [JsonProperty(PropertyName = "score_mode")]
        internal string _ScoreMode { get; set; }

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

        public CustomFiltersScoreDescriptor<T> ScoreMode(string scoreMode)
        {
            scoreMode.ThrowIfNull("script");
            this._ScoreMode = scoreMode;
            return this;
        }

        public CustomFiltersScoreDescriptor<T> Filters(params Func<FilterDescriptor<T>, BaseFilter>[] filterSelectors)
        {
            filterSelectors.ThrowIfNull("filterSelectors");

            FilterDescriptor<T> filter = null;
            BaseFilter f = null;

            foreach (var filterSelector in filterSelectors)
            {
                filter = new FilterDescriptor<T>();
                f = filterSelector(filter);
                this._Filters.Add(f);
            }

            return this;
        }
    }
}
