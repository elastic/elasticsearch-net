// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;

namespace Nest
{
	public class RecoveryIndexStatus
	{
		[Obsolete("Deprecated. Use Size instead. Will be removed in 8.0")]
		public RecoveryBytes Bytes => Size;

		[DataMember(Name = "files")]
		public RecoveryFiles Files { get; internal set; }

		[DataMember(Name = "size")]
		public RecoveryBytes Size { get; internal set; }

		[DataMember(Name = "source_throttle_time_in_millis")]
		public long SourceThrottleTimeInMilliseconds { get; internal set; }

		[DataMember(Name = "target_throttle_time_in_millis")]
		public long TargetThrottleTimeInMilliseconds { get; internal set; }

		[DataMember(Name = "total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }
	}
}
