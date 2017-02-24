using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IReindexRethrottleResponse : IResponse
	{
		[JsonProperty("nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ReindexNode>))]
		IReadOnlyDictionary<string, ReindexNode> Nodes { get; }
	}

	public class ReindexRethrottleResponse : ResponseBase, IReindexRethrottleResponse
	{
		public IReadOnlyDictionary<string, ReindexNode> Nodes { get; internal set; } = EmptyReadOnly<string, ReindexNode>.Dictionary;
	}
}
