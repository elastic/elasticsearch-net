using System;
using System.Runtime.Serialization;

namespace Nest
{
	public class ShardRecovery
	{
		[DataMember(Name ="id")]
		public long Id { get; internal set; }

		[DataMember(Name ="index")]
		public RecoveryIndexStatus Index { get; internal set; }

		[DataMember(Name ="primary")]
		public bool Primary { get; internal set; }

		[DataMember(Name ="source")]
		public RecoveryOrigin Source { get; internal set; }

		[DataMember(Name ="stage")]
		public string Stage { get; internal set; }

		[Obsolete("Deprecated. Will be removed in 8.0")]
		public RecoveryStartStatus Start { get; internal set; }

		// TODO Rename in 8.0
		[DataMember(Name ="start_time_in_millis")]
		public DateTime? StartTime { get; internal set; }

		// TODO Rename in 8.0
		[DataMember(Name ="stop_time_in_millis")]
		public DateTime? StopTime { get; internal set; }

		[DataMember(Name ="target")]
		public RecoveryOrigin Target { get; internal set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="translog")]
		public RecoveryTranslogStatus Translog { get; internal set; }

		[DataMember(Name ="type")]
		public string Type { get; internal set; }

		[DataMember(Name ="verify_index")]
		public RecoveryVerifyIndex VerifyIndex { get; internal set; }
	}
}
