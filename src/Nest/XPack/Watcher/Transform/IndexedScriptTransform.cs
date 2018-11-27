using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IIndexedScriptTransform : IScriptTransform
	{
		[DataMember(Name ="id")]
		string Id { get; set; }
	}

	public class IndexedScriptTransform : ScriptTransformBase, IIndexedScriptTransform
	{
		public IndexedScriptTransform(string id) => Id = id;

		public string Id { get; set; }
	}

	public class IndexedScriptTransformDescriptor
		: ScriptTransformDescriptorBase<IndexedScriptTransformDescriptor, IIndexedScriptTransform>, IIndexedScriptTransform
	{
		public IndexedScriptTransformDescriptor(string id) => Self.Id = id;

		public IndexedScriptTransformDescriptor() { }

		string IIndexedScriptTransform.Id { get; set; }
	}
}
