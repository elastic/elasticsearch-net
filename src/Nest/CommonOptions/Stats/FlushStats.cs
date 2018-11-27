using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class FlushStats
	{
		/// <summary>
		/// The number of flushes that were periodically triggered when translog exceeded the flush threshold.
		/// </summary>
		[DataMember(Name ="periodic")]
		public long Periodic { get; set; }

		[DataMember(Name ="total")]
		public long Total { get; set; }

		/// <summary>
		/// The total time merges have been executed.
		/// </summary>
		[DataMember(Name ="total_time")]
		public string TotalTime { get; set; }

		/// <summary>
		/// The total time merges have been executed (in milliseconds).
		/// </summary>
		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; set; }
	}
}
