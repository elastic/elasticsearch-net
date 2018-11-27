using System.Runtime.Serialization;

namespace Nest
{
	public interface ICountResponse : IResponse
	{
		long Count { get; }
		ShardStatistics Shards { get; }
	}

	[DataContract]
	public class CountResponse : ResponseBase, ICountResponse
	{
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="_shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
