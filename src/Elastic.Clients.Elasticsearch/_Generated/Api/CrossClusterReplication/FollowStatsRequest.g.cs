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

public sealed partial class FollowStatsRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

internal sealed partial class FollowStatsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequest>
{
	public override Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get follower stats.
/// </para>
/// <para>
/// Get cross-cluster replication follower stats.
/// The API returns shard-level stats about the "following tasks" associated with each shard for the specified indices.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestConverter))]
public sealed partial class FollowStatsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FollowStatsRequest(Elastic.Clients.Elasticsearch.Indices indices) : base(r => r.Required("index", indices))
	{
	}
#if NET7_0_OR_GREATER
	public FollowStatsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FollowStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.CrossClusterReplicationFollowStats;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ccr.follow_stats";

	/// <summary>
	/// <para>
	/// A comma-delimited list of index patterns.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Indices Indices { get => P<Elastic.Clients.Elasticsearch.Indices>("index"); set => PR("index", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Get follower stats.
/// </para>
/// <para>
/// Get cross-cluster replication follower stats.
/// The API returns shard-level stats about the "following tasks" associated with each shard for the specified indices.
/// </para>
/// </summary>
public readonly partial struct FollowStatsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FollowStatsRequestDescriptor(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequest instance)
	{
		Instance = instance;
	}

	public FollowStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequest(indices);
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public FollowStatsRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequest instance) => new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequest(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-delimited list of index patterns.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequest Build(System.Action<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor(new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowStatsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}