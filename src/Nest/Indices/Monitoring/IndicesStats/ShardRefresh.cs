using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardRefresh
	{
		[DataMember(Name ="listeners")]
		public long Listeners { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }
	}
}
