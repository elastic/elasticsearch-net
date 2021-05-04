// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardMerges
	{
		[DataMember(Name ="current")]
		public long Current { get; internal set; }

		[DataMember(Name ="current_docs")]
		public long CurrentDocuments { get; internal set; }

		[DataMember(Name ="current_size_in_bytes")]
		public long CurrentSizeInBytes { get; internal set; }

		[DataMember(Name ="total")]
		public long Total { get; internal set; }

		[DataMember(Name ="total_auto_throttle_in_bytes")]
		public long TotalAutoThrottleInBytes { get; internal set; }

		[DataMember(Name ="total_docs")]
		public long TotalDocuments { get; internal set; }

		[DataMember(Name ="total_size_in_bytes")]
		public long TotalSizeInBytes { get; internal set; }

		[DataMember(Name ="total_stopped_time_in_millis")]
		public long TotalStoppedTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="total_throttled_time_in_millis")]
		public long TotalThrottledTimeInMilliseconds { get; internal set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }
	}
}
