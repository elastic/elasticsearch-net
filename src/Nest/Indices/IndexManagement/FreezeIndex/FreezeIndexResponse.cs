using System.Runtime.Serialization;

namespace Nest
{
	public class FreezeIndexResponse : AcknowledgedResponseBase
	{
		[DataMember(Name = "shards_acknowledged")]
		public bool ShardsAcknowledged { get; internal set; }
	}
}
