using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IReindexRethrottleResponse : IResponse
	{
		[JsonProperty("nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		Dictionary<string, ReindexNode> Nodes { get; set; }
	}

	public class ReindexRethrottleResponse : ResponseBase, IReindexRethrottleResponse
	{
		public Dictionary<string, ReindexNode> Nodes { get; set; }
	}
}
