using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardSegments
	{
		[JsonProperty("count")]
		public long Count { get; internal set; }
		[JsonProperty("memory_in_bytes")]
		public long MemoryInBytes { get; internal set; }
		[JsonProperty("terms_memory_in_bytes")]
		public long TermsMemoryInBytes { get; internal set; }
		[JsonProperty("stored_fields_memory_in_bytes")]
		public long StoredFieldsMemoryInBytes { get; internal set; }
		[JsonProperty("term_vectors_memory_in_bytes")]
		public long TermVectorsMemoryInBytes { get; internal set; }
		[JsonProperty("norms_memory_in_bytes")]
		public long NormsMemoryInBytes { get; internal set; }
		[JsonProperty("points_memory_in_bytes")]
		public long PointsMemoryInBytes { get; internal set; }
		[JsonProperty("doc_values_memory_in_bytes")]
		public long DocumentValuesMemoryInBytes { get; internal set; }
		[JsonProperty("index_writer_memory_in_bytes")]
		public long IndexWriterMemoryInBytes { get; internal set; }
		[JsonProperty("version_map_memory_in_bytes")]
		public long VersionMapMemoryInBytes { get; internal set; }
		[JsonProperty("fixed_bit_set_memory_in_bytes")]
		public long FixedBitMemoryInBytes { get; internal set; }
		[JsonProperty("max_unsafe_auto_id_timestamp")]
		public long MaxUnsafeAutoIdTimeStamp { get; internal set; }
		[JsonProperty("file_sizes")]
		public IReadOnlyDictionary<string, ShardFileSizeInfo> FileSizes { get; internal set; } = EmptyReadOnly<string, ShardFileSizeInfo>.Dictionary;
	}
}
