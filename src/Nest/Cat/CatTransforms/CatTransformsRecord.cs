// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	public class CatTransformsRecord : ICatRecord
	{
		/// <summary>
		/// The timestamp when changes were last detected in the source indices.
		/// </summary>
		[DataMember(Name ="changes_last_detection_time")]
		public string ChangesLastDetectionTime { get; internal set; }

		/// <summary>
		/// Exponential moving average of the duration of the checkpoint, in milliseconds.
		/// </summary>
		[DataMember(Name ="checkpoint_duration_time_exp_avg")]
		public long? CheckpointDurationTimeExpAvg { get; internal set; }

		/// <summary>
		/// (Default) The time the transform was created.
		/// </summary>
		[DataMember(Name ="create_time")]
		public DateTimeOffset CreateTime { get; internal set; }

		/// <summary>
		/// (Default) The description of the transform.
		/// </summary>
		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		/// <summary>
		/// (Default) The destination index for the transform.
		/// </summary>
		[DataMember(Name ="dest_index")]
		public string DestinationIndex { get; internal set; }

		/// <summary>
		/// The number of documents that have been indexed into the destination index for the transform.
		/// </summary>
		[DataMember(Name ="documents_indexed")]
		public long? DocumentsIndexed { get; internal set; }

		/// <summary>
		/// The number of documents that have been processed from the source index of the transform.
		/// </summary>
		[DataMember(Name ="documents_processed")]
		public long? DocumentsProcessed { get; internal set; }

		/// <summary>
		/// (Default) The interval between checks for changes in the source indices when the transform is running continuously. Also determines the
		/// retry interval in the event of transient failures while the transform is searching or indexing. The minimum value is 1s and the maximum
		/// is 1h. The default value is 1m.
		/// </summary>
		[DataMember(Name ="frequency")]
		public Time Frequency { get; internal set; }

		/// <summary>
		/// (Default) Identifier for the transform. This identifier can contain lowercase alphanumeric characters (a-z and 0-9), hyphens, and
		/// underscores. It must start and end with alphanumeric characters.
		/// </summary>
		[DataMember(Name ="id")]
		public string Id { get; internal set; }

		/// <summary>
		/// The number of indexing failures.
		/// </summary>
		[DataMember(Name ="index_failure")]
		public long? IndexFailure { get; internal set; }

		/// <summary>
		/// The amount of time spent indexing, in milliseconds.
		/// </summary>
		[DataMember(Name ="index_time")]
		public long? IndexTime { get; internal set; }

		/// <summary>
		/// The number of indices created.
		/// </summary>
		[DataMember(Name ="index_total")]
		public long? IndexTotal { get; internal set; }

		/// <summary>
		/// Exponential moving average of the number of new documents that have been indexed.
		/// </summary>
		[DataMember(Name ="indexed_documents_exp_avg")]
		public long? IndexedDocumentsExpAvg { get; internal set; }

		/// <summary>
		/// (Default) Defines the initial page size to use for the composite aggregation for each checkpoint. If circuit breaker exceptions occur,
		/// the page size is dynamically adjusted to a lower value. The minimum value is 10 and the maximum is 10,000. The default value is 500.
		/// </summary>
		[DataMember(Name ="max_page_search_size")]
		public long? MaxPageSearchSize { get; internal set; }

		/// <summary>
		/// The number of search or bulk index operations processed. Documents are processed in batches instead of individually.
		/// </summary>
		[DataMember(Name ="pages_processed")]
		public long? PagesProcessed { get; internal set; }

		/// <summary>
		/// (Default) The unique identifier for a pipeline.
		/// </summary>
		[DataMember(Name ="pipeline")]
		public string Pipeline { get; internal set; }

		/// <summary>
		/// Exponential moving average of the number of documents that have been processed.
		/// </summary>
		[DataMember(Name ="processed_documents_exp_avg")]
		public long? ProcessedDocumentsExpAvg { get; internal set; }

		/// <summary>
		/// The amount of time spent processing results, in milliseconds.
		/// </summary>
		[DataMember(Name ="processing_time")]
		public long? ProcessingTime { get; internal set; }

		/// <summary>
		/// If a transform has a failed state, this property provides details about the reason for the failure.
		/// </summary>
		[DataMember(Name ="reason")]
		public string Reason { get; internal set; }

		/// <summary>
		/// The number of search failures.
		/// </summary>
		[DataMember(Name ="search_failure")]
		public long? SearchFailure { get; internal set; }

		/// <summary>
		/// The amount of time spent searching, in milliseconds.
		/// </summary>
		[DataMember(Name ="search_time")]
		public long? SearchTime { get; internal set; }

		/// <summary>
		/// The number of search operations on the source index for the transform.
		/// </summary>
		[DataMember(Name ="search_total")]
		public long? SearchTotal { get; internal set; }

		/// <summary>
		/// (Default) The source indices for the transform. It can be a single index, an index pattern (for example, "myindex*"), an array of indices
		/// (for example, ["index1", "index2"]), or an array of index patterns (for example, ["myindex1-*", "myindex2-*"].
		/// </summary>
		[DataMember(Name ="source_index")]
		[JsonFormatter(typeof(IndicesFormatter))]
		public Indices SourceIndices { get; internal set; }

		/// <summary>
		/// (Default) The status of the transform.
		/// </summary>
		[DataMember(Name ="state")]
		public TransformState State { get; internal set; }

		/// <summary>
		/// (Default) Indicates the type of transform.
		/// </summary>
		[DataMember(Name ="transform_type")]
		public TransformType TransformType { get; internal set; }

		/// <summary>
		/// The number of times the transform has been triggered by the scheduler. For example, the scheduler triggers the transform indexer to
		/// check for updates or ingest new data at an interval specified in the frequency property.
		/// </summary>
		[DataMember(Name ="trigger_count")]
		public long? TriggerCount { get; internal set; }

		/// <summary>
		/// (Default) The version of Elasticsearch that existed on the node when the transform was created.
		/// </summary>
		[DataMember(Name ="version")]
		public string Version { get; internal set; }
	}
}
