using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Nest.DSL.Query;
using Newtonsoft.Json.Converters;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICustomFiltersScoreQuery
	{
		[JsonProperty(PropertyName = "query")]
		IQueryDescriptor _Query { get; set; }

		[JsonProperty(PropertyName = "filters")]
		List<IFilterScoreQuery> _Filters { get; set; }

		[JsonProperty(PropertyName = "score_mode")]
		[JsonConverter(typeof(StringEnumConverter))]
		ScoreMode _ScoreMode { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> _Params { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string _Lang { get; set; }

		[JsonProperty(PropertyName = "max_boost")]
		string _MaxBoost { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class CustomFiltersScoreQueryDescriptor<T> : IQuery, ICustomFiltersScoreQuery where T : class
	{
		IQueryDescriptor ICustomFiltersScoreQuery._Query { get; set; }

		List<IFilterScoreQuery> ICustomFiltersScoreQuery._Filters { get; set; }

		ScoreMode ICustomFiltersScoreQuery._ScoreMode { get; set; }

		Dictionary<string, object> ICustomFiltersScoreQuery._Params { get; set; }

		string ICustomFiltersScoreQuery._Lang { get; set; }

		string ICustomFiltersScoreQuery._MaxBoost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((ICustomFiltersScoreQuery)this)._Query == null || ((ICustomFiltersScoreQuery)this)._Query.IsConditionless;
			}
		}

		public CustomFiltersScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			((ICustomFiltersScoreQuery)this)._Query = q;
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> ScoreMode(ScoreMode scoreMode)
		{
			((ICustomFiltersScoreQuery)this)._ScoreMode = scoreMode;
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> Filters(params Func<FilterScoreQueryDescriptor<T>, FilterScoreQueryDescriptor<T>>[] filterSelectors)
		{
			filterSelectors.ThrowIfNull("filterSelectors");

			((ICustomFiltersScoreQuery)this)._Filters = new List<IFilterScoreQuery>();

			foreach (var filterSelector in filterSelectors)
			{
				var filter = new FilterScoreQueryDescriptor<T>();
				filterSelector.ThrowIfNull("filterSelector");
				((ICustomFiltersScoreQuery)this)._Filters.Add(filterSelector(filter));
			}

			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			((ICustomFiltersScoreQuery)this)._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> Language(string language)
		{
			((ICustomFiltersScoreQuery)this)._Lang = language;
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> MaxBoost(string maxBoost)
		{
			((ICustomFiltersScoreQuery)this)._MaxBoost = maxBoost;
			return this;
		}
	}
}
