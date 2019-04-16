using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IIndexedScript : IScript
	{
		[DataMember(Name ="id")]
		string Id { get; set; }
	}

	public class IndexedScript : ScriptBase, IIndexedScript
	{
		public IndexedScript(string id) => Id = id;

		public string Id { get; set; }
	}

	public class IndexedScriptDescriptor
		: ScriptDescriptorBase<IndexedScriptDescriptor, IIndexedScript>, IIndexedScript
	{
		public IndexedScriptDescriptor() { }

		public IndexedScriptDescriptor(string id) => Self.Id = id;

		string IIndexedScript.Id { get; set; }

		public IndexedScriptDescriptor Id(string id) => Assign(a => a.Id = id);
	}
}
