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
	public interface ICustomFiltersScoreQuery : IQuery
	{
		[JsonProperty(PropertyName = "query")]
		IQueryDescriptor Query { get; set; }

		[JsonProperty(PropertyName = "filters")]
		List<IFilterScoreQuery> Filters { get; set; }

		[JsonProperty(PropertyName = "score_mode")]
		[JsonConverter(typeof(StringEnumConverter))]
		ScoreMode ScoreMode { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string Lang { get; set; }

		[JsonProperty(PropertyName = "max_boost")]
		string MaxBoost { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class CustomFiltersScoreQueryDescriptor<T> : ICustomFiltersScoreQuery where T : class
	{
		IQueryDescriptor ICustomFiltersScoreQuery.Query { get; set; }

		List<IFilterScoreQuery> ICustomFiltersScoreQuery.Filters { get; set; }

		ScoreMode ICustomFiltersScoreQuery.ScoreMode { get; set; }

		Dictionary<string, object> ICustomFiltersScoreQuery.Params { get; set; }

		string ICustomFiltersScoreQuery.Lang { get; set; }

		string ICustomFiltersScoreQuery.MaxBoost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((ICustomFiltersScoreQuery)this).Query == null || ((ICustomFiltersScoreQuery)this).Query.IsConditionless;
			}
		}

		public CustomFiltersScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			((ICustomFiltersScoreQuery)this).Query = q;
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> ScoreMode(ScoreMode scoreMode)
		{
			((ICustomFiltersScoreQuery)this).ScoreMode = scoreMode;
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> Filters(params Func<FilterScoreQueryDescriptor<T>, FilterScoreQueryDescriptor<T>>[] filterSelectors)
		{
			filterSelectors.ThrowIfNull("filterSelectors");

			((ICustomFiltersScoreQuery)this).Filters = new List<IFilterScoreQuery>();

			foreach (var filterSelector in filterSelectors)
			{
				var filter = new FilterScoreQueryDescriptor<T>();
				filterSelector.ThrowIfNull("filterSelector");
				((ICustomFiltersScoreQuery)this).Filters.Add(filterSelector(filter));
			}

			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			((ICustomFiltersScoreQuery)this).Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> Language(string language)
		{
			((ICustomFiltersScoreQuery)this).Lang = language;
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> MaxBoost(string maxBoost)
		{
			((ICustomFiltersScoreQuery)this).MaxBoost = maxBoost;
			return this;
		}
	}
}
