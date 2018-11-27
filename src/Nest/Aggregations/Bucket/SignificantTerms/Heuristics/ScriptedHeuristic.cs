using System;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ScriptedHeuristic))]
	public interface IScriptedHeuristic
	{
		[DataMember(Name ="script")]
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
