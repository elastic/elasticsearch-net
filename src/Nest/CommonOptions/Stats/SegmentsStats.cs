using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class SegmentsStats
	{
		[JsonProperty("count")]
		public long Count { get; set; }

		[JsonProperty("doc_values_memory")]
		public string DocValuesMemory { get; set; }
		[JsonProperty("doc_values_memory_in_bytes")]
		public long DocValuesMemoryInBytes { get; set; }

		[JsonProperty("fixed_bit_set_memory")]
		public string FixedBitSetMemory { get; set; }
		[JsonProperty("fixed_bit_set_memory_in_bytes")]
		public long FixedBitSetMemoryInBytes { get; set; }

		[JsonProperty("index_writer_max_memory")]
		public string IndexWriterMaxMemory { get; set; }
		[JsonProperty("index_writer_max_memory_in_bytes")]
		public long IndexWriterMaxMemoryInBytes { get; set; }

		[JsonProperty("index_writer_memory")]
		public string IndexWriterMemory { get; set; }
		[JsonProperty("index_writer_memory_in_bytes")]
		public long IndexWriterMemoryInBytes { get; set; }

		[JsonProperty("memory")]
		public string Memory { get; set; }
		[JsonProperty("memory_in_bytes")]
		public long MemoryInBytes { get; set; }

		[JsonProperty("norms_memory")]
		public string NormsMemory { get; set; }
		[JsonProperty("norms_memory_in_bytes")]
		public long NormsMemoryInBytes { get; set; }

		[JsonProperty("stored_fields_memory")]
		public string StoredFieldsMemory { get; set; }
		[JsonProperty("stored_fields_memory_in_bytes")]
		public long StoredFieldsMemoryInBytes { get; set; }

		[JsonProperty("term_vectors_memory")]
		public string TermVectorsMemory { get; set; }
		[JsonProperty("term_vectors_memory_in_bytes")]
		public long TermVectorsMemoryInBytes { get; set; }

		[JsonProperty("terms_memory")]
		public string TermsMemory { get; set; }
		[JsonProperty("terms_memory_in_bytes")]
		public long TermsMemoryInBytes { get; set; }

		[JsonProperty("version_map_memory")]
		public string VersionMapMemory { get; set; }
		[JsonProperty("version_map_memory_in_bytes")]
		public long VersionMapMemoryInBytes { get; set; }

	}
}
