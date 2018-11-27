using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardFieldData
	{
		[DataMember(Name ="evictions")]
		public long Evictions { get; internal set; }

		[DataMember(Name ="memory_size_in_bytes")]
		public long MemorySizeInBytes { get; internal set; }
	}
}
