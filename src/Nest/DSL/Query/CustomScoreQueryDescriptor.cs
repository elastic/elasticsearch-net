using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<CustomScoreQueryDescriptor<object>>))]
	public interface ICustomScoreQuery : IQuery
	{
		[JsonProperty(PropertyName = "lang")]
		string Lang { get; set; }

		[JsonProperty(PropertyName = "script")]
		string Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }
	}

	public class CustomScoreQuery : PlainQuery, ICustomScoreQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.CustomScore = this;
		}

		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public string Lang { get; set; }
		public string Script { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public IQueryContainer Query { get; set; }
	}

	public class CustomScoreQueryDescriptor<T> : ICustomScoreQuery where T : class
	{
		private ICustomScoreQuery Self { get { return this;  } }

		string ICustomScoreQuery.Lang { get; set; }

		string ICustomScoreQuery.Script { get; set; }

		Dictionary<string, object> ICustomScoreQuery.Params { get; set; }

		IQueryContainer ICustomScoreQuery.Query { get; set; }

		string IQuery.Name { get; set;  }

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Query == null || Self.Query.IsConditionless;
			}
		}

		public CustomScoreQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public CustomScoreQueryDescriptor<T> Lang(string lang)
		{
			Self.Lang = lang;
			return this;
		}

        public CustomScoreQueryDescriptor<T> Lang(ScriptLang lang)
        {
            Self.Lang = lang.GetStringValue();
            return this;
        }

		public CustomScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			Self.Query = q;
			return this;
		}

		/// <summary>
		/// Scripts are cached for faster execution. If the script has parameters that it needs to take into account, it is preferable to use the same script, and provide parameters to it:
		/// </summary>
		/// <param name="script"></param>
		/// <returns></returns>
		public CustomScoreQueryDescriptor<T> Script(string script)
		{
			Self.Script = script;
			return this;
		}

		public CustomScoreQueryDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}
	}
}
