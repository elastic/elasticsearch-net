using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IReindexRethrottleResponse : IResponse
	{
		[DataMember(Name ="nodes")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, ReindexNode>))]
		IReadOnlyDictionary<string, ReindexNode> Nodes { get; }
	}

	public class ReindexRethrottleResponse : ResponseBase, IReindexRethrottleResponse
	{
		public IReadOnlyDictionary<string, ReindexNode> Nodes { get; internal set; } = EmptyReadOnly<string, ReindexNode>.Dictionary;
	}
}
