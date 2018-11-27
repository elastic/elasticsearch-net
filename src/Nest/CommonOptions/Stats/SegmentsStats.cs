using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class SegmentsStats
	{
		[DataMember(Name ="count")]
		public long Count { get; set; }

		[DataMember(Name ="doc_values_memory")]
		public string DocValuesMemory { get; set; }

		[DataMember(Name ="doc_values_memory_in_bytes")]
		public long DocValuesMemoryInBytes { get; set; }

		[DataMember(Name ="fixed_bit_set_memory")]
		public string FixedBitSetMemory { get; set; }

		[DataMember(Name ="fixed_bit_set_memory_in_bytes")]
		public long FixedBitSetMemoryInBytes { get; set; }

		[DataMember(Name ="index_writer_max_memory")]
		public string IndexWriterMaxMemory { get; set; }

		[DataMember(Name ="index_writer_max_memory_in_bytes")]
		public long IndexWriterMaxMemoryInBytes { get; set; }

		[DataMember(Name ="index_writer_memory")]
		public string IndexWriterMemory { get; set; }

		[DataMember(Name ="index_writer_memory_in_bytes")]
		public long IndexWriterMemoryInBytes { get; set; }

		[DataMember(Name ="memory")]
		public string Memory { get; set; }

		[DataMember(Name ="memory_in_bytes")]
		public long MemoryInBytes { get; set; }

		[DataMember(Name ="norms_memory")]
		public string NormsMemory { get; set; }

		[DataMember(Name ="norms_memory_in_bytes")]
		public long NormsMemoryInBytes { get; set; }

		[DataMember(Name ="points_memory")]
		public string PointsMemory { get; set; }

		[DataMember(Name ="points_memory_in_bytes")]
		public long PointsMemoryInBytes { get; set; }

		[DataMember(Name ="stored_fields_memory")]
		public string StoredFieldsMemory { get; set; }

		[DataMember(Name ="stored_fields_memory_in_bytes")]
		public long StoredFieldsMemoryInBytes { get; set; }

		[DataMember(Name ="terms_memory")]
		public string TermsMemory { get; set; }

		[DataMember(Name ="terms_memory_in_bytes")]
		public long TermsMemoryInBytes { get; set; }

		[DataMember(Name ="term_vectors_memory")]
		public string TermVectorsMemory { get; set; }

		[DataMember(Name ="term_vectors_memory_in_bytes")]
		public long TermVectorsMemoryInBytes { get; set; }

		[DataMember(Name ="version_map_memory")]
		public string VersionMapMemory { get; set; }

		[DataMember(Name ="version_map_memory_in_bytes")]
		public long VersionMapMemoryInBytes { get; set; }
	}
}
