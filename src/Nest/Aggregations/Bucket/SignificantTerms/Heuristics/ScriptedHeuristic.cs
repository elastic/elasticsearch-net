using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ScriptedHeuristic>))]
	public interface IScriptedHeuristic
	{
		[JsonProperty("script")]
		string Script { get; set; }
		[JsonProperty("lang")]
		string Lang { get; set; }
		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }
	}

	public class ScriptedHeuristic
	{
		public string Script { get; set; }
		public string Lang { get; set; }
		public IDictionary<string, object> Params { get; set; }
	}

	public class ScriptedHeuristicDescriptor : DescriptorBase<ScriptedHeuristicDescriptor, IScriptedHeuristic>, IScriptedHeuristic
	{
		string IScriptedHeuristic.Script { get; set; }
		string IScriptedHeuristic.Lang { get; set; }
		IDictionary<string, object> IScriptedHeuristic.Params { get; set; }

		public ScriptedHeuristicDescriptor Script(string script) => Assign(a => a.Script = script);

		public ScriptedHeuristicDescriptor Lang(string lang) => Assign(a => a.Lang = lang);

		public ScriptedHeuristicDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsSelector) =>
			Assign(a => a.Params = paramsSelector?.Invoke(new FluentDictionary<string, object>()));
	}
}