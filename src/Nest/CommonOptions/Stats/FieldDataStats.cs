using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class FielddataStats
	{
		[DataMember(Name ="evictions")]
		public long Evictions { get; set; }

		[DataMember(Name ="memory_size")]
		public string MemorySize { get; set; }

		[DataMember(Name ="memory_size_in_bytes")]
		public long MemorySizeInBytes { get; set; }
	}
}
