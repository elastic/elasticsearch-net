using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Nest
{
	public class ScriptedHeuristic
	{
		[JsonProperty("script")]
		public string Script { get; set; }
		[JsonProperty("lang")]
		public string Lang { get; set; }
		[JsonProperty("params")]
		public IDictionary<string, object> Params { get; set; }
	}

	public class ScriptedHeuristicDescriptor
	{
		internal ScriptedHeuristic ScriptedHeuristic = new ScriptedHeuristic();
		public ScriptedHeuristicDescriptor Script(string script)
		{
			this.ScriptedHeuristic.Script = script;
			return this;
		}

		public ScriptedHeuristicDescriptor Lang(string lang)
		{
			this.ScriptedHeuristic.Lang = lang;
			return this;
		}

		public ScriptedHeuristicDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector)
		{
			this.ScriptedHeuristic.Params = paramsSelector(new FluentDictionary<string, object>());
			return this;
		}
	}
}