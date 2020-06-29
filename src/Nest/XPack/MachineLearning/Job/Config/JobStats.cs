// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Provides statistics about the operation progress of a machine learning job.
	/// </summary>
	[DataContract]
	public class JobStats
	{
		/// <summary>
		/// For open jobs only, contains messages relating to the selection of a node to run the job.
		/// </summary>
		[DataMember(Name = "assignment_explanation")]
		public string AssignmentExplanation { get; internal set; }

		/// <summary>
		/// Describes the number of records processed and any related error counts.
		/// </summary>
		[DataMember(Name = "data_counts")]
		public DataCounts DataCounts { get; internal set; }

		/// <summary>
		/// Indicates that the process of deleting the job is in progress but not yet completed.
		/// It is only reported when true.
		/// </summary>
		[DataMember(Name = "deleting")]
		public bool? Deleting { get; internal set; }

		/// <summary>
		///  Contains job statistics if job contains a forecast.
		/// </summary>
		[DataMember(Name = "forecasts_stats")]
		public JobForecastStatistics Forecasts { get; internal set; }

		/// <summary>
		/// A unique identifier for the job.
		/// </summary>
		[DataMember(Name = "job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// Provides information about the size and contents of the model.
		/// </summary>
		[DataMember(Name = "model_size_stats")]
		public ModelSizeStats ModelSizeStats { get; internal set; }

		/// <summary>
		/// For open jobs only, contains information about the node where the job runs.
		/// </summary>
		[DataMember(Name = "node")]
		public DiscoveryNode Node { get; internal set; }

		/// <summary>
		/// For open jobs only, the elapsed time for which the job has been open.
		/// </summary>
		[DataMember(Name = "open_time")]
		public Time OpenTime { get; internal set; }

		/// <summary>
		/// The status of the job.
		/// </summary>
		[DataMember(Name = "state")]
		public JobState State { get; internal set; }

		/// <summary>
		/// Timing-related statistics about the job's progress
		/// <para />
		/// Valid only in Elasticsearch 7.3.0+
		/// </summary>
		[DataMember(Name = "timing_stats")]
		public TimingStats TimingStats { get; internal set; }
	}

	public class TimingStats
	{
		[DataMember(Name = "job_id")]
		public string JobId { get; internal set; }

		[DataMember(Name = "bucket_count")]
		public long BucketCount { get; internal set; }

		/// <summary>
		/// Minimum among all bucket processing times in milliseconds
		/// </summary>
		[DataMember(Name = "minimum_bucket_processing_time_ms")]
		public double MinimumBucketProcessingTimeMilliseconds { get; internal set; }

		/// <summary>
		/// Maximum among all bucket processing times in milliseconds
		/// </summary>
		[DataMember(Name = "maximum_bucket_processing_time_ms")]
		public double MaximumBucketProcessingTimeMilliseconds { get; internal set; }

		/// <summary>
		/// Average of all bucket processing times in milliseconds
		/// </summary>
		[DataMember(Name = "average_bucket_processing_time_ms")]
		public double AverageBucketProcessingTimeMilliseconds { get; internal set; }

		/// <summary>
		/// Exponential moving average of all bucket processing times in milliseconds
		/// </summary>
		[DataMember(Name = "exponential_average_bucket_processing_time_ms")]
		public double ExponentialAverageBucketProcessingTimeMilliseconds { get; internal set; }

		/// <summary>
		/// Exponential moving average of all bucket processing times in milliseconds
		/// </summary>
		/// <remarks>Valid in Elasticsearch 7.4.0+</remarks>
		[DataMember(Name = "exponential_average_bucket_processing_time_per_hour_ms")]
		public double ExponentialAverageBucketProcessingTimePerHourMilliseconds { get; internal set; }

		/// <summary>
		/// Sum of all bucket processing times, in milliseconds.
		/// </summary>
		[DataMember(Name = "total_bucket_processing_time_ms")]
		public double TotalBucketProcessingTimeMilliseconds { get; internal set; }
	}

	public class JobForecastStatistics
	{
		/// <summary>
		/// A value of 0 indicates that forecasts do not exist for this job.
		/// A value of 1 indicates that at least one forecast exists.
		/// </summary>
		[DataMember(Name = "forecasted_jobs")]
		public long ForecastedJobs { get; internal set; }

		/// <summary>
		/// Statistics about the memory usage: minimum, maximum, average and total.
		/// </summary>
		[DataMember(Name = "memory_bytes")]
		public JobStatistics MemoryBytes { get; internal set; }

		/// <summary>
		/// Statistics about the forecast runtime in milliseconds: minimum, maximum, average and total.
		/// </summary>
		[DataMember(Name = "processing_time_ms")]
		public JobStatistics ProcessingTimeMilliseconds { get; internal set; }

		/// <summary>
		/// Statistics about the number of forecast records: minimum, maximum, average and total.
		/// </summary>
		[DataMember(Name = "records")]
		public JobStatistics Records { get; internal set; }

		/// <summary>
		/// Counts per forecast status.
		/// </summary>
		[DataMember(Name = "status")]
		public IReadOnlyDictionary<string, long> Status { get; internal set; } = EmptyReadOnly<string, long>.Dictionary;

		/// <summary>
		/// The number of forecasts currently available for this model.
		/// </summary>
		[DataMember(Name = "total")]
		public long Total { get; internal set; }

		public class JobStatistics
		{
			[DataMember(Name = "avg")]
			public double Average { get; internal set; }

			[DataMember(Name = "max")]
			public double Maximum { get; internal set; }

			[DataMember(Name = "min")]
			public double Minimum { get; internal set; }

			[DataMember(Name = "total")]
			public double Total { get; internal set; }
		}
	}
}
