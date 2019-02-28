using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IIndicesResponse : IAcknowledgedResponse
	{
		[DataMember(Name = "_shards")]
		ShardStatistics ShardsHit { get; }
	}

	[DataContract]
	public abstract class IndicesResponseBase : AcknowledgedResponseBase, IIndicesResponse
	{
		[DataMember(Name = "_shards")]
		public ShardStatistics ShardsHit { get; internal set; }
	}
}
