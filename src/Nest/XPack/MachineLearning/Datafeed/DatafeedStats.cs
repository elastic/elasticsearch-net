// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class DatafeedStats
	{
		[DataMember(Name = "assignment_explanation")]
		public string AssignmentExplanation { get; internal set; }

		[DataMember(Name = "datafeed_id")]
		public string DatafeedId { get; internal set; }

		[DataMember(Name = "node")]
		public DiscoveryNode Node { get; internal set; }

		[DataMember(Name = "state")]
		public DatafeedState State { get; internal set; }

		[DataMember(Name = "timing_stats")]
		public DatafeedTimingStats TimingStats { get; internal set; }
	}

	[DataContract]
	public class DatafeedTimingStats
	{
		[DataMember(Name = "bucket_count")]
		public long BucketCount { get; internal set; }

		[DataMember(Name = "exponential_average_search_time_per_hour_ms")]
		public double ExponentialAverageSearchTimePerHourMilliseconds { get; internal set; }

		[DataMember(Name = "job_id")]
		public string JobId { get; internal set; }

		[DataMember(Name = "search_count")]
		public long SearchCount { get; internal set; }

		/// <summary>
		/// Total search time in milliseconds
		/// </summary>
		/// <remarks>
		/// Valid only in Elasticsearch 7.4.0+
		/// </remarks>
		[DataMember(Name = "total_search_time_ms")]
		public double TotalSearchTimeMilliseconds { get; internal set; }
	}
}
