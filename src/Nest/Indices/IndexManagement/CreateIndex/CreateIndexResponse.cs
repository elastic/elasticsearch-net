using System.Runtime.Serialization;

namespace Nest
{
	public class CreateIndexResponse : AcknowledgedResponseBase
	{
		[DataMember(Name ="shards_acknowledged")]
		public bool ShardsAcknowledged { get; set; }
	}
}
