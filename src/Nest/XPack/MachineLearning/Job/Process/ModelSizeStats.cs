// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Provides information about the size and contents of the model.
	/// </summary>
	[DataContract]
	public class ModelSizeStats
	{
		/// <summary>
		/// The number of buckets for which new entities in incoming data were not processed due to
		/// insufficient model memory.
		/// </summary>
		[DataMember(Name = "bucket_allocation_failures_count")]
		public long BucketAllocationFailuresCount { get; internal set; }

		/// <summary>
		/// The number of documents that have had a field categorized.
		/// </summary>
		[DataMember(Name = "categorized_doc_count")]
		public long CategorizedDocCount { get; internal set; }

		/// <summary>
		/// The status of categorization for the job.
		/// </summary>
		[DataMember(Name = "categorization_status")]
		public ModelCategorizationStatus CategorizationStatus { get; internal set; }

		/// <summary>
		/// The number of categories created by categorization that will never be assigned again because another
		/// category's definition makes it a superset of the dead category.
		/// (Dead categories are a side effect of the way categorization has no prior training.)
		/// </summary>
		[DataMember(Name = "dead_category_count")]
		public long DeadCategoryCount { get; internal set; }

		/// <summary>
		/// The number of times that categorization wanted to create a new category but couldn't because the job had hit its model_memory_limit.
		/// This count does not track which specific categories failed to be created. Therefore you cannot use this value to determine
		/// the number of unique categories that were missed.
		/// <para />
		/// Available in Elasticsearch 7.8.0+
		/// </summary>
		[DataMember(Name = "failed_category_count")]
		public long FailedCategoryCount { get; internal set; }

		/// <summary>
		/// The number of categories that match more than 1% of categorized documents.
		/// </summary>
		[DataMember(Name = "frequent_category_count")]
		public long FrequentCategoryCount { get; internal set; }

		/// <summary>
		///  A unique identifier for the job.
		/// </summary>
		[DataMember(Name = "job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// The timestamp according to server time.
		/// </summary>
		[DataMember(Name = "log_time")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset LogTime { get; internal set; }

		/// <summary>
		/// The status of the mathematical models.
		/// </summary>
		[DataMember(Name = "memory_status")]
		public MemoryStatus MemoryStatus { get; internal set; }

		/// <summary>
		/// The number of bytes of memory used by the models. This is the maximum value since the last time the
		/// model was persisted. If the job is closed, this value indicates the latest size.
		/// </summary>
		[DataMember(Name = "model_bytes")]
		public long ModelBytes { get; internal set; }

		/// <summary>
		/// The number of bytes over the high limit for memory usage at the last allocation failure.
		/// </summary>
		[DataMember(Name = "model_bytes_exceeded")]
		public long ModelBytesExceeded { get; internal set; }

		/// <summary>
		/// The upper limit for model memory usage, checked on increasing values.
		/// </summary>
		[DataMember(Name = "model_bytes_memory_limit")]
		public long ModelBytesMemoryLimit { get; internal set; }

		/// <summary>
		/// The number of categories that match just one categorized document.
		/// </summary>
		[DataMember(Name = "rare_category_count")]
		public long RareCategoryCount { get; internal set; }

		/// <summary>
		/// For internal use. The type of result.
		/// </summary>
		[DataMember(Name = "result_type")]
		public string ResultType { get; internal set; }

		/// <summary>
		/// The timestamp according to the timestamp of the data.
		/// </summary>
		[DataMember(Name = "timestamp")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset? Timestamp { get; internal set; }

		/// <summary>
		/// The number of by field values that were analyzed by the models.
		/// </summary>
		[DataMember(Name = "total_by_field_count")]
		public long TotalByFieldCount { get; internal set; }

		/// <summary>
		/// The number of categories created by categorization.
		/// </summary>
		[DataMember(Name = "total_category_count")]
		public long TotalCategoryCount { get; internal set; }

		/// <summary>
		/// The number of over field values that were analyzed by the models.
		/// </summary>
		[DataMember(Name = "total_over_field_count")]
		public long TotalOverFieldCount { get; internal set; }

		/// <summary>
		/// The number of partition field values that were analyzed by the models.
		/// </summary>
		[DataMember(Name = "total_partition_field_count")]
		public long TotalPartitionFieldCount { get; internal set; }
	}
}
