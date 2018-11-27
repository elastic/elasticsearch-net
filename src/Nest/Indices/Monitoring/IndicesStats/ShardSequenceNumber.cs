using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardSequenceNumber
	{
		[DataMember(Name ="global_checkpoint")]
		public long GlobalCheckpoint { get; internal set; }

		[DataMember(Name ="local_checkpoint")]
		public long LocalCheckpoint { get; internal set; }

		[DataMember(Name ="max_seq_no")]
		public long MaximumSequenceNumber { get; internal set; }
	}
}
