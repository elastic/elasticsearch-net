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

		[DataMember(Name ="external_total")]
		public long ExternalTotal { get; set; }

		[DataMember(Name ="external_total_time_in_millis")]
		public long ExternalTotalTimeInMilliseconds { get; set; }
	}
}
