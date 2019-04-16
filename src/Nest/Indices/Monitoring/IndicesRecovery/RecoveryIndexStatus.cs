using System.Runtime.Serialization;

namespace Nest
{
	public class RecoveryIndexStatus
	{
		[DataMember(Name ="bytes")]
		public RecoveryBytes Bytes { get; internal set; }

		[DataMember(Name ="files")]
		public RecoveryFiles Files { get; internal set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }
	}
}
