// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class AnomalyConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.Anomaly>
{
	private static readonly System.Text.Json.JsonEncodedText PropActual = System.Text.Json.JsonEncodedText.Encode("actual");
	private static readonly System.Text.Json.JsonEncodedText PropAnomalyScoreExplanation = System.Text.Json.JsonEncodedText.Encode("anomaly_score_explanation");
	private static readonly System.Text.Json.JsonEncodedText PropBucketSpan = System.Text.Json.JsonEncodedText.Encode("bucket_span");
	private static readonly System.Text.Json.JsonEncodedText PropByFieldName = System.Text.Json.JsonEncodedText.Encode("by_field_name");
	private static readonly System.Text.Json.JsonEncodedText PropByFieldValue = System.Text.Json.JsonEncodedText.Encode("by_field_value");
	private static readonly System.Text.Json.JsonEncodedText PropCauses = System.Text.Json.JsonEncodedText.Encode("causes");
	private static readonly System.Text.Json.JsonEncodedText PropDetectorIndex = System.Text.Json.JsonEncodedText.Encode("detector_index");
	private static readonly System.Text.Json.JsonEncodedText PropFieldName = System.Text.Json.JsonEncodedText.Encode("field_name");
	private static readonly System.Text.Json.JsonEncodedText PropFunction = System.Text.Json.JsonEncodedText.Encode("function");
	private static readonly System.Text.Json.JsonEncodedText PropFunctionDescription = System.Text.Json.JsonEncodedText.Encode("function_description");
	private static readonly System.Text.Json.JsonEncodedText PropGeoResults = System.Text.Json.JsonEncodedText.Encode("geo_results");
	private static readonly System.Text.Json.JsonEncodedText PropInfluencers = System.Text.Json.JsonEncodedText.Encode("influencers");
	private static readonly System.Text.Json.JsonEncodedText PropInitialRecordScore = System.Text.Json.JsonEncodedText.Encode("initial_record_score");
	private static readonly System.Text.Json.JsonEncodedText PropIsInterim = System.Text.Json.JsonEncodedText.Encode("is_interim");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropOverFieldName = System.Text.Json.JsonEncodedText.Encode("over_field_name");
	private static readonly System.Text.Json.JsonEncodedText PropOverFieldValue = System.Text.Json.JsonEncodedText.Encode("over_field_value");
	private static readonly System.Text.Json.JsonEncodedText PropPartitionFieldName = System.Text.Json.JsonEncodedText.Encode("partition_field_name");
	private static readonly System.Text.Json.JsonEncodedText PropPartitionFieldValue = System.Text.Json.JsonEncodedText.Encode("partition_field_value");
	private static readonly System.Text.Json.JsonEncodedText PropProbability = System.Text.Json.JsonEncodedText.Encode("probability");
	private static readonly System.Text.Json.JsonEncodedText PropRecordScore = System.Text.Json.JsonEncodedText.Encode("record_score");
	private static readonly System.Text.Json.JsonEncodedText PropResultType = System.Text.Json.JsonEncodedText.Encode("result_type");
	private static readonly System.Text.Json.JsonEncodedText PropTimestamp = System.Text.Json.JsonEncodedText.Encode("timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropTypical = System.Text.Json.JsonEncodedText.Encode("typical");

	public override Elastic.Clients.Elasticsearch.MachineLearning.Anomaly Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<double>?> propActual = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.AnomalyExplanation?> propAnomalyScoreExplanation = default;
		LocalJsonValue<System.TimeSpan> propBucketSpan = default;
		LocalJsonValue<string?> propByFieldName = default;
		LocalJsonValue<string?> propByFieldValue = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.AnomalyCause>?> propCauses = default;
		LocalJsonValue<int> propDetectorIndex = default;
		LocalJsonValue<string?> propFieldName = default;
		LocalJsonValue<string?> propFunction = default;
		LocalJsonValue<string?> propFunctionDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.GeoResults?> propGeoResults = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.Influence>?> propInfluencers = default;
		LocalJsonValue<double> propInitialRecordScore = default;
		LocalJsonValue<bool> propIsInterim = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<string?> propOverFieldName = default;
		LocalJsonValue<string?> propOverFieldValue = default;
		LocalJsonValue<string?> propPartitionFieldName = default;
		LocalJsonValue<string?> propPartitionFieldValue = default;
		LocalJsonValue<double> propProbability = default;
		LocalJsonValue<double> propRecordScore = default;
		LocalJsonValue<string> propResultType = default;
		LocalJsonValue<System.DateTimeOffset> propTimestamp = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<double>?> propTypical = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propActual.TryReadProperty(ref reader, options, PropActual, static System.Collections.Generic.IReadOnlyCollection<double>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<double>(o, null)))
			{
				continue;
			}

			if (propAnomalyScoreExplanation.TryReadProperty(ref reader, options, PropAnomalyScoreExplanation, null))
			{
				continue;
			}

			if (propBucketSpan.TryReadProperty(ref reader, options, PropBucketSpan, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanSecondsMarker))))
			{
				continue;
			}

			if (propByFieldName.TryReadProperty(ref reader, options, PropByFieldName, null))
			{
				continue;
			}

			if (propByFieldValue.TryReadProperty(ref reader, options, PropByFieldValue, null))
			{
				continue;
			}

			if (propCauses.TryReadProperty(ref reader, options, PropCauses, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.AnomalyCause>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.AnomalyCause>(o, null)))
			{
				continue;
			}

			if (propDetectorIndex.TryReadProperty(ref reader, options, PropDetectorIndex, null))
			{
				continue;
			}

			if (propFieldName.TryReadProperty(ref reader, options, PropFieldName, null))
			{
				continue;
			}

			if (propFunction.TryReadProperty(ref reader, options, PropFunction, null))
			{
				continue;
			}

			if (propFunctionDescription.TryReadProperty(ref reader, options, PropFunctionDescription, null))
			{
				continue;
			}

			if (propGeoResults.TryReadProperty(ref reader, options, PropGeoResults, null))
			{
				continue;
			}

			if (propInfluencers.TryReadProperty(ref reader, options, PropInfluencers, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.Influence>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.Influence>(o, null)))
			{
				continue;
			}

			if (propInitialRecordScore.TryReadProperty(ref reader, options, PropInitialRecordScore, null))
			{
				continue;
			}

			if (propIsInterim.TryReadProperty(ref reader, options, PropIsInterim, null))
			{
				continue;
			}

			if (propJobId.TryReadProperty(ref reader, options, PropJobId, null))
			{
				continue;
			}

			if (propOverFieldName.TryReadProperty(ref reader, options, PropOverFieldName, null))
			{
				continue;
			}

			if (propOverFieldValue.TryReadProperty(ref reader, options, PropOverFieldValue, null))
			{
				continue;
			}

			if (propPartitionFieldName.TryReadProperty(ref reader, options, PropPartitionFieldName, null))
			{
				continue;
			}

			if (propPartitionFieldValue.TryReadProperty(ref reader, options, PropPartitionFieldValue, null))
			{
				continue;
			}

			if (propProbability.TryReadProperty(ref reader, options, PropProbability, null))
			{
				continue;
			}

			if (propRecordScore.TryReadProperty(ref reader, options, PropRecordScore, null))
			{
				continue;
			}

			if (propResultType.TryReadProperty(ref reader, options, PropResultType, null))
			{
				continue;
			}

			if (propTimestamp.TryReadProperty(ref reader, options, PropTimestamp, static System.DateTimeOffset (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propTypical.TryReadProperty(ref reader, options, PropTypical, static System.Collections.Generic.IReadOnlyCollection<double>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<double>(o, null)))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.MachineLearning.Anomaly(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Actual = propActual.Value,
			AnomalyScoreExplanation = propAnomalyScoreExplanation.Value,
			BucketSpan = propBucketSpan.Value,
			ByFieldName = propByFieldName.Value,
			ByFieldValue = propByFieldValue.Value,
			Causes = propCauses.Value,
			DetectorIndex = propDetectorIndex.Value,
			FieldName = propFieldName.Value,
			Function = propFunction.Value,
			FunctionDescription = propFunctionDescription.Value,
			GeoResults = propGeoResults.Value,
			Influencers = propInfluencers.Value,
			InitialRecordScore = propInitialRecordScore.Value,
			IsInterim = propIsInterim.Value,
			JobId = propJobId.Value,
			OverFieldName = propOverFieldName.Value,
			OverFieldValue = propOverFieldValue.Value,
			PartitionFieldName = propPartitionFieldName.Value,
			PartitionFieldValue = propPartitionFieldValue.Value,
			Probability = propProbability.Value,
			RecordScore = propRecordScore.Value,
			ResultType = propResultType.Value,
			Timestamp = propTimestamp.Value,
			Typical = propTypical.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.Anomaly value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropActual, value.Actual, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<double>? v) => w.WriteCollectionValue<double>(o, v, null));
		writer.WriteProperty(options, PropAnomalyScoreExplanation, value.AnomalyScoreExplanation, null, null);
		writer.WriteProperty(options, PropBucketSpan, value.BucketSpan, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanSecondsMarker)));
		writer.WriteProperty(options, PropByFieldName, value.ByFieldName, null, null);
		writer.WriteProperty(options, PropByFieldValue, value.ByFieldValue, null, null);
		writer.WriteProperty(options, PropCauses, value.Causes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.AnomalyCause>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.AnomalyCause>(o, v, null));
		writer.WriteProperty(options, PropDetectorIndex, value.DetectorIndex, null, null);
		writer.WriteProperty(options, PropFieldName, value.FieldName, null, null);
		writer.WriteProperty(options, PropFunction, value.Function, null, null);
		writer.WriteProperty(options, PropFunctionDescription, value.FunctionDescription, null, null);
		writer.WriteProperty(options, PropGeoResults, value.GeoResults, null, null);
		writer.WriteProperty(options, PropInfluencers, value.Influencers, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.Influence>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.Influence>(o, v, null));
		writer.WriteProperty(options, PropInitialRecordScore, value.InitialRecordScore, null, null);
		writer.WriteProperty(options, PropIsInterim, value.IsInterim, null, null);
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropOverFieldName, value.OverFieldName, null, null);
		writer.WriteProperty(options, PropOverFieldValue, value.OverFieldValue, null, null);
		writer.WriteProperty(options, PropPartitionFieldName, value.PartitionFieldName, null, null);
		writer.WriteProperty(options, PropPartitionFieldValue, value.PartitionFieldValue, null, null);
		writer.WriteProperty(options, PropProbability, value.Probability, null, null);
		writer.WriteProperty(options, PropRecordScore, value.RecordScore, null, null);
		writer.WriteProperty(options, PropResultType, value.ResultType, null, null);
		writer.WriteProperty(options, PropTimestamp, value.Timestamp, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropTypical, value.Typical, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<double>? v) => w.WriteCollectionValue<double>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.AnomalyConverter))]
