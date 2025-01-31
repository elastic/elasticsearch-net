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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class PromoteDataStreamRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Promote a data stream.
/// Promote a data stream from a replicated data stream managed by cross-cluster replication (CCR) to a regular data stream.
/// </para>
/// <para>
/// With CCR auto following, a data stream from a remote cluster can be replicated to the local cluster.
/// These data streams can't be rolled over in the local cluster.
/// These replicated data streams roll over only if the upstream data stream rolls over.
/// In the event that the remote cluster is no longer available, the data stream in the local cluster can be promoted to a regular data stream, which allows these data streams to be rolled over in the local cluster.
/// </para>
/// <para>
/// NOTE: When promoting a data stream, ensure the local cluster has a data stream enabled index template that matches the data stream.
/// If this is missing, the data stream will not be able to roll over until a matching index template is created.
/// This will affect the lifecycle management of the data stream and interfere with the data stream size and retention.
/// </para>
/// </summary>
public sealed partial class PromoteDataStreamRequest : PlainRequest<PromoteDataStreamRequestParameters>
{
	public PromoteDataStreamRequest(Elastic.Clients.Elasticsearch.IndexName name) : base(r => r.Required("name", name))
	{
	}

	[JsonConstructor]
	internal PromoteDataStreamRequest()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementPromoteDataStream;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.promote_data_stream";

	/// <summary>
	/// <para>
	/// The name of the data stream
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.IndexName Name { get => P<Elastic.Clients.Elasticsearch.IndexName>("name"); set => PR("name", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>
/// Promote a data stream.
/// Promote a data stream from a replicated data stream managed by cross-cluster replication (CCR) to a regular data stream.
/// </para>
/// <para>
/// With CCR auto following, a data stream from a remote cluster can be replicated to the local cluster.
/// These data streams can't be rolled over in the local cluster.
/// These replicated data streams roll over only if the upstream data stream rolls over.
/// In the event that the remote cluster is no longer available, the data stream in the local cluster can be promoted to a regular data stream, which allows these data streams to be rolled over in the local cluster.
/// </para>
/// <para>
/// NOTE: When promoting a data stream, ensure the local cluster has a data stream enabled index template that matches the data stream.
/// If this is missing, the data stream will not be able to roll over until a matching index template is created.
/// This will affect the lifecycle management of the data stream and interfere with the data stream size and retention.
/// </para>
/// </summary>
public sealed partial class PromoteDataStreamRequestDescriptor : RequestDescriptor<PromoteDataStreamRequestDescriptor, PromoteDataStreamRequestParameters>
{
	internal PromoteDataStreamRequestDescriptor(Action<PromoteDataStreamRequestDescriptor> configure) => configure.Invoke(this);

	public PromoteDataStreamRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementPromoteDataStream;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.promote_data_stream";

	public PromoteDataStreamRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);

	public PromoteDataStreamRequestDescriptor Name(Elastic.Clients.Elasticsearch.IndexName name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}