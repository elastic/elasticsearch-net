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

public sealed partial class NodesUsageRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

internal sealed partial class NodesUsageRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest>
{
	public override Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get feature usage information.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestConverter))]
public sealed partial class NodesUsageRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestParameters>
{
	public NodesUsageRequest(Elastic.Clients.Elasticsearch.NodeIds? nodeId) : base(r => r.Optional("node_id", nodeId))
	{
	}

	public NodesUsageRequest(Elastic.Clients.Elasticsearch.Metrics? metric) : base(r => r.Optional("metric", metric))
	{
	}

	public NodesUsageRequest(Elastic.Clients.Elasticsearch.NodeIds? nodeId, Elastic.Clients.Elasticsearch.Metrics? metric) : base(r => r.Optional("node_id", nodeId).Optional("metric", metric))
	{
	}
#if NET7_0_OR_GREATER
	public NodesUsageRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public NodesUsageRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NodesUsageRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NodesUsage;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "nodes.usage";

	/// <summary>
	/// <para>
	/// Limits the information returned to the specific metrics.
	/// A comma-separated list of the following options: <c>_all</c>, <c>rest_actions</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Metrics? Metric { get => P<Elastic.Clients.Elasticsearch.Metrics?>("metric"); set => PO("metric", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of node IDs or names to limit the returned information; use <c>_local</c> to return information from the node you're connecting to, leave empty to get information from all nodes
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.NodeIds? NodeId { get => P<Elastic.Clients.Elasticsearch.NodeIds?>("node_id"); set => PO("node_id", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Get feature usage information.
/// </para>
/// </summary>
public readonly partial struct NodesUsageRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NodesUsageRequestDescriptor(Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest instance)
	{
		Instance = instance;
	}

	public NodesUsageRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds? nodeId)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest(nodeId);
	}

	public NodesUsageRequestDescriptor(Elastic.Clients.Elasticsearch.Metrics? metric)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest(metric);
	}

	public NodesUsageRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds? nodeId, Elastic.Clients.Elasticsearch.Metrics? metric)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest(nodeId, metric);
	}

	public NodesUsageRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor(Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest instance) => new Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest(Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Limits the information returned to the specific metrics.
	/// A comma-separated list of the following options: <c>_all</c>, <c>rest_actions</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor Metric(Elastic.Clients.Elasticsearch.Metrics? value)
	{
		Instance.Metric = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of node IDs or names to limit the returned information; use <c>_local</c> to return information from the node you're connecting to, leave empty to get information from all nodes
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor NodeId(Elastic.Clients.Elasticsearch.NodeIds? value)
	{
		Instance.NodeId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest Build(System.Action<Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor(new Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesUsageRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}