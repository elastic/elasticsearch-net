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

public sealed partial class GetDataLifecycleStatsRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Get data stream lifecycle stats.
/// Get statistics about the data streams that are managed by a data stream lifecycle.
/// </para>
/// </summary>
public sealed partial class GetDataLifecycleStatsRequest : PlainRequest<GetDataLifecycleStatsRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementGetDataLifecycleStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.get_data_lifecycle_stats";
}

/// <summary>
/// <para>
/// Get data stream lifecycle stats.
/// Get statistics about the data streams that are managed by a data stream lifecycle.
/// </para>
/// </summary>
public sealed partial class GetDataLifecycleStatsRequestDescriptor : RequestDescriptor<GetDataLifecycleStatsRequestDescriptor, GetDataLifecycleStatsRequestParameters>
{
	internal GetDataLifecycleStatsRequestDescriptor(Action<GetDataLifecycleStatsRequestDescriptor> configure) => configure.Invoke(this);

	public GetDataLifecycleStatsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementGetDataLifecycleStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.get_data_lifecycle_stats";

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}