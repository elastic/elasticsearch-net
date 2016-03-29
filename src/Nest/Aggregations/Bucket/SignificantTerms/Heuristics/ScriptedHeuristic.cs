using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ScriptedHeuristic>))]
	public interface IScriptedHeuristic 
	{
		[JsonProperty("script")]
		IScript Script { get; set; }
	}

	public class ScriptedHeuristic : IScriptedHeuristic
	{
		public IScript Script { get; set; }
	}

	public class ScriptedHeuristicDescriptor 
		: DescriptorBase<ScriptedHeuristicDescriptor, IScriptedHeuristic>, IScriptedHeuristic
	{
		IScript IScriptedHeuristic.Script { get; set; }

		public ScriptedHeuristicDescriptor Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public ScriptedHeuristicDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));
	}
}