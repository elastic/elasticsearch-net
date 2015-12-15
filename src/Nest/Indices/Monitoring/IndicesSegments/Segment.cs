using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class Segment
	{
		[JsonProperty(PropertyName = "generation")]
		public int Generation { get; internal set; }

		[JsonProperty(PropertyName = "num_docs")]
		public long TotalDocuments { get; internal set; }

		[JsonProperty(PropertyName = "deleted_docs")]
		public long DeletedDocuments { get; internal set; }

		[JsonProperty(PropertyName = "size")]
		public string Size { get; internal set; }

		[JsonProperty(PropertyName = "size_in_bytes")]
		public double SizeInBytes { get; internal set; }

		[JsonProperty(PropertyName = "committed")]
		public bool Committed { get; internal set; }

		[JsonProperty(PropertyName = "Search")]
		public bool Search { get; internal set; }
	}
}