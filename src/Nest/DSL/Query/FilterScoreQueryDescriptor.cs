using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.DSL.Query
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<FilterScoreQueryDescriptor<object>>))]
	public interface IFilterScoreQuery : IQuery
	{
		[JsonProperty(PropertyName = "filter")]
		FilterContainer Filter { get; set; }
		
		[JsonProperty(PropertyName = "lang")]
		string Lang { get; set; }
		
		[JsonProperty(PropertyName = "script")]
		string Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }
	}

	public class FilterScoreQuery : IFilterScoreQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public FilterContainer Filter { get; set; }
		public string Lang { get; set; }
		public string Script { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public double? Boost { get; set; }
	}

	public class FilterScoreQueryDescriptor<T> : IFilterScoreQuery where T : class
	{
		private IFilterScoreQuery Self { get { return this; } }

		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless { get { return Self.Filter == null; } }

		FilterContainer IFilterScoreQuery.Filter { get; set; }

		string IFilterScoreQuery.Script { get; set; }

		string IFilterScoreQuery.Lang { get; set; }

		double? IFilterScoreQuery.Boost { get; set; }

		Dictionary<string, object> IFilterScoreQuery.Params { get; set; }

		public FilterScoreQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public FilterScoreQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}

		public FilterScoreQueryDescriptor<T> Script(string script)
		{
			Self.Script = script;
			return this;
		}
		public FilterScoreQueryDescriptor<T> Lang(string lang)
		{
			Self.Lang = lang;
			return this;
		}
        public FilterScoreQueryDescriptor<T> Lang(ScriptLang lang)
        {
            Self.Lang = lang.GetStringValue();
            return this;
        }

		public FilterScoreQueryDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}
		public FilterScoreQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			Self.Filter = filterSelector(filter);
			return this;
		}
	}
}
