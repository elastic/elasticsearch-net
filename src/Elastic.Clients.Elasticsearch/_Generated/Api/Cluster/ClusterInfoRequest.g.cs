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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Cluster;

public sealed partial class ClusterInfoRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Get cluster info.
/// Returns basic information about the cluster.
/// </para>
/// </summary>
public sealed partial class ClusterInfoRequest : PlainRequest<ClusterInfoRequestParameters>
{
	public ClusterInfoRequest(IReadOnlyCollection<Elastic.Clients.Elasticsearch.ClusterInfoTarget> target) : base(r => r.Required("target", target))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.ClusterInfo;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "cluster.info";
}

/// <summary>
/// <para>
/// Get cluster info.
/// Returns basic information about the cluster.
/// </para>
/// </summary>
public sealed partial class ClusterInfoRequestDescriptor : RequestDescriptor<ClusterInfoRequestDescriptor, ClusterInfoRequestParameters>
{
	internal ClusterInfoRequestDescriptor(Action<ClusterInfoRequestDescriptor> configure) => configure.Invoke(this);

	public ClusterInfoRequestDescriptor(IReadOnlyCollection<Elastic.Clients.Elasticsearch.ClusterInfoTarget> target) : base(r => r.Required("target", target))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.ClusterInfo;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "cluster.info";

	public ClusterInfoRequestDescriptor Target(IReadOnlyCollection<Elastic.Clients.Elasticsearch.ClusterInfoTarget> target)
	{
		RouteValues.Required("target", target);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}