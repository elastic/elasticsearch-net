using System.Runtime.Serialization;

namespace Nest
{
	public class UnfreezeIndexResponse : AcknowledgedResponseBase
	{
		[DataMember(Name = "shards_acknowledged")]
		public bool ShardsAcknowledged { get; internal set; }
	}
}
