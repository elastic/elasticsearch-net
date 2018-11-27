using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IIndexedScriptCondition : IScriptCondition
	{
		[DataMember(Name ="id")]
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
