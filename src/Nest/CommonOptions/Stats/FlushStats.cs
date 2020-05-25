// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

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
