// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class MergesStats
	{
		[DataMember(Name ="current")]
		public long Current { get; set; }

		[DataMember(Name ="current_docs")]
		public long CurrentDocuments { get; set; }

		[DataMember(Name ="current_size")]
		public string CurrentSize { get; set; }

		[DataMember(Name ="current_size_in_bytes")]
		public long CurrentSizeInBytes { get; set; }

		[DataMember(Name ="total")]
		public long Total { get; set; }

		[DataMember(Name ="total_auto_throttle")]
		public string TotalAutoThrottle { get; set; }

		[DataMember(Name ="total_auto_throttle_in_bytes")]
		public long TotalAutoThrottleInBytes { get; set; }

		[DataMember(Name ="total_docs")]
		public long TotalDocuments { get; set; }

		[DataMember(Name ="total_size")]
		public string TotalSize { get; set; }

		[DataMember(Name ="total_size_in_bytes")]
		public long TotalSizeInBytes { get; set; }

		[DataMember(Name ="total_stopped_time")]
		public string TotalStoppedTime { get; set; }

		[DataMember(Name ="total__stopped_time_in_millis")]
		public long TotalStoppedTimeInMilliseconds { get; set; }

		[DataMember(Name ="total_throttled_time")]
		public string TotalThrottledTime { get; set; }

		[DataMember(Name ="total_throttled_time_in_millis")]
		public long TotalThrottledTimeInMilliseconds { get; set; }

		[DataMember(Name ="total_time")]
		public string TotalTime { get; set; }

		[DataMember(Name ="total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; set; }
	}
}
