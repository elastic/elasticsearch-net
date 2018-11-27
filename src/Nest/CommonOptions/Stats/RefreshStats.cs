using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class RefreshStats
	{
		[DataMember(Name ="total")]
		public long Total { get; set; }

		[DataMember(Name ="total_time")]
		public string TotalTime { get; set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; set; }
	}
}
