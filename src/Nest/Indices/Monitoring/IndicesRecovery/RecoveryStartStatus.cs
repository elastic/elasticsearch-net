using System.Runtime.Serialization;

namespace Nest
{
	public class RecoveryStartStatus
	{
		[DataMember(Name ="check_index_time")]
		public long CheckIndexTime { get; internal set; }

		[DataMember(Name ="total_time_in_millis")]
		public string TotalTimeInMilliseconds { get; internal set; }
	}
}
