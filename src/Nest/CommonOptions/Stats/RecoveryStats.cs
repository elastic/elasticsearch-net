// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class RecoveryStats
	{
		[DataMember(Name ="current_as_source")]
		public long CurrentAsSource { get; set; }

		[DataMember(Name ="current_as_target")]
		public long CurrentAsTarget { get; set; }

		[DataMember(Name ="throttle_time")]
		public string ThrottleTime { get; set; }

		[DataMember(Name ="throttle_time_in_millis")]
		public long ThrottleTimeInMilliseconds { get; set; }
	}
}
