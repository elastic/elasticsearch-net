using System.Runtime.Serialization;

namespace Nest {
	public class RecoveryVerifyIndex
	{
		[DataMember(Name ="check_index_time_in_millis")]
		public long CheckIndexTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }
	}
}