using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class ScriptFilterDescriptor : FilterBase
	{
		[JsonProperty(PropertyName = "script")]
		internal string _Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		internal Dictionary<string, object> _Params { get; set; }

		public ScriptFilterDescriptor Script(string script)
		{
			script.ThrowIfNull("script");
			this._Script = script;
			return this;
		}

		public ScriptFilterDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}
	}
}
