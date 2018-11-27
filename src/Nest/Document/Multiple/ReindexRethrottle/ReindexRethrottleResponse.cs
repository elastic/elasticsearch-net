using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IReindexRethrottleResponse : IResponse
	{
		[DataMember(Name ="nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ReindexNode>))]
		IReadOnlyDictionary<string, ReindexNode> Nodes { get; }
	}

	public class ReindexRethrottleResponse : ResponseBase, IReindexRethrottleResponse
	{
		public IReadOnlyDictionary<string, ReindexNode> Nodes { get; internal set; } = EmptyReadOnly<string, ReindexNode>.Dictionary;
	}
}
