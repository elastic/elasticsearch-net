using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class Segment
	{
		[JsonProperty("generation")]
		public int Generation { get; internal set; }

		[JsonProperty("num_docs")]
		public long TotalDocuments { get; internal set; }

		[JsonProperty("deleted_docs")]
		public long DeletedDocuments { get; internal set; }

		[JsonProperty("size")]
		public string Size { get; internal set; }

		[JsonProperty("size_in_bytes")]
		public double SizeInBytes { get; internal set; }

		[JsonProperty("committed")]
		public bool Committed { get; internal set; }

		[JsonProperty("Search")]
		public bool Search { get; internal set; }
	}
}
