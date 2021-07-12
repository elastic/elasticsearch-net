// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class AnalyzeRepositoryResponse : ResponseBase
	{
		/// <summary>
		/// The number of blobs written to the repository during the test, equal to the ?blob_count request parameter.
		/// </summary>
		[DataMember(Name = "blob_count")]
		public int BlobCount { get; internal set; }

		/// <summary>
		/// The path in the repository under which all the blobs were written during the test.
		/// </summary>
		[DataMember(Name = "blob_path")]
		public string BlobPath { get; internal set; }

		/// <summary>
		/// The number of write operations performed concurrently during the test, equal to the ?concurrency request parameter.
		/// </summary>
		[DataMember(Name = "concurrency")]
		public int Concurrency { get; internal set; }

		/// <summary>
		/// Identifies the node which coordinated the analysis and performed the final cleanup.
		/// </summary>
		[DataMember(Name = "coordinating_node")]
		public NodeIdentity CoordinatingNode { get; internal set; }

		/// <summary>
		/// The limit on the size of a blob written during the test, equal to the ?max_blob_size parameter.
		/// </summary>
		[DataMember(Name = "delete_elapsed")]
		public string DeleteElapsed { get; internal set; }

		/// <summary>
		/// The limit, in bytes, on the size of a blob written during the test, equal to the ?max_blob_size parameter.
		/// </summary>
		[DataMember(Name = "delete_elapsed_nanos")]
		public long DeleteElapsedNanos { get; internal set; }

		/// <summary>
		/// A description of every read and write operation performed during the test. This is only returned if the ?detailed
		/// request parameter is set to true.
		/// </summary>
		[DataMember(Name = "details")]
		public IEnumerable<AnalysisDetails> Details { get; internal set; }

		/// <summary>
		/// The limit on the number of nodes on which early read operations were performed after writing each blob, equal to the
		/// ?early_read_node_count request parameter.
		/// </summary>
		[DataMember(Name = "early_read_node_count")]
		public int EarlyReadNodeCount { get; internal set; }

		/// <summary>
		/// A list of correctness issues detected, which will be empty if the API succeeded. Included to emphasize that a
		/// successful response does not guarantee correct behaviour in future.
		/// </summary>
		[DataMember(Name = "issues_detected")]
		public IEnumerable<string> IssuesDetected { get; internal set; }

		/// <summary>
		/// The limit on the size of a blob written during the test, equal to the ?max_blob_size parameter.
		/// </summary>
		[DataMember(Name = "listing_elapsed")]
		public string ListingElapsed { get; internal set; }

		/// <summary>
		/// The limit, in bytes, on the size of a blob written during the test, equal to the ?max_blob_size parameter.
		/// </summary>
		[DataMember(Name = "listing_elapsed_nanos")]
		public long ListingElapsedNanos { get; internal set; }

		/// <summary>
		/// The limit on the size of a blob written during the test, equal to the ?max_blob_size parameter.
		/// </summary>
		[DataMember(Name = "max_blob_size")]
		public string MaxBlobSize { get; internal set; }

		/// <summary>
		/// The limit, in bytes, on the size of a blob written during the test, equal to the ?max_blob_size parameter.
		/// </summary>
		[DataMember(Name = "max_blob_size_bytes")]
		public long MaxBlobSizeBytes { get; internal set; }

		/// <summary>
		/// The limit on the total size of all blob written during the test, equal to the ?max_total_data_size parameter.
		/// </summary>
		[DataMember(Name = "max_total_data_size")]
		public string MaxTotalDataSize { get; internal set; }

		/// <summary>
		/// The limit, in bytes, on the total size of all blob written during the test, equal to the ?max_total_data_size
		/// parameter.
		/// </summary>
		[DataMember(Name = "max_total_data_size_bytes")]
		public long MaxTotalDataSizeBytes { get; internal set; }

		/// <summary>
		/// The probability of performing rare actions during the test. Equal to the ?rare_action_probability request parameter.
		/// </summary>
		[DataMember(Name = "rare_action_probability")]
		public double RareActionProbability { get; internal set; }

		/// <summary>
		/// The limit on the number of nodes on which read operations were performed after writing each blob, equal to the
		/// ?read_node_count request parameter.
		/// </summary>
		[DataMember(Name = "read_node_count")]
		public int ReadNodeCount { get; internal set; }

		/// <summary>
		/// The name of the repository that was the subject of the analysis.
		/// </summary>
		[DataMember(Name = "repository")]
		public string Repository { get; internal set; }

		/// <summary>
		/// The seed for the pseudo-random number generator used to generate the operations used during the test. Equal to the
		/// ?seed request parameter if set.
		/// </summary>
		[DataMember(Name = "seed")]
		public long Seed { get; internal set; }

		/// <summary>
		/// A collection of statistics that summarise the results of the write operations in the test.
		/// </summary>
		[DataMember(Name = "summary")]
		public Summary Summary { get; internal set; }
	}

	[DataContract]
	public class Blob
	{
		/// <summary>
		/// The name of the blob.
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		/// <summary>
		/// Whether the blob was overwritten while the read operations were ongoing.
		/// </summary>
		[DataMember(Name = "overwritten")]
		public bool Overwritten { get; internal set; }

		/// <summary>
		/// Whether any read operations were started before the write operation completed.
		/// </summary>
		[DataMember(Name = "read_early")]
		public bool ReadEarly { get; internal set; }

		/// <summary>
		/// The position, in bytes, at which read operations completed.
		/// </summary>
		[DataMember(Name = "read_end")]
		public long ReadEnd { get; internal set; }

		/// <summary>
		/// The position, in bytes, at which read operations started.
		/// </summary>
		[DataMember(Name = "read_start")]
		public long ReadStart { get; internal set; }

		/// <summary>
		/// The size of the blob.
		/// </summary>
		[DataMember(Name = "size")]
		public string Size { get; internal set; }

		/// <summary>
		/// The size of the blob in bytes.
		/// </summary>
		[DataMember(Name = "size_bytes")]
		public long SizeBytes { get; internal set; }
	}

	[DataContract]
	public class AnalysisDetails
	{
		/// <summary>
		/// A description of the blob that was written and read.
		/// </summary>
		[DataMember(Name = "blob")]
		public Blob Blob { get; internal set; }

		/// <summary>
		/// The elapsed time spent overwriting this blob. Omitted if the blob was not overwritten.
		/// </summary>
		[DataMember(Name = "overwrite_elapsed")]
		public string OverwriteElapsed { get; internal set; }

		/// <summary>
		/// The elapsed time spent overwriting this blob, in nanoseconds. Omitted if the blob was not overwritten.
		/// </summary>
		[DataMember(Name = "overwrite_elapsed_nanos")]
		public long OverwriteElapsedNanos { get; internal set; }

		/// <summary>
		/// A description of every read operation performed on this blob.
		/// </summary>
		[DataMember(Name = "reads")]
		public IEnumerable<ReadDetails> Reads { get; internal set; }

		/// <summary>
		/// The elapsed time spent writing this blob.
		/// </summary>
		[DataMember(Name = "write_elapsed")]
		public string WriteElapsed { get; internal set; }

		/// <summary>
		/// The elapsed time spent writing this blob, in nanoseconds.
		/// </summary>
		[DataMember(Name = "write_elapsed_nanos")]
		public long WriteElapsedNanos { get; internal set; }

		/// <summary>
		/// Identifies the node which wrote this blob and coordinated the read operations.
		/// </summary>
		[DataMember(Name = "writer_node")]
		public NodeIdentity WriterNode { get; internal set; }

		/// <summary>
		/// The length of time spent waiting for the max_snapshot_bytes_per_sec throttle while writing this blob.
		/// </summary>
		[DataMember(Name = "write_throttled")]
		public string WriteThrottled { get; internal set; }

		/// <summary>
		/// The length of time spent waiting for the max_snapshot_bytes_per_sec throttle while writing this blob, in nanoseconds.
		/// </summary>
		[DataMember(Name = "write_throttled_nanos")]
		public long WriteThrottledNanos { get; internal set; }
	}

	[DataContract]
	public class ReadDetails
	{
		/// <summary>
		/// Whether the read operation may have started before the write operation was complete. Omitted if false.
		/// </summary>
		[DataMember(Name = "before_write_complete")]
		public bool BeforeWriteComplete { get; internal set; }

		/// <summary>
		/// The length of time spent reading this blob. Omitted if the blob was not found.
		/// </summary>
		[DataMember(Name = "elapsed")]
		public string Elapsed { get; internal set; }

		/// <summary>
		/// The length of time spent reading this blob, in nanoseconds. Omitted if the blob was not found.
		/// </summary>
		[DataMember(Name = "elapsed_nanos")]
		public long ElapsedNanos { get; internal set; }

		/// <summary>
		/// The length of time waiting for the first byte of the read operation to be received. Omitted if the blob was not found.
		/// </summary>
		[DataMember(Name = "first_byte_time")]
		public string FirstByteTime { get; internal set; }

		/// <summary>
		/// The length of time waiting for the first byte of the read operation to be received, in nanoseconds. Omitted if the blob
		/// was not found.
		/// </summary>
		[DataMember(Name = "first_byte_time_nanos")]
		public long FirstByteTimeNanos { get; internal set; }

		/// <summary>
		/// Whether the blob was found by this read operation or not. May be false if the read was started before the write
		/// completed, or the write was aborted before completion.
		/// </summary>
		[DataMember(Name = "found")]
		public bool Found { get; internal set; }

		/// <summary>
		/// Identifies the node which performed the read operation.
		/// </summary>
		[DataMember(Name = "node")]
		public NodeIdentity NodeIdentity { get; internal set; }

		/// <summary>
		/// The length of time spent waiting due to the max_restore_bytes_per_sec or indices.recovery.max_bytes_per_sec throttles
		/// during the read of this blob. Omitted if the blob was not found.
		/// </summary>
		[DataMember(Name = "throttled")]
		public string Throttled { get; internal set; }

		/// <summary>
		/// The length of time spent waiting due to the max_restore_bytes_per_sec or indices.recovery.max_bytes_per_sec throttles
		/// during the read of this blob, in nanoseconds. Omitted if the blob was not found.
		/// </summary>
		[DataMember(Name = "throttled_nanos")]
		public long ThrottledNanos { get; internal set; }
	}

	[DataContract]
	public class NodeIdentity
	{
		/// <summary>
		/// The id of the node.
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		/// <summary>
		/// The name of the node.
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; internal set; }
	}

	[DataContract]
	public class Summary
	{
		/// <summary>
		/// Read statistics.
		/// </summary>
		[DataMember(Name = "read")]
		public SummaryStatistics Read { get; internal set; }

		/// <summary>
		/// Write statistics.
		/// </summary>
		[DataMember(Name = "write")]
		public SummaryStatistics Write { get; internal set; }
	}

	[DataContract]
	public class SummaryStatistics
	{
		/// <summary>
		/// The number of write operations performed in the test.
		/// </summary>
		[DataMember(Name = "count")]
		public int Count { get; internal set; }

		/// <summary>
		/// The total elapsed time spent on writing blobs in the test.
		/// </summary>
		[DataMember(Name = "total_elapsed")]
		public string TotalElapsed { get; internal set; }

		/// <summary>
		/// The total elapsed time spent on writing blobs in the test, in nanoseconds.
		/// </summary>
		[DataMember(Name = "total_elapsed_nanos")]
		public long TotalElapsedNanos { get; internal set; }

		/// <summary>
		/// The total size of all the blobs written in the test.
		/// </summary>
		[DataMember(Name = "total_size")]
		public string TotalSize { get; internal set; }

		/// <summary>
		/// The total size of all the blobs written in the test, in bytes.
		/// </summary>
		[DataMember(Name = "total_size_bytes")]
		public long TotalSizeBytes { get; internal set; }

		/// <summary>
		/// The total time spent waiting due to the max_snapshot_bytes_per_sec throttle.
		/// </summary>
		[DataMember(Name = "total_throttled")]
		public string TotalThrottled { get; internal set; }

		/// <summary>
		/// The total time spent waiting due to the max_snapshot_bytes_per_sec throttle, in nanoseconds.
		/// </summary>
		[DataMember(Name = "total_throttled_nanos")]
		public long TotalThrottledNanos { get; internal set; }
	}
}