public sealed partial class Anomaly
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Anomaly(System.TimeSpan bucketSpan, int detectorIndex, double initialRecordScore, bool isInterim, string jobId, double probability, double recordScore, string resultType, System.DateTimeOffset timestamp)
	{
		BucketSpan = bucketSpan;
		DetectorIndex = detectorIndex;
		InitialRecordScore = initialRecordScore;
		IsInterim = isInterim;
		JobId = jobId;
		Probability = probability;
		RecordScore = recordScore;
		ResultType = resultType;
		Timestamp = timestamp;
	}
#if NET7_0_OR_GREATER
	public Anomaly()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Anomaly()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Anomaly(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The actual value for the bucket.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<double>? Actual { get; set; }

	/// <summary>
	/// <para>
	/// Information about the factors impacting the initial anomaly score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.AnomalyExplanation? AnomalyScoreExplanation { get; set; }

	/// <summary>
	/// <para>
	/// The length of the bucket in seconds. This value matches the <c>bucket_span</c> that is specified in the job.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan BucketSpan { get; set; }

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to their own history. It is used for finding unusual values in the context of the split.
	/// </para>
	/// </summary>
	public string? ByFieldName { get; set; }

	/// <summary>
	/// <para>
	/// The value of <c>by_field_name</c>.
	/// </para>
	/// </summary>
	public string? ByFieldValue { get; set; }

	/// <summary>
	/// <para>
	/// For population analysis, an over field must be specified in the detector. This property contains an array of anomaly records that are the causes for the anomaly that has been identified for the over field. This sub-resource contains the most anomalous records for the <c>over_field_name</c>. For scalability reasons, a maximum of the 10 most significant causes of the anomaly are returned. As part of the core analytical modeling, these low-level anomaly records are aggregated for their parent over field record. The <c>causes</c> resource contains similar elements to the record resource, namely <c>actual</c>, <c>typical</c>, <c>geo_results.actual_point</c>, <c>geo_results.typical_point</c>, <c>*_field_name</c> and <c>*_field_value</c>. Probability and scores are not applicable to causes.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.AnomalyCause>? Causes { get; set; }

	/// <summary>
	/// <para>
	/// A unique identifier for the detector.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int DetectorIndex { get; set; }

	/// <summary>
	/// <para>
	/// Certain functions require a field to operate on, for example, <c>sum()</c>. For those functions, this value is the name of the field to be analyzed.
	/// </para>
	/// </summary>
	public string? FieldName { get; set; }

	/// <summary>
	/// <para>
	/// The function in which the anomaly occurs, as specified in the detector configuration. For example, <c>max</c>.
	/// </para>
	/// </summary>
	public string? Function { get; set; }

	/// <summary>
	/// <para>
	/// The description of the function in which the anomaly occurs, as specified in the detector configuration.
	/// </para>
	/// </summary>
	public string? FunctionDescription { get; set; }

	/// <summary>
	/// <para>
	/// If the detector function is <c>lat_long</c>, this object contains comma delimited strings for the latitude and longitude of the actual and typical values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.GeoResults? GeoResults { get; set; }

	/// <summary>
	/// <para>
	/// If influencers were specified in the detector configuration, this array contains influencers that contributed to or were to blame for an anomaly.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.Influence>? Influencers { get; set; }

	/// <summary>
	/// <para>
	/// A normalized score between 0-100, which is based on the probability of the anomalousness of this record. This is the initial value that was calculated at the time the bucket was processed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double InitialRecordScore { get; set; }

	/// <summary>
	/// <para>
	/// If true, this is an interim result. In other words, the results are calculated based on partial input data.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool IsInterim { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string JobId { get; set; }

	/// <summary>
	/// <para>
	/// The field used to split the data. In particular, this property is used for analyzing the splits with respect to the history of all splits. It is used for finding unusual values in the population of all splits.
	/// </para>
	/// </summary>
	public string? OverFieldName { get; set; }

	/// <summary>
	/// <para>
	/// The value of <c>over_field_name</c>.
	/// </para>
	/// </summary>
	public string? OverFieldValue { get; set; }

	/// <summary>
	/// <para>
	/// The field used to segment the analysis. When you use this property, you have completely independent baselines for each value of this field.
	/// </para>
	/// </summary>
	public string? PartitionFieldName { get; set; }

	/// <summary>
	/// <para>
	/// The value of <c>partition_field_name</c>.
	/// </para>
	/// </summary>
	public string? PartitionFieldValue { get; set; }

	/// <summary>
	/// <para>
	/// The probability of the individual anomaly occurring, in the range 0 to 1. For example, <c>0.0000772031</c>. This value can be held to a high precision of over 300 decimal places, so the <c>record_score</c> is provided as a human-readable and friendly interpretation of this.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Probability { get; set; }

	/// <summary>
	/// <para>
	/// A normalized score between 0-100, which is based on the probability of the anomalousness of this record. Unlike <c>initial_record_score</c>, this value will be updated by a re-normalization process as new data is analyzed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double RecordScore { get; set; }

	/// <summary>
	/// <para>
	/// Internal. This is always set to <c>record</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ResultType { get; set; }

	/// <summary>
	/// <para>
	/// The start time of the bucket for which these results were calculated.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTimeOffset Timestamp { get; set; }

	/// <summary>
	/// <para>
	/// The typical value for the bucket, according to analytical modeling.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<double>? Typical { get; set; }
}