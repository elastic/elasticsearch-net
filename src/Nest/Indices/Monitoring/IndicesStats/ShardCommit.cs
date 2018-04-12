using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardCommit
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("generation")]
		public int Generation { get; set; }

		[JsonProperty("headers")]
		public IDictionary<string, string> UserData { get; set; }

		[JsonProperty("num_docs")]
		public long NumberOfDocuments { get; set; }
	}
}