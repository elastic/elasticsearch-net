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

public sealed partial class FollowInfoRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// It can also be set to <c>-1</c> to indicate that the request should never timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

internal sealed partial class FollowInfoRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest>
{
	public override Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get follower information.
/// </para>
/// <para>
/// Get information about all cross-cluster replication follower indices.
/// For example, the results include follower index names, leader index names, replication options, and whether the follower indices are active or paused.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestConverter))]
public sealed partial class FollowInfoRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FollowInfoRequest(Elastic.Clients.Elasticsearch.Indices indices) : base(r => r.Required("index", indices))
	{
	}
#if NET7_0_OR_GREATER
	public FollowInfoRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FollowInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.CrossClusterReplicationFollowInfo;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ccr.follow_info";

	/// <summary>
	/// <para>
	/// A comma-delimited list of follower index patterns.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Indices Indices { get => P<Elastic.Clients.Elasticsearch.Indices>("index"); set => PR("index", value); }

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// It can also be set to <c>-1</c> to indicate that the request should never timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Get follower information.
/// </para>
/// <para>
/// Get information about all cross-cluster replication follower indices.
/// For example, the results include follower index names, leader index names, replication options, and whether the follower indices are active or paused.
/// </para>
/// </summary>
public readonly partial struct FollowInfoRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FollowInfoRequestDescriptor(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest instance)
	{
		Instance = instance;
	}

	public FollowInfoRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest(indices);
	}

	[System.Obsolete("TODO")]
	public FollowInfoRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest instance) => new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-delimited list of follower index patterns.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// It can also be set to <c>-1</c> to indicate that the request should never timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest Build(System.Action<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor(new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Get follower information.
/// </para>
/// <para>
/// Get information about all cross-cluster replication follower indices.
/// For example, the results include follower index names, leader index names, replication options, and whether the follower indices are active or paused.
/// </para>
/// </summary>
public readonly partial struct FollowInfoRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FollowInfoRequestDescriptor(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest instance)
	{
		Instance = instance;
	}

	public FollowInfoRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest(indices);
	}

	public FollowInfoRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest(typeof(TDocument));
	}

	public static explicit operator Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest instance) => new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest(Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// A comma-delimited list of follower index patterns.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The period to wait for a connection to the master node.
	/// If the master node is not available before the timeout expires, the request fails and returns an error.
	/// It can also be set to <c>-1</c> to indicate that the request should never timeout.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest Build(System.Action<Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.CrossClusterReplication.FollowInfoRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}