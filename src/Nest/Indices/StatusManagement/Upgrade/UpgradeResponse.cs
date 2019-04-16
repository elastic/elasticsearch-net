using System.Runtime.Serialization;

namespace Nest
{
	public interface IUpgradeResponse : IResponse
	{
		[DataMember(Name ="_shards")]
		ShardStatistics Shards { get; }
	}

	public class UpgradeResponse : ResponseBase, IUpgradeResponse
	{
		public ShardStatistics Shards { get; internal set; }
	}
}
