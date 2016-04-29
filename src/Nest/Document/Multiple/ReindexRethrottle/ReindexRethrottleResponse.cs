using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IReindexRethrottleResponse : IResponse
	{
		[JsonProperty("nodes")]
		IEnumerable<ReindexNode> Nodes { get; set; }
	}

	public class ReindexRethrottleResponse : ResponseBase, IReindexRethrottleResponse
	{
		public IEnumerable<ReindexNode> Nodes { get; set; }
	}
}
