// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class DataStreamsStatsResponse : ResponseBase
	{
		/// <summary>
		/// Contains information about shards that attempted to execute the request.
		/// </summary>
		[DataMember(Name = "_shards")]
		public ShardStatistics Shards { get; internal set; }

		/// <summary>
		/// Total number of selected data streams.
		/// </summary>
		[DataMember(Name = "data_stream_count")]
		public int DataStreamCount { get; internal set; }

		/// <summary>
		/// Total number of backing indices for the selected data streams.
		/// </summary>
		[DataMember(Name = "backing_indices")]
		public int BackingIndices { get; internal set; }

		/// <summary>
		/// Total size of all shards for the data stream's backing indices.
		/// This property is only returned if the `human` query parameter is <c>true</c>
		/// on the request
		/// </summary>
		[DataMember(Name = "total_store_size")]
		public string TotalStoreSize { get; internal set; }

		/// <summary>
		/// Total size, in bytes, of all shards for the selected data streams.
		/// </summary>
		[DataMember(Name = "total_store_size_bytes")]
		public long TotalStoreSizeBytes { get; internal set; }

		/// <summary>
		/// Statistics for the selected data streams.
		/// </summary>
		[DataMember(Name = "data_streams")]
		public IReadOnlyCollection<DataStreamStats> DataStreams { get; internal set; } = EmptyReadOnly<DataStreamStats>.Collection;
	}

	public class DataStreamStats
	{
		/// <summary>
		/// Name of the data stream.
		/// </summary>
		[DataMember(Name = "data_stream")]
		public string DataStream { get; internal set; }

		/// <summary>
		/// Current number of backing indices for the data stream.
		/// </summary>
		[DataMember(Name = "backing_indices")]
		public int BackingIndices { get; internal set; }

		/// <summary>
		/// Total size of all shards for the data stream's backing indices.
		/// This property is only returned if the `human` query parameter is <c>true</c>
		/// on the request
		/// </summary>
		[DataMember(Name = "store_size")]
		public string StoreSize { get; internal set; }

		/// <summary>
		/// Total size, in bytes, of all shards for the data stream’s backing indices.
		/// </summary>
		[DataMember(Name = "store_size_bytes")]
		public long StoreSizeBytes { get; internal set; }

		/// <summary>
		/// The data stream's highest timestamp value, converted to milliseconds since the Unix epoch.
		/// </summary>
		[DataMember(Name = "maximum_timestamp")]
		public long MaximumTimestamp { get; internal set; }

		/// <summary>
		/// The data stream's highest timestamp value, converted to a <see cref="DateTimeOffset"/>
		/// </summary>
		public DateTimeOffset MaximumTimestampDateTimeOffset => DateTimeUtil.UnixEpoch.AddMilliseconds(MaximumTimestamp);
	}
}
