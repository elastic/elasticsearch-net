using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardSegments
	{
		[JsonProperty("count")]
		public long Count { get; set; }
		[JsonProperty("memory_in_bytes")]
		public long MemoryInBytes { get; set; }
		[JsonProperty("terms_memory_in_bytes")]
		public long TermsMemoryInBytes { get; set; }
		[JsonProperty("stored_fields_memory_in_bytes")]
		public long StoredFieldsMemoryInBytes { get; set; }
		[JsonProperty("term_vectors_memory_in_bytes")]
		public long TermVectorsMemoryInBytes { get; set; }
		[JsonProperty("norms_memory_in_bytes")]
		public long NormsMemoryInBytes { get; set; }
		[JsonProperty("points_memory_in_bytes")]
		public long PointsMemoryInBytes { get; set; }
		[JsonProperty("doc_values_memory_in_bytes")]
		public long DocumentValuesMemoryInBytes { get; set; }
		[JsonProperty("index_writer_memory_in_bytes")]
		public long IndexWriterMemoryInBytes { get; set; }
		[JsonProperty("version_map_memory_in_bytes")]
		public long VersionMapMemoryInBytes { get; set; }
		[JsonProperty("fixed_bit_set_memory_in_bytes")]
		public long FixedBitMemoryInBytes { get; set; }
		[JsonProperty("max_unsafe_auto_id_timestamp")]
		public long MaxUnsafeAutoIdTimeStamp { get; set; }
		// TODO! [string, long] [JsonProperty("file_sizes")]
	}
}