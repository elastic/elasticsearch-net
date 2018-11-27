using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardStatsStore
	{
		[DataMember(Name ="size_in_bytes")]
		public long SizeInBytes { get; internal set; }
	}
}
