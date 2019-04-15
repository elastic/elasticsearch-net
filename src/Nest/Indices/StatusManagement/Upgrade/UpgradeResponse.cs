using System.Runtime.Serialization;

namespace Nest
{
	public class UpgradeResponse : ResponseBase
	{
		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
