// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatJobsRecord : ICatRecord
	{
		/// <summary>
		/// For open anomaly detection jobs only, contains messages relating to the selection of a node to run the job.
		/// </summary>
		[DataMember(Name ="assignment_explanation")]
		public string AssignmentExplanation { get; set; }

		/// <summary>
		/// (Default) The number of bucket results produced by the job.
		/// </summary>
		[DataMember(Name ="buckets.count")]
		public string BucketsCount { get; set; }

		/// <summary>
		/// Exponential moving average of all bucket processing times, in milliseconds.
		/// </summary>
		[DataMember(Name ="buckets.time.exp_avg")]
		public string BucketsTimeExpAvg { get; set; }

		/// <summary>
		/// Exponentially-weighted moving average of bucket processing times calculated in a 1 hour time window, in milliseconds.
		/// </summary>
		[DataMember(Name ="buckets.time.exp_avg_hour")]
		public string BucketsTimeExpAvgHour { get; set; }

		/// <summary>
		/// Maximum among all bucket processing times, in milliseconds.
		/// </summary>
		[DataMember(Name ="buckets.time.max")]
		public string BucketsTimeMax { get; set; }

		/// <summary>
		/// Minimum among all bucket processing times, in milliseconds.
		/// </summary>
		[DataMember(Name ="buckets.time.min")]
		public string BucketsTimeMin { get; set; }

		/// <summary>
		/// Sum of all bucket processing times, in milliseconds.
		/// </summary>
		[DataMember(Name ="buckets.time.total")]
		public string BucketsTimeTotal { get; set; }

		/// <summary>
		/// The number of buckets processed.
		/// </summary>
		[DataMember(Name ="data.buckets")]
		public string DataBuckets { get; set; }

		/// <summary>
		/// The timestamp of the earliest chronologically input document.
		/// </summary>
		[DataMember(Name ="data.earliest_record")]
		public string DataEarliestRecord { get; set; }

		/// <summary>
		/// The number of buckets which did not contain any data. If your data contains many empty buckets, consider increasing your bucket_span
		/// or using functions that are tolerant to gaps in data such as mean, non_null_sum or non_zero_count.
		/// </summary>
		[DataMember(Name ="data.empty_buckets")]
		public string DataEmptyBuckets { get; set; }

		/// <summary>
		/// The number of bytes of input data posted to the anomaly detection job.
		/// </summary>
		[DataMember(Name ="data.input_bytes")]
		public string DataInputBytes { get; set; }

		/// <summary>
		/// The total number of fields in input documents posted to the anomaly detection job. This count includes fields that are not used in the
		/// analysis. However, be aware that if you are using a datafeed, it extracts only the required fields from the documents it retrieves
		/// before posting them to the job.
		/// </summary>
		[DataMember(Name ="data.input_fields")]
		public string DataInputFields { get; set; }

		/// <summary>
		/// The number of input documents posted to the anomaly detection job.
		/// </summary>
		[DataMember(Name ="data.input_records")]
		public string DataInputRecords { get; set; }

		/// <summary>
		/// The number of input documents with either a missing date field or a date that could not be parsed.
		/// </summary>
		[DataMember(Name ="data.invalid_dates")]
		public string DataInvalidDates { get; set; }

		/// <summary>
		/// The timestamp at which data was last analyzed, according to server time.
		/// </summary>
		[DataMember(Name ="data.last")]
		public string DataLast { get; set; }

		/// <summary>
		/// The timestamp of the last bucket that did not contain any data.
		/// </summary>
		[DataMember(Name ="data.last_empty_bucket")]
		public string DataLastEmptyBucket { get; set; }

		/// <summary>
		/// The timestamp of the last bucket that was considered sparse.
		/// </summary>
		[DataMember(Name ="data.last_sparse_bucket")]
		public string DataLastSparseBucket { get; set; }

		/// <summary>
		/// The timestamp of the latest chronologically input document.
		/// </summary>
		[DataMember(Name ="data.latest_record")]
		public string DataLatestRecord { get; set; }

		/// <summary>
		/// The number of input documents that are missing a field that the anomaly detection job is configured to analyze. Input documents
		/// with missing fields are still processed because it is possible that not all fields are missing.
		/// <para />
		/// Note: If you are using datafeeds or posting data to the job in JSON format, a high missing_field_count is often not an indication
		/// of data issues. It is not necessarily a cause for concern.
		/// </summary>
		[DataMember(Name ="data.missing_fields")]
		public string DataMissingFields { get; set; }

		/// <summary>
		/// The number of input documents that are out of time sequence and outside of the latency window. This information is applicable
		/// only when you provide data to the anomaly detection job by using the post data API. These out of order documents are discarded,
		/// since jobs require time series data to be in ascending chronological order.
		/// </summary>
		[DataMember(Name ="data.out_of_order_timestamps")]
		public string DataOutOfOrderTimestamps { get; set; }

		/// <summary>
		/// The total number of fields in all the documents that have been processed by the anomaly detection job. Only fields that are specified
		/// in the detector configuration object contribute to this count. The timestamp is not included in this count.
		/// </summary>
		[DataMember(Name ="data.processed_fields")]
		public string DataProcessedFields { get; set; }

		/// <summary>
		/// (Default) The number of input documents that have been processed by the anomaly detection job. This value includes documents with
		/// missing fields, since they are nonetheless analyzed. If you use datafeeds and have aggregations in your search query, the
		/// processed_record_count is the number of aggregation results processed, not the number of Elasticsearch documents.
		/// </summary>
		[DataMember(Name ="data.processed_records")]
		public string DataProcessedRecords { get; set; }

		/// <summary>
		/// The number of buckets that contained few data points compared to the expected number of data points. If your data contains many
		/// sparse buckets, consider using a longer bucket_span.
		/// </summary>
		[DataMember(Name ="data.sparse_buckets")]
		public string DataSparseBuckets { get; set; }

		/// <summary>
		/// The average memory usage in bytes for forecasts related to the job.
		/// </summary>
		[DataMember(Name="forecasts.memory.avg")]
		public string ForecastsMemoryAvg { get; internal set; }

		/// <summary>
		/// The maximum memory usage in bytes for forecasts related to the job.
		/// </summary>
		[DataMember(Name="forecasts.memory.min")]
		public string ForecastsMemoryMin { get; internal set; }

		/// <summary>
		/// The total memory usage in bytes for forecasts related to the job.
		/// </summary>
		[DataMember(Name="forecasts.memory.total")]
		public string ForecastsMemoryTotal { get; internal set; }

		/// <summary>
		/// The average number of `model_forecast` documents written for forecasts related to the job.
		/// </summary>
		[DataMember(Name="forecasts.records.avg")]
		public string ForecastsRecordsAvg { get; internal set; }

		/// <summary>
		/// The maximum number of `model_forecast` documents written for forecasts related to the job.
		/// </summary>
		[DataMember(Name="forecasts.records.max")]
		public string ForecastsRecordsMax { get; internal set; }

		/// <summary>
		/// The minimum number of `model_forecast` documents written for forecasts relatedto the job.
		/// </summary>
		[DataMember(Name="forecasts.records.min")]
		public string ForecastsRecordsMin { get; internal set; }

		/// <summary>
		/// The total number of `model_forecast` documents written for forecasts related to the job.
		/// </summary>
		[DataMember(Name="forecasts.records.total")]
		public string ForecastsRecordsTotal { get; internal set; }

		/// <summary>
		/// The average runtime in milliseconds for forecasts related to the job.
		/// </summary>
		[DataMember(Name="forecasts.time.avg")]
		public string ForecastsTimeAvg { get; internal set; }

		/// <summary>
		/// The maximum runtime in milliseconds for forecasts related to the job.
		/// </summary>
		[DataMember(Name="forecasts.time.max")]
		public string ForecastsTimeMax { get; internal set; }

		/// <summary>
		/// The minimum runtime in milliseconds for forecasts related to the job.
		/// </summary>
		[DataMember(Name="forecasts.time.min")]
		public string ForecastsTimeMin { get; internal set; }

		/// <summary>
		/// The total runtime in milliseconds for forecasts related to the job.(Default)
		/// </summary>
		[DataMember(Name="forecasts.total")]
		public string ForecastsTotal { get; internal set; }

		/// <summary>
		/// (Default) Identifier for the anomaly detection job.
		/// </summary>
		[DataMember(Name="id")]
		public string Id { get; internal set; }

		/// <summary>
		/// The number of buckets for which new entities in incoming data were not processed due to insufficient model memory. This situation is also
		/// signified by a hard_limit: memory_status property value.
		/// </summary>
		[DataMember(Name="model.bucket_allocation_failures")]
		public string ModelBucketAllocationFailures { get; internal set; }

		/// <summary>
		/// The number of by field values that were analyzed by the models. This value is cumulative for all detectors in the job.
		/// </summary>
		[DataMember(Name="model.by_fields")]
		public string ModelByFields { get; internal set; }

		/// <summary>
		/// (Default) The number of bytes of memory used by the models. This is the maximum value since the last time the model was persisted.
		/// If the job is closed, this value indicates the latest size.
		/// </summary>
		[DataMember(Name="model.bytes")]
		public string ModelBytes { get; internal set; }

		/// <summary>
		/// The number of bytes over the high limit for memory usage at the last allocation failure.
		/// </summary>
		[DataMember(Name="model.bytes")]
		public string ModelBytesExceeded { get; internal set; }

		/// <summary>
		/// The status of categorization for the job.
		/// </summary>
		[DataMember(Name="model.categorization_status")]
		public ModelCategorizationStatus ModelCategorizationStatus { get; internal set; }

		/// <summary>
		/// The number of documents that have had a field categorized.
		/// </summary>
		[DataMember(Name="model.categorized_doc_count")]
		public string ModelCategorizedDocCount { get; internal set; }

		/// <summary>
		/// The number of categories created by categorization that will never be assigned again because another category’s definition makes it a
		/// superset of the dead category. (Dead categories are a side effect of the way categorization has no prior training.)
		/// </summary>
		[DataMember(Name="model.dead_category_count")]
		public string ModelDeadCategoryCount { get; internal set; }

		/// <summary>
		/// The number of categories that match more than 1% of categorized documents.
		/// </summary>
		[DataMember(Name="model.frequent_category_count")]
		public string ModelFrequentCategoryCount { get; internal set; }

		/// <summary>
		/// The timestamp when the model stats were gathered, according to server time.
		/// </summary>
		[DataMember(Name="model.log_time")]
		public string ModelLogTime { get; internal set; }

		/// <summary>
		/// The upper limit for model memory usage, checked on increasing values.
		/// </summary>
		[DataMember(Name="model.memory_limit")]
		public string ModelMemoryLimit { get; internal set; }

		/// <summary>
		/// (Default) The status of the mathematical models.
		/// </summary>
		[DataMember(Name="model.memory_status")]
		public ModelMemoryStatus ModelMemoryStatus { get; internal set; }

		/// <summary>
		/// The number of over field values that were analyzed by the models. This value is cumulative for all detectors in the job.
		/// </summary>
		[DataMember(Name="model.over_fields")]
		public string ModelOverFields { get; internal set; }

		/// <summary>
		/// The number of partition field values that were analyzed by the models. This value is cumulative for all detectors in the job.
		/// </summary>
		[DataMember(Name="model.partition_fields")]
		public string ModelPartitionFields { get; internal set; }

		/// <summary>
		/// The number of categories that match just one categorized document.
		/// </summary>
		[DataMember(Name="model.rare_category_count")]
		public string ModelRareCategoryCount { get; internal set; }

		/// <summary>
		/// The timestamp of the last record when the model stats were gathered.
		/// </summary>
		[DataMember(Name="model.timestamp")]
		public string ModelTimestamp { get; internal set; }

		/// <summary>
		/// The network address of the node. Contains properties for the node that runs the job. This information is available only for open jobs.
		/// </summary>
		[DataMember(Name="node.address")]
		public string NodeAddress { get; internal set; }

		/// <summary>
		/// The ephemeral ID of the node.The unique identifier of the node. Contains properties for the node that runs the job. This information
		/// is available only for open jobs.
		/// </summary>
		[DataMember(Name="node.ephemeral_id")]
		public string NodeEphemeralId { get; internal set; }

		/// <summary>
		/// The unique identifier of the node. Contains properties for the node that runs the job. This information is available only for open jobs.
		/// </summary>
		[DataMember(Name="node.id")]
		public string NodeId { get; internal set; }

		/// <summary>
		/// The node name. Contains properties for the node that runs the job. This information is available only for open jobs.
		/// </summary>
		[DataMember(Name="node.name")]
		public string NodeName { get; internal set; }

		/// <summary>
		/// For open jobs only, the elapsed time for which the job has been open.
		/// </summary>
		[DataMember(Name="opened_time")]
		public string OpenedTime { get; internal set; }

		/// <summary>
		/// (Default) The status of the anomaly detection job.
		/// </summary>
		[DataMember(Name="state")]
		public JobState State { get; internal set; }
	}
}
