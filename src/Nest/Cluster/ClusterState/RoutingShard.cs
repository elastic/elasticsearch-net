using System.Runtime.Serialization;

namespace Nest
{
	public class RoutingShard
	{
		[DataMember(Name ="allocation_id")]
		public AllocationId AllocationId { get; internal set; }

		[DataMember(Name ="index")]
		public string Index { get; internal set; }

		[DataMember(Name ="node")]
		public string Node { get; internal set; }

		[DataMember(Name ="primary")]
		public bool Primary { get; internal set; }

		[DataMember(Name ="relocating_node")]
		public string RelocatingNode { get; internal set; }

		[DataMember(Name ="shard")]
		public int Shard { get; internal set; }

		[DataMember(Name ="state")]
		public string State { get; internal set; }
	}
}
