using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface ISplitIndexResponse : IAcknowledgedResponse
	{
		[DataMember(Name ="shards_acknowledged")]
		bool ShardsAcknowledged { get; }
	}

	public class SplitIndexResponse : AcknowledgedResponseBase, ISplitIndexResponse
	{
		public bool ShardsAcknowledged { get; internal set; }
	}
}
