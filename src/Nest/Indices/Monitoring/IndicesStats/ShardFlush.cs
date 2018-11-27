using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardFlush
	{
		[DataMember(Name ="total")]
		public long Total { get; internal set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }
	}
}
