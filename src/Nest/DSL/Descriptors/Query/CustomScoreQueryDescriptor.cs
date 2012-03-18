using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class CustomScoreQueryDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "script")]
		internal string _Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		internal Dictionary<string, object> _Params { get; set; }

		[JsonProperty(PropertyName = "query")]
		internal QueryDescriptor<T> _Query { get; set; }

		public CustomScoreQueryDescriptor<T> Query(Action<QueryDescriptor<T>> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			querySelector(query);

			this._Query = query;
			return this;
		}

		public CustomScoreQueryDescriptor<T> Script(string script)
		{
			script.ThrowIfNull("script");
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
