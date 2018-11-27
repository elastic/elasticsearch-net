using System.Runtime.Serialization;

namespace Nest
{
	public interface IShardsOperationResponse : IResponse
	{
		ShardStatistics Shards { get; }
	}

	public abstract class ShardsOperationResponseBase : ResponseBase, IShardsOperationResponse
	{
		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
