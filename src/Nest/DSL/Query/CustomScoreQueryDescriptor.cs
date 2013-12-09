using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class CustomScoreQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "lang")]
		internal string _Lang { get; set; }

		[JsonProperty(PropertyName = "script")]
		internal string _Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> _Params { get; set; }

		[JsonProperty(PropertyName = "query")]
		internal BaseQuery _Query { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return this._Query == null || this._Query.IsConditionless;
			}
		}

		public CustomScoreQueryDescriptor<T> Lang(string lang)
		{
			this._Lang = lang;
			return this;
		}

		public CustomScoreQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			this._Query = q;
			return this;
		}
		/// <summary>
		/// Scripts are cached for faster execution. If the script has parameters that it needs to take into account, it is preferable to use the same script, and provide parameters to it:
		/// </summary>
		/// <param name="script"></param>
		/// <returns></returns>
		public CustomScoreQueryDescriptor<T> Script(string script)
		{
			this._Script = script;
			return this;
		}
		public CustomScoreQueryDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}
	}
}
