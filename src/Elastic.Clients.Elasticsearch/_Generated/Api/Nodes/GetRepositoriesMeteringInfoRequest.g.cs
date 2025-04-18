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

namespace Elastic.Clients.Elasticsearch.Nodes;

public sealed partial class GetRepositoriesMeteringInfoRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class GetRepositoriesMeteringInfoRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequest>
{
	public override Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get cluster repositories metering.
/// Get repositories metering information for a cluster.
/// This API exposes monotonically non-decreasing counters and it is expected that clients would durably store the information needed to compute aggregations over a period of time.
/// Additionally, the information exposed by this API is volatile, meaning that it will not be present after node restarts.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestConverter))]
public sealed partial class GetRepositoriesMeteringInfoRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetRepositoriesMeteringInfoRequest(Elastic.Clients.Elasticsearch.NodeIds nodeId) : base(r => r.Required("node_id", nodeId))
	{
	}
#if NET7_0_OR_GREATER
	public GetRepositoriesMeteringInfoRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GetRepositoriesMeteringInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NodesGetRepositoriesMeteringInfo;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "nodes.get_repositories_metering_info";

	/// <summary>
	/// <para>
	/// Comma-separated list of node IDs or names used to limit returned information.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.NodeIds NodeId { get => P<Elastic.Clients.Elasticsearch.NodeIds>("node_id"); set => PR("node_id", value); }
}

/// <summary>
/// <para>
/// Get cluster repositories metering.
/// Get repositories metering information for a cluster.
/// This API exposes monotonically non-decreasing counters and it is expected that clients would durably store the information needed to compute aggregations over a period of time.
/// Additionally, the information exposed by this API is volatile, meaning that it will not be present after node restarts.
/// </para>
/// </summary>
public readonly partial struct GetRepositoriesMeteringInfoRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GetRepositoriesMeteringInfoRequestDescriptor(Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequest instance)
	{
		Instance = instance;
	}

	public GetRepositoriesMeteringInfoRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds nodeId)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequest(nodeId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public GetRepositoriesMeteringInfoRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor(Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequest instance) => new Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequest(Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Comma-separated list of node IDs or names used to limit returned information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor NodeId(Elastic.Clients.Elasticsearch.NodeIds value)
	{
		Instance.NodeId = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequest Build(System.Action<Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor(new Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.GetRepositoriesMeteringInfoRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}