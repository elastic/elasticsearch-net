using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IIndexedScriptCondition : IScriptCondition
	{
		[JsonProperty("id")]
		string Id { get; set; }
	}

	public class IndexedScriptCondition : ScriptConditionBase, IIndexedScriptCondition
	{
		public IndexedScriptCondition(string id) => Id = id;

		public string Id { get; set; }
	}

	public class IndexedScriptConditionDescriptor
		: ScriptConditionDescriptorBase<IndexedScriptConditionDescriptor, IIndexedScriptCondition>, IIndexedScriptCondition
	{
		public IndexedScriptConditionDescriptor(string id) => Self.Id = id;

		string IIndexedScriptCondition.Id { get; set; }
	}
}
