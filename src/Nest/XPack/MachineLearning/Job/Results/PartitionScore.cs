using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class PartitionScore
	{
		/// <summary>
		/// The name of the partition field that was used in the analysis. This value is present only if it was specified in the detector.
		/// </summary>
		[JsonProperty("partition_field_name")]
		public string PartitionFieldName { get; internal set; }

		/// <summary>
		/// The value of partition_field_name. This value is present only if it was specified in the detector.
		/// </summary>
		[JsonProperty("partition_field_value")]
		public string PartitionFieldValue { get; internal set; }

		/// <summary>
		/// A normalized score between 0-100, which is based on the probability of the anomalousness of this record. This is the initial value that was calculated at the time the bucket was processed.
		/// </summary>
		[JsonProperty("initial_record_score")]
		public double InitialRecordScore { get; internal set; }

		/// <summary>
		/// A normalized score between 0-100, which is based on the probability of the anomalousness of this record. Unlike initial_record_score, this value will be updated by a re-normalization process as new data is analyzed.
		/// </summary>
		[JsonProperty("record_score")]
		public double RecordScore { get; internal set; }

		/// <summary>
		/// The probability of the individual anomaly occurring, in the range 0 to 1. This value can be held to a high precision of over 300 decimal places.
		/// </summary>
		[JsonProperty("probability")]
		public double Probability { get; internal set; }
	}
}
