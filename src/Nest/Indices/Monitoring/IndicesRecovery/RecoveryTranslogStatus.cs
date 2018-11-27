using System.Runtime.Serialization;

namespace Nest
{
	public class RecoveryTranslogStatus
	{
		[DataMember(Name ="percent")]
		public string Percent { get; internal set; }

		[DataMember(Name ="recovered")]
		public long Recovered { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }

		[DataMember(Name ="total_on_start")]
		public long TotalOnStart { get; internal set; }

		[DataMember(Name ="total_time")]
		public string TotalTime { get; internal set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }
	}
}
