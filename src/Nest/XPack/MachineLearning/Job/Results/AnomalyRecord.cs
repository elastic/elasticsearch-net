using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Detailed analyticial results of anomalous activity that has been identified in
	/// the input data based on the detector configuration.
	/// </summary>
	[JsonObject]
	public class AnomalyRecord
	{
		/// <summary>
		/// The unique identifier for the job that these results belong to.
		/// </summary>
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// Internal. This is always set to record.
		/// </summary>
		[JsonProperty("result_type")]
		public string ResultType { get; internal set; }

		/// <summary>
		/// The probability of the individual anomaly occurring, in the range 0 to 1. For example, 0.0000772031.
		/// This value can be held to a high precision of over 300 decimal places, so the <see cref="RecordScore"/>
		/// is provided as a human-readable and friendly interpretation of this.
		/// </summary>
		[JsonProperty("probability")]
		public double Probability { get; internal set; }

		/// <summary>
		/// A normalized score between 0-100, which is based on the probability of the anomalousness of this record.
		/// Unlike <see cref="InitialRecordScore"/>, this value will be updated by a re-normalization process
		/// as new data is analyzed.
		/// </summary>
		[JsonProperty("record_score")]
		public double RecordScore { get; internal set; }

		/// <summary>
		/// A normalized score between 0-100, which is based on the probability of the anomalousness of this record.
		/// This is the initial value that was calculated at the time the bucket was processed.
		/// </summary>
		[JsonProperty("initial_record_score")]
		public double InitialRecordScore { get; internal set; }

		/// <summary>
		/// The length of the bucket. This value matches the bucket_span that is specified in the job.
		/// </summary>
		[JsonProperty("bucket_span")]
		public Time BucketSpan { get; internal set; }

		/// <summary>
		/// A unique identifier for the detector.
		/// </summary>
		[JsonProperty("detector_index")]
		public int DetectorIndex { get; internal set; }

		/// <summary>
		/// If true, this is an interim result. In other words, the anomaly record is calculated
		/// based on partial input data.
		/// </summary>
		[JsonProperty("is_interim")]
		public bool IsInterim { get; internal set; }

		/// <summary>
		/// The start time of the bucket for which these results were calculated.
		/// </summary>
		[JsonProperty("timestamp")]
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		public DateTimeOffset Timestamp { get; internal set; }

		/// <summary>
		/// The function in which the anomaly occurs, as specified in the detector configuration.
		/// </summary>
		[JsonProperty("function")]
		public string Function { get; internal set; }

		/// <summary>
		/// The description of the function in which the anomaly occurs, as specified in the detector configuration.
		/// </summary>
		[JsonProperty("function_description")]
		public string FunctionDescription { get; internal set; }

		/// <summary>
		/// The typical value for the bucket, according to analytical modeling.
		/// </summary>
		/// <remarks>
		/// Additional record properties are added, depending on the fields being analyzed.
		/// For example, if it’s analyzing hostname as a by field, then a field hostname is added to the result document.
		/// This information enables you to filter the anomaly results more easily.
		/// </remarks>
		[JsonProperty("typical")]
		public IReadOnlyCollection<double> Typical { get; internal set; } = EmptyReadOnly<double>.Collection;

		/// <summary>
		/// The actual value for the bucket.
		/// </summary>
		[JsonProperty("actual")]
		public IReadOnlyCollection<double> Actual { get; internal set; } = EmptyReadOnly<double>.Collection;

		/// <summary>
		/// Certain functions require a field to operate on, for example, sum().
		/// For those functions, this value is the name of the field to be analyzed.
		/// </summary>
		[JsonProperty("field_name")]
		public string FieldName { get; internal set; }

		/// <summary>
		/// The name of the analyzed field. This value is present only if it is specified in the detector.
		/// </summary>
		[JsonProperty("by_field_name")]
		public string ByFieldName { get; internal set; }

		/// <summary>
		/// The value of <see cref="ByFieldName"/>. This value is present only if it is specified in the detector.
		/// </summary>
		[JsonProperty("by_field_value")]
		public string ByFieldValue { get; internal set; }

		/// <summary>
		/// For population analysis, an over field must be specified in the detector.
		/// This property contains an array of anomaly records that are the causes for the anomaly that has been
		/// identified for the over field. If no over fields exist, this field is not present.
		/// Contains the most anomalous records for the over_field_name. For scalability reasons,
		/// a maximum of the 10 most significant causes of the anomaly are returned.
		/// As part of the core analytical modeling, these low-level anomaly records are aggregated for their
		/// parent over field record.
		/// </summary>
		[JsonProperty("causes")]
		public IReadOnlyCollection<AnomalyCause> Causes { get; internal set; } = EmptyReadOnly<AnomalyCause>.Collection;

		/// <summary>
		///  If influencers was specified in the detector configuration, then this
		/// contains influencers that contributed to or were to blame for an anomaly.
		/// </summary>
		[JsonProperty("influencers")]
		public IReadOnlyCollection<Influence> Influencers { get; internal set; } = EmptyReadOnly<Influence>.Collection;

		/// <summary>
		/// The name of the over field that was used in the analysis. This value is present only if it was
		/// specified in the detector. Over fields are used in population analysis.
		/// </summary>
		[JsonProperty("over_field_name")]
		public string OverFieldName { get; internal set; }

		/// <summary>
		/// The value of <see cref="OverFieldName"/>. This value is present only if it is specified in the detector.
		/// </summary>
		[JsonProperty("over_field_value")]
		public string OverFieldValue { get; internal set; }

		/// <summary>
		/// The name of the partition field that was used in the analysis.
		/// This value is present only if it was specified in the detector.
		/// </summary>
		[JsonProperty("partition_field_name")]
		public string PartitionFieldName { get; internal set; }

		/// <summary>
		/// The value of <see cref="PartitionFieldName"/>. This value is present only if it is specified in the detector.
		/// </summary>
		[JsonProperty("partition_field_value")]
		public string PartitionFieldValue { get; internal set; }
	}
}
