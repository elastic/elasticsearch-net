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

public sealed partial class HotThreadsRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If true, known idle threads (e.g. waiting in a socket select, or to get
	/// a task from an empty queue) are filtered out.
	/// </para>
	/// </summary>
	public bool? IgnoreIdleThreads { get => Q<bool?>("ignore_idle_threads"); set => Q("ignore_idle_threads", value); }

	/// <summary>
	/// <para>
	/// The interval to do the second sampling of threads.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Interval { get => Q<Elastic.Clients.Elasticsearch.Duration?>("interval"); set => Q("interval", value); }

	/// <summary>
	/// <para>
	/// Number of samples of thread stacktrace.
	/// </para>
	/// </summary>
	public long? Snapshots { get => Q<long?>("snapshots"); set => Q("snapshots", value); }

	/// <summary>
	/// <para>
	/// The sort order for 'cpu' type (default: total)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ThreadType? Sort { get => Q<Elastic.Clients.Elasticsearch.ThreadType?>("sort"); set => Q("sort", value); }

	/// <summary>
	/// <para>
	/// Specifies the number of hot threads to provide information for.
	/// </para>
	/// </summary>
	public long? Threads { get => Q<long?>("threads"); set => Q("threads", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received
	/// before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// The type to sample.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ThreadType? Type { get => Q<Elastic.Clients.Elasticsearch.ThreadType?>("type"); set => Q("type", value); }
}

internal sealed partial class HotThreadsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest>
{
	public override Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get the hot threads for nodes.
/// Get a breakdown of the hot threads on each selected node in the cluster.
/// The output is plain text with a breakdown of the top hot threads for each node.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestConverter))]
public sealed partial class HotThreadsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestParameters>
{
	public HotThreadsRequest(Elastic.Clients.Elasticsearch.NodeIds? nodeId) : base(r => r.Optional("node_id", nodeId))
	{
	}
#if NET7_0_OR_GREATER
	public HotThreadsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public HotThreadsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal HotThreadsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NodesHotThreads;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "nodes.hot_threads";

	/// <summary>
	/// <para>
	/// List of node IDs or names used to limit returned information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.NodeIds? NodeId { get => P<Elastic.Clients.Elasticsearch.NodeIds?>("node_id"); set => PO("node_id", value); }

	/// <summary>
	/// <para>
	/// If true, known idle threads (e.g. waiting in a socket select, or to get
	/// a task from an empty queue) are filtered out.
	/// </para>
	/// </summary>
	public bool? IgnoreIdleThreads { get => Q<bool?>("ignore_idle_threads"); set => Q("ignore_idle_threads", value); }

	/// <summary>
	/// <para>
	/// The interval to do the second sampling of threads.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Interval { get => Q<Elastic.Clients.Elasticsearch.Duration?>("interval"); set => Q("interval", value); }

	/// <summary>
	/// <para>
	/// Number of samples of thread stacktrace.
	/// </para>
	/// </summary>
	public long? Snapshots { get => Q<long?>("snapshots"); set => Q("snapshots", value); }

	/// <summary>
	/// <para>
	/// The sort order for 'cpu' type (default: total)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ThreadType? Sort { get => Q<Elastic.Clients.Elasticsearch.ThreadType?>("sort"); set => Q("sort", value); }

	/// <summary>
	/// <para>
	/// Specifies the number of hot threads to provide information for.
	/// </para>
	/// </summary>
	public long? Threads { get => Q<long?>("threads"); set => Q("threads", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received
	/// before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// The type to sample.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ThreadType? Type { get => Q<Elastic.Clients.Elasticsearch.ThreadType?>("type"); set => Q("type", value); }
}

/// <summary>
/// <para>
/// Get the hot threads for nodes.
/// Get a breakdown of the hot threads on each selected node in the cluster.
/// The output is plain text with a breakdown of the top hot threads for each node.
/// </para>
/// </summary>
public readonly partial struct HotThreadsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HotThreadsRequestDescriptor(Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest instance)
	{
		Instance = instance;
	}

	public HotThreadsRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds nodeId)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest(nodeId);
	}

	public HotThreadsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor(Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest instance) => new Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest(Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// List of node IDs or names used to limit returned information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor NodeId(Elastic.Clients.Elasticsearch.NodeIds? value)
	{
		Instance.NodeId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, known idle threads (e.g. waiting in a socket select, or to get
	/// a task from an empty queue) are filtered out.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor IgnoreIdleThreads(bool? value = true)
	{
		Instance.IgnoreIdleThreads = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The interval to do the second sampling of threads.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor Interval(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Interval = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Number of samples of thread stacktrace.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor Snapshots(long? value)
	{
		Instance.Snapshots = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The sort order for 'cpu' type (default: total)
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor Sort(Elastic.Clients.Elasticsearch.ThreadType? value)
	{
		Instance.Sort = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the number of hot threads to provide information for.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor Threads(long? value)
	{
		Instance.Threads = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received
	/// before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type to sample.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor Type(Elastic.Clients.Elasticsearch.ThreadType? value)
	{
		Instance.Type = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest Build(System.Action<Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor(new Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.HotThreadsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}