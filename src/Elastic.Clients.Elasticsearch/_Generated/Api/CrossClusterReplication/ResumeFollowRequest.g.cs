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

public sealed partial class ResumeFollowRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

internal sealed partial class ResumeFollowRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequest>
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

	public override Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long?> propMaxOutstandingReadRequests = default;
		LocalJsonValue<long?> propMaxOutstandingWriteRequests = default;
		LocalJsonValue<long?> propMaxReadRequestOperationCount = default;
		LocalJsonValue<string?> propMaxReadRequestSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propMaxRetryDelay = default;
		LocalJsonValue<long?> propMaxWriteBufferCount = default;
		LocalJsonValue<string?> propMaxWriteBufferSize = default;
		LocalJsonValue<long?> propMaxWriteRequestOperationCount = default;
		LocalJsonValue<string?> propMaxWriteRequestSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propReadPollTimeout = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMaxOutstandingReadRequests.TryReadProperty(ref reader, options, PropMaxOutstandingReadRequests, null))
			{
				continue;
			}

			if (propMaxOutstandingWriteRequests.TryReadProperty(ref reader, options, PropMaxOutstandingWriteRequests, null))
			{
				continue;
			}

			if (propMaxReadRequestOperationCount.TryReadProperty(ref reader, options, PropMaxReadRequestOperationCount, null))
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

			if (propMaxWriteBufferCount.TryReadProperty(ref reader, options, PropMaxWriteBufferCount, null))
			{
				continue;
			}

			if (propMaxWriteBufferSize.TryReadProperty(ref reader, options, PropMaxWriteBufferSize, null))
			{
				continue;
			}

			if (propMaxWriteRequestOperationCount.TryReadProperty(ref reader, options, PropMaxWriteRequestOperationCount, null))
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
		return new Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
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

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMaxOutstandingReadRequests, value.MaxOutstandingReadRequests, null, null);
		writer.WriteProperty(options, PropMaxOutstandingWriteRequests, value.MaxOutstandingWriteRequests, null, null);
		writer.WriteProperty(options, PropMaxReadRequestOperationCount, value.MaxReadRequestOperationCount, null, null);
		writer.WriteProperty(options, PropMaxReadRequestSize, value.MaxReadRequestSize, null, null);
		writer.WriteProperty(options, PropMaxRetryDelay, value.MaxRetryDelay, null, null);
		writer.WriteProperty(options, PropMaxWriteBufferCount, value.MaxWriteBufferCount, null, null);
		writer.WriteProperty(options, PropMaxWriteBufferSize, value.MaxWriteBufferSize, null, null);
		writer.WriteProperty(options, PropMaxWriteRequestOperationCount, value.MaxWriteRequestOperationCount, null, null);
		writer.WriteProperty(options, PropMaxWriteRequestSize, value.MaxWriteRequestSize, null, null);
		writer.WriteProperty(options, PropReadPollTimeout, value.ReadPollTimeout, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Resume a follower.
/// Resume a cross-cluster replication follower index that was paused.
/// The follower index could have been paused with the pause follower API.
/// Alternatively it could be paused due to replication that cannot be retried due to failures during following tasks.
/// When this API returns, the follower index will resume fetching operations from the leader index.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestConverter))]
public sealed partial class ResumeFollowRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ResumeFollowRequest(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}
#if NET7_0_OR_GREATER
	public ResumeFollowRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ResumeFollowRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.CrossClusterReplicationResumeFollow;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ccr.resume_follow";

	/// <summary>
	/// <para>
	/// The name of the follow index to resume following.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexName Index { get => P<Elastic.Clients.Elasticsearch.IndexName>("index"); set => PR("index", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
	public long? MaxOutstandingReadRequests { get; set; }
	public long? MaxOutstandingWriteRequests { get; set; }
	public long? MaxReadRequestOperationCount { get; set; }
	public string? MaxReadRequestSize { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? MaxRetryDelay { get; set; }
	public long? MaxWriteBufferCount { get; set; }
	public string? MaxWriteBufferSize { get; set; }
	public long? MaxWriteRequestOperationCount { get; set; }
	public string? MaxWriteRequestSize { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? ReadPollTimeout { get; set; }
}

/// <summary>
/// <para>
/// Resume a follower.
/// Resume a cross-cluster replication follower index that was paused.
/// The follower index could have been paused with the pause follower API.
/// Alternatively it could be paused due to replication that cannot be retried due to failures during following tasks.
/// When this API returns, the follower index will resume fetching operations from the leader index.
/// </para>
/// </summary>
public readonly partial struct ResumeFollowRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ResumeFollowRequestDescriptor(Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequest instance)
	{
		Instance = instance;
	}

	public ResumeFollowRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index)
	{
		Instance = new Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequest(index);
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ResumeFollowRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor(Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequest instance) => new Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequest(Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The name of the follow index to resume following.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor MaxOutstandingReadRequests(long? value)
	{
		Instance.MaxOutstandingReadRequests = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor MaxOutstandingWriteRequests(long? value)
	{
		Instance.MaxOutstandingWriteRequests = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor MaxReadRequestOperationCount(long? value)
	{
		Instance.MaxReadRequestOperationCount = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor MaxReadRequestSize(string? value)
	{
		Instance.MaxReadRequestSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor MaxRetryDelay(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MaxRetryDelay = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor MaxWriteBufferCount(long? value)
	{
		Instance.MaxWriteBufferCount = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor MaxWriteBufferSize(string? value)
	{
		Instance.MaxWriteBufferSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor MaxWriteRequestOperationCount(long? value)
	{
		Instance.MaxWriteRequestOperationCount = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor MaxWriteRequestSize(string? value)
	{
		Instance.MaxWriteRequestSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor ReadPollTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.ReadPollTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequest Build(System.Action<Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor(new Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.ResumeFollowRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}