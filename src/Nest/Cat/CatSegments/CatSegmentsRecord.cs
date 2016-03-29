using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatSegmentsRecord : ICatRecord
	{
		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("shard")]
		public string Shard { get; set; }
		
		[JsonProperty("prirep")]
		public string PrimaryReplica { get; set; }

		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("segment")]
		public string Segment { get; set; }

		[JsonProperty("generation")]
		public string Generation { get; set; }

		[JsonProperty("docs.count")]
		public string DocsCount { get; set; }

		[JsonProperty("docs.deleted")]
		public string DocsDeleted { get; set; }

		[JsonProperty("size")]
		public string Size { get; set; }

		[JsonProperty("size.memory")]
		public string SizeMemory { get; set; }

		[JsonProperty("committed")]
		public string Committed { get; set; }

		[JsonProperty("searchable")]
		public string Searchable { get; set; }

		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("compound")]
		public string Compound { get; set; }
	}
}
