using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CountResponse : ResponseBase
	{
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
