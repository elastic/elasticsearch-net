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

namespace Elastic.Clients.Elasticsearch.CrossClusterReplication;

internal sealed partial class FollowerIndexParametersConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexParameters>
{
	private static readonly System.Text.Json.JsonEncodedText PropMaxOutstandingReadRequests = System.Text.Json.JsonEncodedText.Encode("max_outstanding_read_requests");
	private static readonly System.Text.Json.JsonEncodedText PropMaxOutstandingWriteRequests = System.Text.Json.JsonEncodedText.Encode("max_outstanding_write_requests");
	private static readonly System.Text.Json.JsonEncodedText PropMaxReadRequestOperationCount = System.Text.Json.JsonEncodedText.Encode("max_read_request_operation_count");
	private static readonly System.Text.Json.JsonEncodedText PropMaxReadRequestSize = System.Text.Json.JsonEncodedText.Encode("max_read_request_size");
	private static readonly System.Text.Json.JsonEncodedText PropMaxRetryDelay = System.Text.Json.JsonEncodedText.Encode("max_retry_delay");
	private static readonly System.Text.Json.JsonEncodedText PropMaxWriteBufferCount = System.Text.Json.JsonEncodedText.Encode("max_write_buffer_count");
	private static readonly System.Text.Json.JsonEncodedText PropMaxWriteBufferSize = System.Text.Json.JsonEncodedText.Encode("max_write_buffer_size");
	private static readonly System.Text.Json.JsonEncodedText PropMaxWriteRequestOperationCount = System.Text.Json.JsonEncodedText.Encode("max_write_request_operation_count");
	private static readonly System.Text.Json.JsonEncodedText PropMaxWriteRequestSize = System.Text.Json.JsonEncodedText.Encode("max_write_request_size");
	private static readonly System.Text.Json.JsonEncodedText PropReadPollTimeout = System.Text.Json.JsonEncodedText.Encode("read_poll_timeout");

	public override Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexParameters Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long?> propMaxOutstandingReadRequests = default;
		LocalJsonValue<int?> propMaxOutstandingWriteRequests = default;
		LocalJsonValue<int?> propMaxReadRequestOperationCount = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMaxReadRequestSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propMaxRetryDelay = default;
		LocalJsonValue<int?> propMaxWriteBufferCount = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMaxWriteBufferSize = default;
		LocalJsonValue<int?> propMaxWriteRequestOperationCount = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMaxWriteRequestSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propReadPollTimeout = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMaxOutstandingReadRequests.TryReadProperty(ref reader, options, PropMaxOutstandingReadRequests, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propMaxOutstandingWriteRequests.TryReadProperty(ref reader, options, PropMaxOutstandingWriteRequests, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propMaxReadRequestOperationCount.TryReadProperty(ref reader, options, PropMaxReadRequestOperationCount, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propMaxReadRequestSize.TryReadProperty(ref reader, options, PropMaxReadRequestSize, null))
			{
				continue;
			}

			if (propMaxRetryDelay.TryReadProperty(ref reader, options, PropMaxRetryDelay, null))
			{
				continue;
			}

			if (propMaxWriteBufferCount.TryReadProperty(ref reader, options, PropMaxWriteBufferCount, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propMaxWriteBufferSize.TryReadProperty(ref reader, options, PropMaxWriteBufferSize, null))
			{
				continue;
			}

			if (propMaxWriteRequestOperationCount.TryReadProperty(ref reader, options, PropMaxWriteRequestOperationCount, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propMaxWriteRequestSize.TryReadProperty(ref reader, options, PropMaxWriteRequestSize, null))
			{
				continue;
			}

			if (propReadPollTimeout.TryReadProperty(ref reader, options, PropReadPollTimeout, null))
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
		return new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexParameters(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			MaxOutstandingReadRequests = propMaxOutstandingReadRequests.Value,
			MaxOutstandingWriteRequests = propMaxOutstandingWriteRequests.Value,
			MaxReadRequestOperationCount = propMaxReadRequestOperationCount.Value,
			MaxReadRequestSize = propMaxReadRequestSize.Value,
			MaxRetryDelay = propMaxRetryDelay.Value,
			MaxWriteBufferCount = propMaxWriteBufferCount.Value,
			MaxWriteBufferSize = propMaxWriteBufferSize.Value,
			MaxWriteRequestOperationCount = propMaxWriteRequestOperationCount.Value,
			MaxWriteRequestSize = propMaxWriteRequestSize.Value,
			ReadPollTimeout = propReadPollTimeout.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexParameters value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMaxOutstandingReadRequests, value.MaxOutstandingReadRequests, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropMaxOutstandingWriteRequests, value.MaxOutstandingWriteRequests, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropMaxReadRequestOperationCount, value.MaxReadRequestOperationCount, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropMaxReadRequestSize, value.MaxReadRequestSize, null, null);
		writer.WriteProperty(options, PropMaxRetryDelay, value.MaxRetryDelay, null, null);
		writer.WriteProperty(options, PropMaxWriteBufferCount, value.MaxWriteBufferCount, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropMaxWriteBufferSize, value.MaxWriteBufferSize, null, null);
		writer.WriteProperty(options, PropMaxWriteRequestOperationCount, value.MaxWriteRequestOperationCount, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropMaxWriteRequestSize, value.MaxWriteRequestSize, null, null);
		writer.WriteProperty(options, PropReadPollTimeout, value.ReadPollTimeout, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowerIndexParametersConverter))]
public sealed partial class FollowerIndexParameters
{
#if NET7_0_OR_GREATER
	public FollowerIndexParameters()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public FollowerIndexParameters()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FollowerIndexParameters(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The maximum number of outstanding reads requests from the remote cluster.
	/// </para>
	/// </summary>
	public long? MaxOutstandingReadRequests { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of outstanding write requests on the follower.
	/// </para>
	/// </summary>
	public int? MaxOutstandingWriteRequests { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of operations to pull per read from the remote cluster.
	/// </para>
	/// </summary>
	public int? MaxReadRequestOperationCount { get; set; }

	/// <summary>
	/// <para>
	/// The maximum size in bytes of per read of a batch of operations pulled from the remote cluster.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? MaxReadRequestSize { get; set; }

	/// <summary>
	/// <para>
	/// The maximum time to wait before retrying an operation that failed exceptionally. An exponential backoff strategy is employed when
	/// retrying.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MaxRetryDelay { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of operations that can be queued for writing. When this limit is reached, reads from the remote cluster will be
	/// deferred until the number of queued operations goes below the limit.
	/// </para>
	/// </summary>
	public int? MaxWriteBufferCount { get; set; }

	/// <summary>
	/// <para>
	/// The maximum total bytes of operations that can be queued for writing. When this limit is reached, reads from the remote cluster will
	/// be deferred until the total bytes of queued operations goes below the limit.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? MaxWriteBufferSize { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of operations per bulk write request executed on the follower.
	/// </para>
	/// </summary>
	public int? MaxWriteRequestOperationCount { get; set; }

	/// <summary>
	/// <para>
	/// The maximum total bytes of operations per bulk write request executed on the follower.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? MaxWriteRequestSize { get; set; }

	/// <summary>
	/// <para>
	/// The maximum time to wait for new operations on the remote cluster when the follower index is synchronized with the leader index.
	/// When the timeout has elapsed, the poll for operations will return to the follower so that it can update some statistics.
	/// Then the follower will immediately attempt to read from the leader again.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? ReadPollTimeout { get; set; }
}