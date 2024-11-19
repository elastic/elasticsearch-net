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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.CrossClusterReplication;

public sealed partial class FollowRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Specifies the number of shards to wait on being active before responding. This defaults to waiting on none of the shards to be
	/// active.
	/// A shard must be restored from the leader index before being active. Restoring a follower shard requires transferring all the
	/// remote Lucene segment files to the follower index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
}

/// <summary>
/// <para>
/// Creates a new follower index configured to follow the referenced leader index.
/// </para>
/// </summary>
public sealed partial class FollowRequest : PlainRequest<FollowRequestParameters>
{
	public FollowRequest(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.CrossClusterReplicationFollow;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ccr.follow";

	/// <summary>
	/// <para>
	/// Specifies the number of shards to wait on being active before responding. This defaults to waiting on none of the shards to be
	/// active.
	/// A shard must be restored from the leader index before being active. Restoring a follower shard requires transferring all the
	/// remote Lucene segment files to the follower index.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

	/// <summary>
	/// <para>
	/// If the leader index is part of a data stream, the name to which the local data stream for the followed index should be renamed.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("data_stream_name")]
	public string? DataStreamName { get; set; }

	/// <summary>
	/// <para>
	/// The name of the index in the leader cluster to follow.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("leader_index")]
	public Elastic.Clients.Elasticsearch.IndexName LeaderIndex { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of outstanding reads requests from the remote cluster.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_outstanding_read_requests")]
	public long? MaxOutstandingReadRequests { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of outstanding write requests on the follower.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_outstanding_write_requests")]
	public int? MaxOutstandingWriteRequests { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of operations to pull per read from the remote cluster.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_read_request_operation_count")]
	public int? MaxReadRequestOperationCount { get; set; }

	/// <summary>
	/// <para>
	/// The maximum size in bytes of per read of a batch of operations pulled from the remote cluster.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_read_request_size")]
	public Elastic.Clients.Elasticsearch.ByteSize? MaxReadRequestSize { get; set; }

	/// <summary>
	/// <para>
	/// The maximum time to wait before retrying an operation that failed exceptionally. An exponential backoff strategy is employed when
	/// retrying.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_retry_delay")]
	public Elastic.Clients.Elasticsearch.Duration? MaxRetryDelay { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of operations that can be queued for writing. When this limit is reached, reads from the remote cluster will be
	/// deferred until the number of queued operations goes below the limit.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_write_buffer_count")]
	public int? MaxWriteBufferCount { get; set; }

	/// <summary>
	/// <para>
	/// The maximum total bytes of operations that can be queued for writing. When this limit is reached, reads from the remote cluster will
	/// be deferred until the total bytes of queued operations goes below the limit.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_write_buffer_size")]
	public Elastic.Clients.Elasticsearch.ByteSize? MaxWriteBufferSize { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of operations per bulk write request executed on the follower.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_write_request_operation_count")]
	public int? MaxWriteRequestOperationCount { get; set; }

	/// <summary>
	/// <para>
	/// The maximum total bytes of operations per bulk write request executed on the follower.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_write_request_size")]
	public Elastic.Clients.Elasticsearch.ByteSize? MaxWriteRequestSize { get; set; }

	/// <summary>
	/// <para>
	/// The maximum time to wait for new operations on the remote cluster when the follower index is synchronized with the leader index.
	/// When the timeout has elapsed, the poll for operations will return to the follower so that it can update some statistics.
	/// Then the follower will immediately attempt to read from the leader again.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("read_poll_timeout")]
	public Elastic.Clients.Elasticsearch.Duration? ReadPollTimeout { get; set; }

	/// <summary>
	/// <para>
	/// The remote cluster containing the leader index.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("remote_cluster")]
	public string RemoteCluster { get; set; }

	/// <summary>
	/// <para>
	/// Settings to override from the leader index.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("settings")]
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? Settings { get; set; }
}

/// <summary>
/// <para>
/// Creates a new follower index configured to follow the referenced leader index.
/// </para>
/// </summary>
public sealed partial class FollowRequestDescriptor<TDocument> : RequestDescriptor<FollowRequestDescriptor<TDocument>, FollowRequestParameters>
{
	internal FollowRequestDescriptor(Action<FollowRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public FollowRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	public FollowRequestDescriptor() : this(typeof(TDocument))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.CrossClusterReplicationFollow;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ccr.follow";

	public FollowRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public FollowRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private string? DataStreamNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexName LeaderIndexValue { get; set; }
	private long? MaxOutstandingReadRequestsValue { get; set; }
	private int? MaxOutstandingWriteRequestsValue { get; set; }
	private int? MaxReadRequestOperationCountValue { get; set; }
	private Elastic.Clients.Elasticsearch.ByteSize? MaxReadRequestSizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? MaxRetryDelayValue { get; set; }
	private int? MaxWriteBufferCountValue { get; set; }
	private Elastic.Clients.Elasticsearch.ByteSize? MaxWriteBufferSizeValue { get; set; }
	private int? MaxWriteRequestOperationCountValue { get; set; }
	private Elastic.Clients.Elasticsearch.ByteSize? MaxWriteRequestSizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? ReadPollTimeoutValue { get; set; }
	private string RemoteClusterValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? SettingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument> SettingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>> SettingsDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// If the leader index is part of a data stream, the name to which the local data stream for the followed index should be renamed.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> DataStreamName(string? dataStreamName)
	{
		DataStreamNameValue = dataStreamName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the index in the leader cluster to follow.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> LeaderIndex(Elastic.Clients.Elasticsearch.IndexName leaderIndex)
	{
		LeaderIndexValue = leaderIndex;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of outstanding reads requests from the remote cluster.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> MaxOutstandingReadRequests(long? maxOutstandingReadRequests)
	{
		MaxOutstandingReadRequestsValue = maxOutstandingReadRequests;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of outstanding write requests on the follower.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> MaxOutstandingWriteRequests(int? maxOutstandingWriteRequests)
	{
		MaxOutstandingWriteRequestsValue = maxOutstandingWriteRequests;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of operations to pull per read from the remote cluster.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> MaxReadRequestOperationCount(int? maxReadRequestOperationCount)
	{
		MaxReadRequestOperationCountValue = maxReadRequestOperationCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum size in bytes of per read of a batch of operations pulled from the remote cluster.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> MaxReadRequestSize(Elastic.Clients.Elasticsearch.ByteSize? maxReadRequestSize)
	{
		MaxReadRequestSizeValue = maxReadRequestSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum time to wait before retrying an operation that failed exceptionally. An exponential backoff strategy is employed when
	/// retrying.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> MaxRetryDelay(Elastic.Clients.Elasticsearch.Duration? maxRetryDelay)
	{
		MaxRetryDelayValue = maxRetryDelay;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of operations that can be queued for writing. When this limit is reached, reads from the remote cluster will be
	/// deferred until the number of queued operations goes below the limit.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> MaxWriteBufferCount(int? maxWriteBufferCount)
	{
		MaxWriteBufferCountValue = maxWriteBufferCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum total bytes of operations that can be queued for writing. When this limit is reached, reads from the remote cluster will
	/// be deferred until the total bytes of queued operations goes below the limit.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> MaxWriteBufferSize(Elastic.Clients.Elasticsearch.ByteSize? maxWriteBufferSize)
	{
		MaxWriteBufferSizeValue = maxWriteBufferSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of operations per bulk write request executed on the follower.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> MaxWriteRequestOperationCount(int? maxWriteRequestOperationCount)
	{
		MaxWriteRequestOperationCountValue = maxWriteRequestOperationCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum total bytes of operations per bulk write request executed on the follower.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> MaxWriteRequestSize(Elastic.Clients.Elasticsearch.ByteSize? maxWriteRequestSize)
	{
		MaxWriteRequestSizeValue = maxWriteRequestSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum time to wait for new operations on the remote cluster when the follower index is synchronized with the leader index.
	/// When the timeout has elapsed, the poll for operations will return to the follower so that it can update some statistics.
	/// Then the follower will immediately attempt to read from the leader again.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> ReadPollTimeout(Elastic.Clients.Elasticsearch.Duration? readPollTimeout)
	{
		ReadPollTimeoutValue = readPollTimeout;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The remote cluster containing the leader index.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> RemoteCluster(string remoteCluster)
	{
		RemoteClusterValue = remoteCluster;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Settings to override from the leader index.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? settings)
	{
		SettingsDescriptor = null;
		SettingsDescriptorAction = null;
		SettingsValue = settings;
		return Self;
	}

	public FollowRequestDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument> descriptor)
	{
		SettingsValue = null;
		SettingsDescriptorAction = null;
		SettingsDescriptor = descriptor;
		return Self;
	}

	public FollowRequestDescriptor<TDocument> Settings(Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>> configure)
	{
		SettingsValue = null;
		SettingsDescriptor = null;
		SettingsDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(DataStreamNameValue))
		{
			writer.WritePropertyName("data_stream_name");
			writer.WriteStringValue(DataStreamNameValue);
		}

		writer.WritePropertyName("leader_index");
		JsonSerializer.Serialize(writer, LeaderIndexValue, options);
		if (MaxOutstandingReadRequestsValue.HasValue)
		{
			writer.WritePropertyName("max_outstanding_read_requests");
			writer.WriteNumberValue(MaxOutstandingReadRequestsValue.Value);
		}

		if (MaxOutstandingWriteRequestsValue.HasValue)
		{
			writer.WritePropertyName("max_outstanding_write_requests");
			writer.WriteNumberValue(MaxOutstandingWriteRequestsValue.Value);
		}

		if (MaxReadRequestOperationCountValue.HasValue)
		{
			writer.WritePropertyName("max_read_request_operation_count");
			writer.WriteNumberValue(MaxReadRequestOperationCountValue.Value);
		}

		if (MaxReadRequestSizeValue is not null)
		{
			writer.WritePropertyName("max_read_request_size");
			JsonSerializer.Serialize(writer, MaxReadRequestSizeValue, options);
		}

		if (MaxRetryDelayValue is not null)
		{
			writer.WritePropertyName("max_retry_delay");
			JsonSerializer.Serialize(writer, MaxRetryDelayValue, options);
		}

		if (MaxWriteBufferCountValue.HasValue)
		{
			writer.WritePropertyName("max_write_buffer_count");
			writer.WriteNumberValue(MaxWriteBufferCountValue.Value);
		}

		if (MaxWriteBufferSizeValue is not null)
		{
			writer.WritePropertyName("max_write_buffer_size");
			JsonSerializer.Serialize(writer, MaxWriteBufferSizeValue, options);
		}

		if (MaxWriteRequestOperationCountValue.HasValue)
		{
			writer.WritePropertyName("max_write_request_operation_count");
			writer.WriteNumberValue(MaxWriteRequestOperationCountValue.Value);
		}

		if (MaxWriteRequestSizeValue is not null)
		{
			writer.WritePropertyName("max_write_request_size");
			JsonSerializer.Serialize(writer, MaxWriteRequestSizeValue, options);
		}

		if (ReadPollTimeoutValue is not null)
		{
			writer.WritePropertyName("read_poll_timeout");
			JsonSerializer.Serialize(writer, ReadPollTimeoutValue, options);
		}

		writer.WritePropertyName("remote_cluster");
		writer.WriteStringValue(RemoteClusterValue);
		if (SettingsDescriptor is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsDescriptor, options);
		}
		else if (SettingsDescriptorAction is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>(SettingsDescriptorAction), options);
		}
		else if (SettingsValue is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Creates a new follower index configured to follow the referenced leader index.
/// </para>
/// </summary>
public sealed partial class FollowRequestDescriptor : RequestDescriptor<FollowRequestDescriptor, FollowRequestParameters>
{
	internal FollowRequestDescriptor(Action<FollowRequestDescriptor> configure) => configure.Invoke(this);

	public FollowRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.CrossClusterReplicationFollow;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ccr.follow";

	public FollowRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public FollowRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private string? DataStreamNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexName LeaderIndexValue { get; set; }
	private long? MaxOutstandingReadRequestsValue { get; set; }
	private int? MaxOutstandingWriteRequestsValue { get; set; }
	private int? MaxReadRequestOperationCountValue { get; set; }
	private Elastic.Clients.Elasticsearch.ByteSize? MaxReadRequestSizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? MaxRetryDelayValue { get; set; }
	private int? MaxWriteBufferCountValue { get; set; }
	private Elastic.Clients.Elasticsearch.ByteSize? MaxWriteBufferSizeValue { get; set; }
	private int? MaxWriteRequestOperationCountValue { get; set; }
	private Elastic.Clients.Elasticsearch.ByteSize? MaxWriteRequestSizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? ReadPollTimeoutValue { get; set; }
	private string RemoteClusterValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? SettingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor SettingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor> SettingsDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// If the leader index is part of a data stream, the name to which the local data stream for the followed index should be renamed.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor DataStreamName(string? dataStreamName)
	{
		DataStreamNameValue = dataStreamName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the index in the leader cluster to follow.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor LeaderIndex(Elastic.Clients.Elasticsearch.IndexName leaderIndex)
	{
		LeaderIndexValue = leaderIndex;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of outstanding reads requests from the remote cluster.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor MaxOutstandingReadRequests(long? maxOutstandingReadRequests)
	{
		MaxOutstandingReadRequestsValue = maxOutstandingReadRequests;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of outstanding write requests on the follower.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor MaxOutstandingWriteRequests(int? maxOutstandingWriteRequests)
	{
		MaxOutstandingWriteRequestsValue = maxOutstandingWriteRequests;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of operations to pull per read from the remote cluster.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor MaxReadRequestOperationCount(int? maxReadRequestOperationCount)
	{
		MaxReadRequestOperationCountValue = maxReadRequestOperationCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum size in bytes of per read of a batch of operations pulled from the remote cluster.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor MaxReadRequestSize(Elastic.Clients.Elasticsearch.ByteSize? maxReadRequestSize)
	{
		MaxReadRequestSizeValue = maxReadRequestSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum time to wait before retrying an operation that failed exceptionally. An exponential backoff strategy is employed when
	/// retrying.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor MaxRetryDelay(Elastic.Clients.Elasticsearch.Duration? maxRetryDelay)
	{
		MaxRetryDelayValue = maxRetryDelay;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of operations that can be queued for writing. When this limit is reached, reads from the remote cluster will be
	/// deferred until the number of queued operations goes below the limit.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor MaxWriteBufferCount(int? maxWriteBufferCount)
	{
		MaxWriteBufferCountValue = maxWriteBufferCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum total bytes of operations that can be queued for writing. When this limit is reached, reads from the remote cluster will
	/// be deferred until the total bytes of queued operations goes below the limit.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor MaxWriteBufferSize(Elastic.Clients.Elasticsearch.ByteSize? maxWriteBufferSize)
	{
		MaxWriteBufferSizeValue = maxWriteBufferSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum number of operations per bulk write request executed on the follower.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor MaxWriteRequestOperationCount(int? maxWriteRequestOperationCount)
	{
		MaxWriteRequestOperationCountValue = maxWriteRequestOperationCount;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum total bytes of operations per bulk write request executed on the follower.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor MaxWriteRequestSize(Elastic.Clients.Elasticsearch.ByteSize? maxWriteRequestSize)
	{
		MaxWriteRequestSizeValue = maxWriteRequestSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The maximum time to wait for new operations on the remote cluster when the follower index is synchronized with the leader index.
	/// When the timeout has elapsed, the poll for operations will return to the follower so that it can update some statistics.
	/// Then the follower will immediately attempt to read from the leader again.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor ReadPollTimeout(Elastic.Clients.Elasticsearch.Duration? readPollTimeout)
	{
		ReadPollTimeoutValue = readPollTimeout;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The remote cluster containing the leader index.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor RemoteCluster(string remoteCluster)
	{
		RemoteClusterValue = remoteCluster;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Settings to override from the leader index.
	/// </para>
	/// </summary>
	public FollowRequestDescriptor Settings(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? settings)
	{
		SettingsDescriptor = null;
		SettingsDescriptorAction = null;
		SettingsValue = settings;
		return Self;
	}

	public FollowRequestDescriptor Settings(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor descriptor)
	{
		SettingsValue = null;
		SettingsDescriptorAction = null;
		SettingsDescriptor = descriptor;
		return Self;
	}

	public FollowRequestDescriptor Settings(Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor> configure)
	{
		SettingsValue = null;
		SettingsDescriptor = null;
		SettingsDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(DataStreamNameValue))
		{
			writer.WritePropertyName("data_stream_name");
			writer.WriteStringValue(DataStreamNameValue);
		}

		writer.WritePropertyName("leader_index");
		JsonSerializer.Serialize(writer, LeaderIndexValue, options);
		if (MaxOutstandingReadRequestsValue.HasValue)
		{
			writer.WritePropertyName("max_outstanding_read_requests");
			writer.WriteNumberValue(MaxOutstandingReadRequestsValue.Value);
		}

		if (MaxOutstandingWriteRequestsValue.HasValue)
		{
			writer.WritePropertyName("max_outstanding_write_requests");
			writer.WriteNumberValue(MaxOutstandingWriteRequestsValue.Value);
		}

		if (MaxReadRequestOperationCountValue.HasValue)
		{
			writer.WritePropertyName("max_read_request_operation_count");
			writer.WriteNumberValue(MaxReadRequestOperationCountValue.Value);
		}

		if (MaxReadRequestSizeValue is not null)
		{
			writer.WritePropertyName("max_read_request_size");
			JsonSerializer.Serialize(writer, MaxReadRequestSizeValue, options);
		}

		if (MaxRetryDelayValue is not null)
		{
			writer.WritePropertyName("max_retry_delay");
			JsonSerializer.Serialize(writer, MaxRetryDelayValue, options);
		}

		if (MaxWriteBufferCountValue.HasValue)
		{
			writer.WritePropertyName("max_write_buffer_count");
			writer.WriteNumberValue(MaxWriteBufferCountValue.Value);
		}

		if (MaxWriteBufferSizeValue is not null)
		{
			writer.WritePropertyName("max_write_buffer_size");
			JsonSerializer.Serialize(writer, MaxWriteBufferSizeValue, options);
		}

		if (MaxWriteRequestOperationCountValue.HasValue)
		{
			writer.WritePropertyName("max_write_request_operation_count");
			writer.WriteNumberValue(MaxWriteRequestOperationCountValue.Value);
		}

		if (MaxWriteRequestSizeValue is not null)
		{
			writer.WritePropertyName("max_write_request_size");
			JsonSerializer.Serialize(writer, MaxWriteRequestSizeValue, options);
		}

		if (ReadPollTimeoutValue is not null)
		{
			writer.WritePropertyName("read_poll_timeout");
			JsonSerializer.Serialize(writer, ReadPollTimeoutValue, options);
		}

		writer.WritePropertyName("remote_cluster");
		writer.WriteStringValue(RemoteClusterValue);
		if (SettingsDescriptor is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsDescriptor, options);
		}
		else if (SettingsDescriptorAction is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor(SettingsDescriptorAction), options);
		}
		else if (SettingsValue is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsValue, options);
		}

		writer.WriteEndObject();
	}
}