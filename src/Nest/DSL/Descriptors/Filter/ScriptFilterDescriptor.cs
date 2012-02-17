using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.DSL;

namespace Nest
{
	public class ScriptFilterDescriptor
	{
		[JsonProperty(PropertyName = "script")]
		internal string _Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		internal Dictionary<string, object> _Params { get; set; }

		[JsonProperty(PropertyName = "_cache")]
		internal bool? _Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		internal string _Name { get; set; }

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
