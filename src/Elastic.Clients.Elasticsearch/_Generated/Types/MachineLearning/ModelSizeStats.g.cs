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

internal sealed partial class ModelSizeStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.ModelSizeStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropAssignmentMemoryBasis = System.Text.Json.JsonEncodedText.Encode("assignment_memory_basis");
	private static readonly System.Text.Json.JsonEncodedText PropBucketAllocationFailuresCount = System.Text.Json.JsonEncodedText.Encode("bucket_allocation_failures_count");
	private static readonly System.Text.Json.JsonEncodedText PropCategorizationStatus = System.Text.Json.JsonEncodedText.Encode("categorization_status");
	private static readonly System.Text.Json.JsonEncodedText PropCategorizedDocCount = System.Text.Json.JsonEncodedText.Encode("categorized_doc_count");
	private static readonly System.Text.Json.JsonEncodedText PropDeadCategoryCount = System.Text.Json.JsonEncodedText.Encode("dead_category_count");
	private static readonly System.Text.Json.JsonEncodedText PropFailedCategoryCount = System.Text.Json.JsonEncodedText.Encode("failed_category_count");
	private static readonly System.Text.Json.JsonEncodedText PropFrequentCategoryCount = System.Text.Json.JsonEncodedText.Encode("frequent_category_count");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropLogTime = System.Text.Json.JsonEncodedText.Encode("log_time");
	private static readonly System.Text.Json.JsonEncodedText PropMemoryStatus = System.Text.Json.JsonEncodedText.Encode("memory_status");
	private static readonly System.Text.Json.JsonEncodedText PropModelBytes = System.Text.Json.JsonEncodedText.Encode("model_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropModelBytesExceeded = System.Text.Json.JsonEncodedText.Encode("model_bytes_exceeded");
	private static readonly System.Text.Json.JsonEncodedText PropModelBytesMemoryLimit = System.Text.Json.JsonEncodedText.Encode("model_bytes_memory_limit");
	private static readonly System.Text.Json.JsonEncodedText PropOutputMemoryAllocatorBytes = System.Text.Json.JsonEncodedText.Encode("output_memory_allocator_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropPeakModelBytes = System.Text.Json.JsonEncodedText.Encode("peak_model_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropRareCategoryCount = System.Text.Json.JsonEncodedText.Encode("rare_category_count");
	private static readonly System.Text.Json.JsonEncodedText PropResultType = System.Text.Json.JsonEncodedText.Encode("result_type");
	private static readonly System.Text.Json.JsonEncodedText PropTimestamp = System.Text.Json.JsonEncodedText.Encode("timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropTotalByFieldCount = System.Text.Json.JsonEncodedText.Encode("total_by_field_count");
	private static readonly System.Text.Json.JsonEncodedText PropTotalCategoryCount = System.Text.Json.JsonEncodedText.Encode("total_category_count");
	private static readonly System.Text.Json.JsonEncodedText PropTotalOverFieldCount = System.Text.Json.JsonEncodedText.Encode("total_over_field_count");
	private static readonly System.Text.Json.JsonEncodedText PropTotalPartitionFieldCount = System.Text.Json.JsonEncodedText.Encode("total_partition_field_count");

	public override Elastic.Clients.Elasticsearch.MachineLearning.ModelSizeStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propAssignmentMemoryBasis = default;
		LocalJsonValue<long> propBucketAllocationFailuresCount = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.CategorizationStatus> propCategorizationStatus = default;
		LocalJsonValue<int> propCategorizedDocCount = default;
		LocalJsonValue<int> propDeadCategoryCount = default;
		LocalJsonValue<int> propFailedCategoryCount = default;
		LocalJsonValue<int> propFrequentCategoryCount = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<System.DateTime> propLogTime = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.MemoryStatus> propMemoryStatus = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize> propModelBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propModelBytesExceeded = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propModelBytesMemoryLimit = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propOutputMemoryAllocatorBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propPeakModelBytes = default;
		LocalJsonValue<int> propRareCategoryCount = default;
		LocalJsonValue<string> propResultType = default;
		LocalJsonValue<long?> propTimestamp = default;
		LocalJsonValue<long> propTotalByFieldCount = default;
		LocalJsonValue<int> propTotalCategoryCount = default;
		LocalJsonValue<long> propTotalOverFieldCount = default;
		LocalJsonValue<long> propTotalPartitionFieldCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAssignmentMemoryBasis.TryReadProperty(ref reader, options, PropAssignmentMemoryBasis, null))
			{
				continue;
			}

			if (propBucketAllocationFailuresCount.TryReadProperty(ref reader, options, PropBucketAllocationFailuresCount, null))
			{
				continue;
			}

			if (propCategorizationStatus.TryReadProperty(ref reader, options, PropCategorizationStatus, null))
			{
				continue;
			}

			if (propCategorizedDocCount.TryReadProperty(ref reader, options, PropCategorizedDocCount, null))
			{
				continue;
			}

			if (propDeadCategoryCount.TryReadProperty(ref reader, options, PropDeadCategoryCount, null))
			{
				continue;
			}

			if (propFailedCategoryCount.TryReadProperty(ref reader, options, PropFailedCategoryCount, null))
			{
				continue;
			}

			if (propFrequentCategoryCount.TryReadProperty(ref reader, options, PropFrequentCategoryCount, null))
			{
				continue;
			}

			if (propJobId.TryReadProperty(ref reader, options, PropJobId, null))
			{
				continue;
			}

			if (propLogTime.TryReadProperty(ref reader, options, PropLogTime, static System.DateTime (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propMemoryStatus.TryReadProperty(ref reader, options, PropMemoryStatus, null))
			{
				continue;
			}

			if (propModelBytes.TryReadProperty(ref reader, options, PropModelBytes, null))
			{
				continue;
			}

			if (propModelBytesExceeded.TryReadProperty(ref reader, options, PropModelBytesExceeded, null))
			{
				continue;
			}

			if (propModelBytesMemoryLimit.TryReadProperty(ref reader, options, PropModelBytesMemoryLimit, null))
			{
				continue;
			}

			if (propOutputMemoryAllocatorBytes.TryReadProperty(ref reader, options, PropOutputMemoryAllocatorBytes, null))
			{
				continue;
			}

			if (propPeakModelBytes.TryReadProperty(ref reader, options, PropPeakModelBytes, null))
			{
				continue;
			}

			if (propRareCategoryCount.TryReadProperty(ref reader, options, PropRareCategoryCount, null))
			{
				continue;
			}

			if (propResultType.TryReadProperty(ref reader, options, PropResultType, null))
			{
				continue;
			}

			if (propTimestamp.TryReadProperty(ref reader, options, PropTimestamp, null))
			{
				continue;
			}

			if (propTotalByFieldCount.TryReadProperty(ref reader, options, PropTotalByFieldCount, null))
			{
				continue;
			}

			if (propTotalCategoryCount.TryReadProperty(ref reader, options, PropTotalCategoryCount, null))
			{
				continue;
			}

			if (propTotalOverFieldCount.TryReadProperty(ref reader, options, PropTotalOverFieldCount, null))
			{
				continue;
			}

			if (propTotalPartitionFieldCount.TryReadProperty(ref reader, options, PropTotalPartitionFieldCount, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.ModelSizeStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AssignmentMemoryBasis = propAssignmentMemoryBasis.Value,
			BucketAllocationFailuresCount = propBucketAllocationFailuresCount.Value,
			CategorizationStatus = propCategorizationStatus.Value,
			CategorizedDocCount = propCategorizedDocCount.Value,
			DeadCategoryCount = propDeadCategoryCount.Value,
			FailedCategoryCount = propFailedCategoryCount.Value,
			FrequentCategoryCount = propFrequentCategoryCount.Value,
			JobId = propJobId.Value,
			LogTime = propLogTime.Value,
			MemoryStatus = propMemoryStatus.Value,
			ModelBytes = propModelBytes.Value,
			ModelBytesExceeded = propModelBytesExceeded.Value,
			ModelBytesMemoryLimit = propModelBytesMemoryLimit.Value,
			OutputMemoryAllocatorBytes = propOutputMemoryAllocatorBytes.Value,
			PeakModelBytes = propPeakModelBytes.Value,
			RareCategoryCount = propRareCategoryCount.Value,
			ResultType = propResultType.Value,
			Timestamp = propTimestamp.Value,
			TotalByFieldCount = propTotalByFieldCount.Value,
			TotalCategoryCount = propTotalCategoryCount.Value,
			TotalOverFieldCount = propTotalOverFieldCount.Value,
			TotalPartitionFieldCount = propTotalPartitionFieldCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.ModelSizeStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAssignmentMemoryBasis, value.AssignmentMemoryBasis, null, null);
		writer.WriteProperty(options, PropBucketAllocationFailuresCount, value.BucketAllocationFailuresCount, null, null);
		writer.WriteProperty(options, PropCategorizationStatus, value.CategorizationStatus, null, null);
		writer.WriteProperty(options, PropCategorizedDocCount, value.CategorizedDocCount, null, null);
		writer.WriteProperty(options, PropDeadCategoryCount, value.DeadCategoryCount, null, null);
		writer.WriteProperty(options, PropFailedCategoryCount, value.FailedCategoryCount, null, null);
		writer.WriteProperty(options, PropFrequentCategoryCount, value.FrequentCategoryCount, null, null);
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropLogTime, value.LogTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime v) => w.WriteValueEx<System.DateTime>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropMemoryStatus, value.MemoryStatus, null, null);
		writer.WriteProperty(options, PropModelBytes, value.ModelBytes, null, null);
		writer.WriteProperty(options, PropModelBytesExceeded, value.ModelBytesExceeded, null, null);
		writer.WriteProperty(options, PropModelBytesMemoryLimit, value.ModelBytesMemoryLimit, null, null);
		writer.WriteProperty(options, PropOutputMemoryAllocatorBytes, value.OutputMemoryAllocatorBytes, null, null);
		writer.WriteProperty(options, PropPeakModelBytes, value.PeakModelBytes, null, null);
		writer.WriteProperty(options, PropRareCategoryCount, value.RareCategoryCount, null, null);
		writer.WriteProperty(options, PropResultType, value.ResultType, null, null);
		writer.WriteProperty(options, PropTimestamp, value.Timestamp, null, null);
		writer.WriteProperty(options, PropTotalByFieldCount, value.TotalByFieldCount, null, null);
		writer.WriteProperty(options, PropTotalCategoryCount, value.TotalCategoryCount, null, null);
		writer.WriteProperty(options, PropTotalOverFieldCount, value.TotalOverFieldCount, null, null);
		writer.WriteProperty(options, PropTotalPartitionFieldCount, value.TotalPartitionFieldCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.ModelSizeStatsConverter))]
public sealed partial class ModelSizeStats
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ModelSizeStats(long bucketAllocationFailuresCount, Elastic.Clients.Elasticsearch.MachineLearning.CategorizationStatus categorizationStatus, int categorizedDocCount, int deadCategoryCount, int failedCategoryCount, int frequentCategoryCount, string jobId, System.DateTime logTime, Elastic.Clients.Elasticsearch.MachineLearning.MemoryStatus memoryStatus, Elastic.Clients.Elasticsearch.ByteSize modelBytes, int rareCategoryCount, string resultType, long totalByFieldCount, int totalCategoryCount, long totalOverFieldCount, long totalPartitionFieldCount)
	{
		BucketAllocationFailuresCount = bucketAllocationFailuresCount;
		CategorizationStatus = categorizationStatus;
		CategorizedDocCount = categorizedDocCount;
		DeadCategoryCount = deadCategoryCount;
		FailedCategoryCount = failedCategoryCount;
		FrequentCategoryCount = frequentCategoryCount;
		JobId = jobId;
		LogTime = logTime;
		MemoryStatus = memoryStatus;
		ModelBytes = modelBytes;
		RareCategoryCount = rareCategoryCount;
		ResultType = resultType;
		TotalByFieldCount = totalByFieldCount;
		TotalCategoryCount = totalCategoryCount;
		TotalOverFieldCount = totalOverFieldCount;
		TotalPartitionFieldCount = totalPartitionFieldCount;
	}
#if NET7_0_OR_GREATER
	public ModelSizeStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ModelSizeStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ModelSizeStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public string? AssignmentMemoryBasis { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long BucketAllocationFailuresCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.CategorizationStatus CategorizationStatus { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int CategorizedDocCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int DeadCategoryCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int FailedCategoryCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int FrequentCategoryCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string JobId { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTime LogTime { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.MemoryStatus MemoryStatus { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.ByteSize ModelBytes { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? ModelBytesExceeded { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? ModelBytesMemoryLimit { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? OutputMemoryAllocatorBytes { get; set; }
	public Elastic.Clients.Elasticsearch.ByteSize? PeakModelBytes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int RareCategoryCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ResultType { get; set; }
	public long? Timestamp { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long TotalByFieldCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int TotalCategoryCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long TotalOverFieldCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long TotalPartitionFieldCount { get; set; }
}