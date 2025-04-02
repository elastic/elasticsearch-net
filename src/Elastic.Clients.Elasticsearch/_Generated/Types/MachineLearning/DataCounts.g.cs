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

internal sealed partial class DataCountsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DataCounts>
{
	private static readonly System.Text.Json.JsonEncodedText PropBucketCount = System.Text.Json.JsonEncodedText.Encode("bucket_count");
	private static readonly System.Text.Json.JsonEncodedText PropEarliestRecordTimestamp = System.Text.Json.JsonEncodedText.Encode("earliest_record_timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropEmptyBucketCount = System.Text.Json.JsonEncodedText.Encode("empty_bucket_count");
	private static readonly System.Text.Json.JsonEncodedText PropInputBytes = System.Text.Json.JsonEncodedText.Encode("input_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropInputFieldCount = System.Text.Json.JsonEncodedText.Encode("input_field_count");
	private static readonly System.Text.Json.JsonEncodedText PropInputRecordCount = System.Text.Json.JsonEncodedText.Encode("input_record_count");
	private static readonly System.Text.Json.JsonEncodedText PropInvalidDateCount = System.Text.Json.JsonEncodedText.Encode("invalid_date_count");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropLastDataTime = System.Text.Json.JsonEncodedText.Encode("last_data_time");
	private static readonly System.Text.Json.JsonEncodedText PropLatestBucketTimestamp = System.Text.Json.JsonEncodedText.Encode("latest_bucket_timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropLatestEmptyBucketTimestamp = System.Text.Json.JsonEncodedText.Encode("latest_empty_bucket_timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropLatestRecordTimestamp = System.Text.Json.JsonEncodedText.Encode("latest_record_timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropLatestSparseBucketTimestamp = System.Text.Json.JsonEncodedText.Encode("latest_sparse_bucket_timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropLogTime = System.Text.Json.JsonEncodedText.Encode("log_time");
	private static readonly System.Text.Json.JsonEncodedText PropMissingFieldCount = System.Text.Json.JsonEncodedText.Encode("missing_field_count");
	private static readonly System.Text.Json.JsonEncodedText PropOutOfOrderTimestampCount = System.Text.Json.JsonEncodedText.Encode("out_of_order_timestamp_count");
	private static readonly System.Text.Json.JsonEncodedText PropProcessedFieldCount = System.Text.Json.JsonEncodedText.Encode("processed_field_count");
	private static readonly System.Text.Json.JsonEncodedText PropProcessedRecordCount = System.Text.Json.JsonEncodedText.Encode("processed_record_count");
	private static readonly System.Text.Json.JsonEncodedText PropSparseBucketCount = System.Text.Json.JsonEncodedText.Encode("sparse_bucket_count");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DataCounts Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propBucketCount = default;
		LocalJsonValue<long?> propEarliestRecordTimestamp = default;
		LocalJsonValue<long> propEmptyBucketCount = default;
		LocalJsonValue<long> propInputBytes = default;
		LocalJsonValue<long> propInputFieldCount = default;
		LocalJsonValue<long> propInputRecordCount = default;
		LocalJsonValue<long> propInvalidDateCount = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<long?> propLastDataTime = default;
		LocalJsonValue<long?> propLatestBucketTimestamp = default;
		LocalJsonValue<long?> propLatestEmptyBucketTimestamp = default;
		LocalJsonValue<long?> propLatestRecordTimestamp = default;
		LocalJsonValue<long?> propLatestSparseBucketTimestamp = default;
		LocalJsonValue<long?> propLogTime = default;
		LocalJsonValue<long> propMissingFieldCount = default;
		LocalJsonValue<long> propOutOfOrderTimestampCount = default;
		LocalJsonValue<long> propProcessedFieldCount = default;
		LocalJsonValue<long> propProcessedRecordCount = default;
		LocalJsonValue<long> propSparseBucketCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBucketCount.TryReadProperty(ref reader, options, PropBucketCount, null))
			{
				continue;
			}

			if (propEarliestRecordTimestamp.TryReadProperty(ref reader, options, PropEarliestRecordTimestamp, null))
			{
				continue;
			}

			if (propEmptyBucketCount.TryReadProperty(ref reader, options, PropEmptyBucketCount, null))
			{
				continue;
			}

			if (propInputBytes.TryReadProperty(ref reader, options, PropInputBytes, null))
			{
				continue;
			}

			if (propInputFieldCount.TryReadProperty(ref reader, options, PropInputFieldCount, null))
			{
				continue;
			}

			if (propInputRecordCount.TryReadProperty(ref reader, options, PropInputRecordCount, null))
			{
				continue;
			}

			if (propInvalidDateCount.TryReadProperty(ref reader, options, PropInvalidDateCount, null))
			{
				continue;
			}

			if (propJobId.TryReadProperty(ref reader, options, PropJobId, null))
			{
				continue;
			}

			if (propLastDataTime.TryReadProperty(ref reader, options, PropLastDataTime, null))
			{
				continue;
			}

			if (propLatestBucketTimestamp.TryReadProperty(ref reader, options, PropLatestBucketTimestamp, null))
			{
				continue;
			}

			if (propLatestEmptyBucketTimestamp.TryReadProperty(ref reader, options, PropLatestEmptyBucketTimestamp, null))
			{
				continue;
			}

			if (propLatestRecordTimestamp.TryReadProperty(ref reader, options, PropLatestRecordTimestamp, null))
			{
				continue;
			}

			if (propLatestSparseBucketTimestamp.TryReadProperty(ref reader, options, PropLatestSparseBucketTimestamp, null))
			{
				continue;
			}

			if (propLogTime.TryReadProperty(ref reader, options, PropLogTime, null))
			{
				continue;
			}

			if (propMissingFieldCount.TryReadProperty(ref reader, options, PropMissingFieldCount, null))
			{
				continue;
			}

			if (propOutOfOrderTimestampCount.TryReadProperty(ref reader, options, PropOutOfOrderTimestampCount, null))
			{
				continue;
			}

			if (propProcessedFieldCount.TryReadProperty(ref reader, options, PropProcessedFieldCount, null))
			{
				continue;
			}

			if (propProcessedRecordCount.TryReadProperty(ref reader, options, PropProcessedRecordCount, null))
			{
				continue;
			}

			if (propSparseBucketCount.TryReadProperty(ref reader, options, PropSparseBucketCount, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.DataCounts(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BucketCount = propBucketCount.Value,
			EarliestRecordTimestamp = propEarliestRecordTimestamp.Value,
			EmptyBucketCount = propEmptyBucketCount.Value,
			InputBytes = propInputBytes.Value,
			InputFieldCount = propInputFieldCount.Value,
			InputRecordCount = propInputRecordCount.Value,
			InvalidDateCount = propInvalidDateCount.Value,
			JobId = propJobId.Value,
			LastDataTime = propLastDataTime.Value,
			LatestBucketTimestamp = propLatestBucketTimestamp.Value,
			LatestEmptyBucketTimestamp = propLatestEmptyBucketTimestamp.Value,
			LatestRecordTimestamp = propLatestRecordTimestamp.Value,
			LatestSparseBucketTimestamp = propLatestSparseBucketTimestamp.Value,
			LogTime = propLogTime.Value,
			MissingFieldCount = propMissingFieldCount.Value,
			OutOfOrderTimestampCount = propOutOfOrderTimestampCount.Value,
			ProcessedFieldCount = propProcessedFieldCount.Value,
			ProcessedRecordCount = propProcessedRecordCount.Value,
			SparseBucketCount = propSparseBucketCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DataCounts value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBucketCount, value.BucketCount, null, null);
		writer.WriteProperty(options, PropEarliestRecordTimestamp, value.EarliestRecordTimestamp, null, null);
		writer.WriteProperty(options, PropEmptyBucketCount, value.EmptyBucketCount, null, null);
		writer.WriteProperty(options, PropInputBytes, value.InputBytes, null, null);
		writer.WriteProperty(options, PropInputFieldCount, value.InputFieldCount, null, null);
		writer.WriteProperty(options, PropInputRecordCount, value.InputRecordCount, null, null);
		writer.WriteProperty(options, PropInvalidDateCount, value.InvalidDateCount, null, null);
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropLastDataTime, value.LastDataTime, null, null);
		writer.WriteProperty(options, PropLatestBucketTimestamp, value.LatestBucketTimestamp, null, null);
		writer.WriteProperty(options, PropLatestEmptyBucketTimestamp, value.LatestEmptyBucketTimestamp, null, null);
		writer.WriteProperty(options, PropLatestRecordTimestamp, value.LatestRecordTimestamp, null, null);
		writer.WriteProperty(options, PropLatestSparseBucketTimestamp, value.LatestSparseBucketTimestamp, null, null);
		writer.WriteProperty(options, PropLogTime, value.LogTime, null, null);
		writer.WriteProperty(options, PropMissingFieldCount, value.MissingFieldCount, null, null);
		writer.WriteProperty(options, PropOutOfOrderTimestampCount, value.OutOfOrderTimestampCount, null, null);
		writer.WriteProperty(options, PropProcessedFieldCount, value.ProcessedFieldCount, null, null);
		writer.WriteProperty(options, PropProcessedRecordCount, value.ProcessedRecordCount, null, null);
		writer.WriteProperty(options, PropSparseBucketCount, value.SparseBucketCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DataCountsConverter))]
public sealed partial class DataCounts
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataCounts(long bucketCount, long emptyBucketCount, long inputBytes, long inputFieldCount, long inputRecordCount, long invalidDateCount, string jobId, long missingFieldCount, long outOfOrderTimestampCount, long processedFieldCount, long processedRecordCount, long sparseBucketCount)
	{
		BucketCount = bucketCount;
		EmptyBucketCount = emptyBucketCount;
		InputBytes = inputBytes;
		InputFieldCount = inputFieldCount;
		InputRecordCount = inputRecordCount;
		InvalidDateCount = invalidDateCount;
		JobId = jobId;
		MissingFieldCount = missingFieldCount;
		OutOfOrderTimestampCount = outOfOrderTimestampCount;
		ProcessedFieldCount = processedFieldCount;
		ProcessedRecordCount = processedRecordCount;
		SparseBucketCount = sparseBucketCount;
	}
#if NET7_0_OR_GREATER
	public DataCounts()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DataCounts()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataCounts(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	long BucketCount { get; set; }
	public long? EarliestRecordTimestamp { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long EmptyBucketCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long InputBytes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long InputFieldCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long InputRecordCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long InvalidDateCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string JobId { get; set; }
	public long? LastDataTime { get; set; }
	public long? LatestBucketTimestamp { get; set; }
	public long? LatestEmptyBucketTimestamp { get; set; }
	public long? LatestRecordTimestamp { get; set; }
	public long? LatestSparseBucketTimestamp { get; set; }
	public long? LogTime { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long MissingFieldCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long OutOfOrderTimestampCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long ProcessedFieldCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long ProcessedRecordCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long SparseBucketCount { get; set; }
}