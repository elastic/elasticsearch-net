using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICustomScoreQuery
	{
		[JsonProperty(PropertyName = "lang")]
		string _Lang { get; set; }

		[JsonProperty(PropertyName = "script")]
		string _Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> _Params { get; set; }

		[JsonProperty(PropertyName = "query")]
		IQueryDescriptor _Query { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class CustomScoreQueryDescriptor<T> : IQuery, ICustomScoreQuery where T : class
	{
		string ICustomScoreQuery._Lang { get; set; }

		string ICustomScoreQuery._Script { get; set; }

		Dictionary<string, object> ICustomScoreQuery._Params { get; set; }

		IQueryDescriptor ICustomScoreQuery._Query { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((ICustomScoreQuery)this)._Query == null || ((ICustomScoreQuery)this)._Query.IsConditionless;
			}
		}

		public CustomScoreQueryDescriptor<T> Lang(string lang)
		{
			((ICustomScoreQuery)this)._Lang = lang;
			return this;
		}

		public CustomScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			((ICustomScoreQuery)this)._Query = q;
			return this;
		}

		/// <summary>
		/// Scripts are cached for faster execution. If the script has parameters that it needs to take into account, it is preferable to use the same script, and provide parameters to it:
		/// </summary>
		/// <param name="script"></param>
		/// <returns></returns>
		public CustomScoreQueryDescriptor<T> Script(string script)
		{
			((ICustomScoreQuery)this)._Script = script;
			return this;
		}

		public CustomScoreQueryDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			((ICustomScoreQuery)this)._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}
	}
}
