using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class WarmerStats
	{
		[DataMember(Name ="current")]
		public long Current { get; set; }

		[DataMember(Name ="total")]
		public long Total { get; set; }

		[DataMember(Name ="total_time")]
		public string TotalTime { get; set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; set; }
	}
}
