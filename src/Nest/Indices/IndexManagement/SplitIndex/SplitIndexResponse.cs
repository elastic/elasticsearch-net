using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class SplitIndexResponse : AcknowledgedResponseBase
	{
		[DataMember(Name ="shards_acknowledged")]
		public bool ShardsAcknowledged { get; internal set; }
	}
}
