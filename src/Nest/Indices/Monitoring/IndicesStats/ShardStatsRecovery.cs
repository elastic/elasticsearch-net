using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardStatsRecovery
	{
		[DataMember(Name ="current_as_source")]
		public long CurrentAsSource { get; internal set; }

		[DataMember(Name ="current_as_target")]
		public long CurrentAsTarget { get; internal set; }

		[DataMember(Name ="throttle_time_in_millis")]
		public long ThrottleTimeInMilliseconds { get; internal set; }
	}
}
