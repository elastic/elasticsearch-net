using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class BucketInfluencer
	{
		// <summary>
		/// The unique identifier for the job that these results belong to.
		/// </summary>
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// Internal. This value is always set to influencer.
		/// </summary>
		[JsonProperty("result_type")]
		public string ResultType { get; internal set; }

		/// <summary>
		/// The field name of the influencer.
		/// </summary>
		[JsonProperty("influencer_field_name")]
		public string InfluencerFieldName { get; internal set; }

		/// <summary>
		/// The entity that influenced, contributed to, or was to blame for the anomaly.
		/// </summary>
		[JsonProperty("influencer_field_value")]
		public string InfluencerFieldValue { get; internal set; }

		/// <summary>
		/// A normalized score between 0-100, which is based on the probability of the influencer in this bucket aggregated across detectors.
		/// </summary>
		[JsonProperty("initial_influencer_score")]
		public double InitialInfluencerScore { get; internal set; }

		/// <summary>
		/// A normalized score between 0-100, which is based on the probability of the influencer in this bucket aggregated across detectors. Updated by a re-normalization process as new data is analyzed.
		/// </summary>
		[JsonProperty("influencer_score")]
		public double InfluencerScore { get; internal set; }

		/// <summary>
		/// The probability that the influencer has this behavior, in the range 0 to 1. This value can be held to a high precision of over 300 decimal places.
		/// </summary>
		[JsonProperty("probability")]
		public double Probability { get; internal set; }

		/// <summary>
		/// The length of the bucket. This value matches the bucket_span that is specified in the job.
		/// </summary>
		[JsonProperty("bucket_span")]
		public long BucketSpan { get; internal set; }

		/// <summary>
		/// If true, this is an interim result. In other words, the influencer results are calculated based on partial input data.
		/// </summary>
		[JsonProperty("is_interim")]
		public bool IsInterim { get; internal set; }

		/// <summary>
		/// The start time of the bucket for which these results were calculated.
		/// </summary>
		[JsonProperty("timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }
	}
}
