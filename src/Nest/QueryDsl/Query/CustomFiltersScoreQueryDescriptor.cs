using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Nest.DSL.Query;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<CustomFiltersScoreQueryDescriptor<object>>))]
	public interface ICustomFiltersScoreQuery : IQuery
	{
		[JsonProperty(PropertyName = "query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

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

	public class CustomFiltersScoreQueryDescriptor<T> : ICustomFiltersScoreQuery where T : class
	{
		ICustomFiltersScoreQuery Self { get { return this; }}

		IQueryContainer ICustomFiltersScoreQuery.Query { get; set; }

		List<IFilterScoreQuery> ICustomFiltersScoreQuery.Filters { get; set; }

		ScoreMode ICustomFiltersScoreQuery.ScoreMode { get; set; }

		Dictionary<string, object> ICustomFiltersScoreQuery.Params { get; set; }

		string ICustomFiltersScoreQuery.Lang { get; set; }

		string ICustomFiltersScoreQuery.MaxBoost { get; set; }

		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Query == null || Self.Query.IsConditionless;
			}
		}

		public CustomFiltersScoreQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			Self.Query = q;
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> ScoreMode(ScoreMode scoreMode)
		{
			Self.ScoreMode = scoreMode;
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> Filters(params Func<FilterScoreQueryDescriptor<T>, FilterScoreQueryDescriptor<T>>[] filterSelectors)
		{
			filterSelectors.ThrowIfNull("filterSelectors");

			Self.Filters = new List<IFilterScoreQuery>();

			foreach (var filterSelector in filterSelectors)
			{
				var filter = new FilterScoreQueryDescriptor<T>();
				filterSelector.ThrowIfNull("filterSelector");
				Self.Filters.Add(filterSelector(filter));
			}

			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> Language(string language)
		{
			Self.Lang = language;
			return this;
		}

		public CustomFiltersScoreQueryDescriptor<T> MaxBoost(string maxBoost)
		{
			Self.MaxBoost = maxBoost;
			return this;
		}
	}
}
