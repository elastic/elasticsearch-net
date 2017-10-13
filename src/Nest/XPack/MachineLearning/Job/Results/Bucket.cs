using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class Bucket
	{
		/// <summary>
		/// The unique identifier for the job that these results belong to.
		/// </summary>
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// The start time of the bucket. This timestamp uniquely identifies the bucket.
		/// </summary>
		[JsonProperty("timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }

		/// <summary>
		/// The maximum anomaly score, between 0-100, for any of the bucket influencers. This is an overall, rate-limited score for the job. All the anomaly records in the bucket contribute to this score. This value might be updated as new data is analyzed.
		/// </summary>
		[JsonProperty("anomaly_score")]
		public double AnomalyScore { get; internal set; }

		/// <summary>
		/// The length of the bucket. This value matches the bucket_span that is specified in the job.
		/// </summary>
		[JsonProperty("bucket_span")]
		public Time BucketSpan { get; internal set; }

		/// <summary>
		/// The maximum anomaly_score for any of the bucket influencers. This is the initial value that was calculated at the time the bucket was processed.
		/// </summary>
		[JsonProperty("initial_anomaly_score")]
		public double InitialAnomalyScore { get; internal set; }

		/// <summary>
		/// The number of input data records processed in this bucket.
		/// </summary>
		[JsonProperty("event_count")]
		public long EventCount { get; internal set; }

		/// <summary>
		/// If true, this is an interim result. In other words, the bucket results are calculated based on partial input data.
		/// </summary>
		[JsonProperty("is_interim")]
		public bool IsInterim { get; internal set; }

		/// <summary>
		/// An array of bucket influencer objects.
		/// </summary>
		[JsonProperty("bucket_influencers")]
		public IReadOnlyCollection<BucketInfluencer> BucketInfluencers { get; internal set; } = EmptyReadOnly<BucketInfluencer>.Collection;

		/// <summary>
		/// The amount of time, in milliseconds, that it took to analyze the bucket contents and calculate results.
		/// </summary>
		[JsonProperty("processing_time_ms")]
		public double ProcessingTimeMilliseconds { get; internal set; }

		/// <summary>
		/// An array of partition score objects.
		/// </summary>
		[JsonProperty("partition_scores")]
		public IReadOnlyCollection<PartitionScore> PartitionScores { get; internal set; } = EmptyReadOnly<PartitionScore>.Collection;

		/// <summary>
		/// Internal. This value is always set to bucket.
		/// </summary>
		[JsonProperty("result_type")]
		public string ResultType { get; internal set; }
	}
}
