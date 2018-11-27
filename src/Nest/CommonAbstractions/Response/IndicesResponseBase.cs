using System.Runtime.Serialization;

namespace Nest
{
	public interface IIndicesResponse : IResponse
	{
		bool Acknowledged { get; }
		ShardStatistics ShardsHit { get; }
	}

	[DataContract]
	public abstract class IndicesResponseBase : ResponseBase, IIndicesResponse
	{
		[DataMember(Name ="acknowledged")]
		public bool Acknowledged { get; private set; }

		[DataMember(Name ="_shards")]
		public ShardStatistics ShardsHit { get; private set; }
	}
}
