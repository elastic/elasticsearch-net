using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IIndexedScriptTransform : IScriptTransform
	{
		[JsonProperty("id")]
		string Id { get; set; }
	}

	public class IndexedScriptTransform : ScriptTransformBase, IIndexedScriptTransform
	{
		public IndexedScriptTransform(string id)
		{
			this.Id = id;
		}

		public string Id { get; set; }
	}

	public class IndexedScriptTransformDescriptor
		: ScriptTransformDescriptorBase<IndexedScriptTransformDescriptor, IIndexedScriptTransform>, IIndexedScriptTransform
	{
		public IndexedScriptTransformDescriptor(string id)
		{
			Self.Id = id;
		}

		public IndexedScriptTransformDescriptor() {}

		string IIndexedScriptTransform.Id { get; set; }
	}
}
