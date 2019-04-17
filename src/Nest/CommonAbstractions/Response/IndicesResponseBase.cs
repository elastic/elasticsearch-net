using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public abstract class IndicesResponseBase : AcknowledgedResponseBase
	{
		[DataMember(Name = "_shards")]
		public ShardStatistics ShardsHit { get; internal set; }
	}
}
