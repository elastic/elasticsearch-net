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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ml
{
	public partial class ModelSizeStats
	{
		[JsonInclude]
		[JsonPropertyName("assignment_memory_basis")]
		public string? AssignmentMemoryBasis { get; init; }

		[JsonInclude]
		[JsonPropertyName("bucket_allocation_failures_count")]
		public long BucketAllocationFailuresCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("categorization_status")]
		public Elastic.Clients.Elasticsearch.Ml.CategorizationStatus CategorizationStatus { get; init; }

		[JsonInclude]
		[JsonPropertyName("categorized_doc_count")]
		public int CategorizedDocCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("dead_category_count")]
		public int DeadCategoryCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("failed_category_count")]
		public int FailedCategoryCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("frequent_category_count")]
		public int FrequentCategoryCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("job_id")]
		public string JobId { get; init; }

		[JsonInclude]
		[JsonPropertyName("log_time")]
		public DateTimeOffset LogTime { get; init; }

		[JsonInclude]
		[JsonPropertyName("memory_status")]
		public Elastic.Clients.Elasticsearch.Ml.MemoryStatus MemoryStatus { get; init; }

		[JsonInclude]
		[JsonPropertyName("model_bytes")]
		public Elastic.Clients.Elasticsearch.ByteSize ModelBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("model_bytes_exceeded")]
		public Elastic.Clients.Elasticsearch.ByteSize? ModelBytesExceeded { get; init; }

		[JsonInclude]
		[JsonPropertyName("model_bytes_memory_limit")]
		public Elastic.Clients.Elasticsearch.ByteSize? ModelBytesMemoryLimit { get; init; }

		[JsonInclude]
		[JsonPropertyName("peak_model_bytes")]
		public Elastic.Clients.Elasticsearch.ByteSize? PeakModelBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("rare_category_count")]
		public int RareCategoryCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("result_type")]
		public string ResultType { get; init; }

		[JsonInclude]
		[JsonPropertyName("timestamp")]
		public long? Timestamp { get; init; }

		[JsonInclude]
		[JsonPropertyName("total_by_field_count")]
		public long TotalByFieldCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("total_category_count")]
		public int TotalCategoryCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("total_over_field_count")]
		public long TotalOverFieldCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("total_partition_field_count")]
		public long TotalPartitionFieldCount { get; init; }
	}
}