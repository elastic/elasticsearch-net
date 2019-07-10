using System.Runtime.Serialization;

namespace Nest
{
	public class ForgetFollowerIndexResponse : ResponseBase
	{
		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
