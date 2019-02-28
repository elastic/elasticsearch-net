using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IShrinkIndexResponse : IAcknowledgedResponse
	{
		[DataMember(Name ="shards_acknowledged")]
		bool ShardsAcknowledged { get; }
	}

	public class ShrinkIndexResponse : AcknowledgedResponseBase, IShrinkIndexResponse
	{
		public bool ShardsAcknowledged { get; internal set; }
	}
}
