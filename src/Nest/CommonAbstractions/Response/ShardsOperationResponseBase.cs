using System.Runtime.Serialization;

namespace Nest
{
	public abstract class ShardsOperationResponseBase : ResponseBase
	{
		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
